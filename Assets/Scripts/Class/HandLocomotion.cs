using UnityEngine;
using System.Collections;


public class HandLocomotion : MonoBehaviour
{
    [Header("References")]
    public Transform bodyTransform;
    public Transform leftHandTransform;
    public Transform rightHandTransform;
    public Rigidbody bodyRigidbody;
    public HandCollisionDetector leftHandDetector;
    public HandCollisionDetector rightHandDetector;
    public PlayerEnergy playerEnergy; // new
    public float energyPerMove = 5f; // new
    private float energyDrainCooldown = 1f; // Consume energy once per second
    private float energyDrainTimer = 0f; // new


    [Header("Movement Settings")]
    public float movementMultiplier = 1f;
    public float velocityTransferMultiplier = 1f;
    public float maxMovementPerFrame = 0.5f;

    [Header("Transition Settings")]
    public int framesBeforeKinematic = 5;
    public int framesBeforeNonKinematic = 10;

    // Internal tracking
    private Vector3 lastLeftHandLocal;
    private Vector3 lastRightHandLocal;
    private Vector3 lastVelocity;
    private bool wasControllingLastFrame;
    private bool needsLeftHandReset;
    private bool needsRightHandReset;
    private float initialGrabRotation;
    private float initialBodyRotation;
    private bool wasTwoHandedLastFrame;

    // Transition counters
    private int kinematicTransitionCounter = 0;
    private int nonKinematicTransitionCounter = 0;
    private bool isTransitioningToKinematic = false;
    private bool isTransitioningFromKinematic = false;

    private Vector3 leftGrabWorldPosition;
    private Vector3 rightGrabWorldPosition;

    private float defaultMovementMultiplier; // for sticky platform
    private Coroutine restoreCoroutine; // for sticky platform


    private void Start()
    {

        defaultMovementMultiplier = movementMultiplier; // for sticky platform

        if (bodyRigidbody == null)
        {
            Debug.LogError("HandLocomotion: Body Rigidbody reference is required!");
            enabled = false;
            return;
        }

        if (leftHandDetector == null || rightHandDetector == null)
        {
            Debug.LogError("HandLocomotion: Both hand detectors must be assigned!");
            enabled = false;
            return;
        }

        ResetHandPositions();
    }

    private void FixedUpdate()
    {
        bool isControlling = leftHandDetector.isTouchingClimbable || rightHandDetector.isTouchingClimbable;
        bool isTwoHanded = leftHandDetector.isTouchingClimbable && rightHandDetector.isTouchingClimbable;

        // Handle transition to hand control
        if (isControlling && !wasControllingLastFrame && !isTransitioningToKinematic)
        {
            isTransitioningToKinematic = true;
            kinematicTransitionCounter = 0;
        }

        // Handle transition to physics control
        if (!isControlling && wasControllingLastFrame && !isTransitioningFromKinematic)
        {
            isTransitioningFromKinematic = true;
            nonKinematicTransitionCounter = 0;
        }

        // Process transitions
        ProcessTransitions(isControlling);

        // Process hand movement when controlling
        if (bodyRigidbody.isKinematic && isControlling)
        {
            if (isTwoHanded)
            {
                if (!wasTwoHandedLastFrame)
                {
                    // Just grabbed with both hands, store initial angles and positions
                    initialGrabRotation = CalculateHandsAngle(GetLocalHandPosition(leftHandTransform),
                                                            GetLocalHandPosition(rightHandTransform));
                    initialBodyRotation = bodyTransform.eulerAngles.y;

                    // Store world positions of grab points
                    leftGrabWorldPosition = leftHandTransform.position;
                    rightGrabWorldPosition = rightHandTransform.position;
                }
                ProcessTwoHandedControl();
            }
            else
            {
                ProcessHandMovement();
            }
        }

        wasControllingLastFrame = isControlling;
        wasTwoHandedLastFrame = isTwoHanded;
    }

    private void ProcessTwoHandedControl()
    {
        Vector3 currentLeftLocal = GetLocalHandPosition(leftHandTransform);
        Vector3 currentRightLocal = GetLocalHandPosition(rightHandTransform);

        // Calculate current hands angle
        float currentHandsAngle = CalculateHandsAngle(currentLeftLocal, currentRightLocal);

        // Calculate absolute rotation
        float rotationDelta = currentHandsAngle - initialGrabRotation;
        float targetRotation = initialBodyRotation + rotationDelta;

        // Store current position before rotation
        Vector3 oldPosition = bodyTransform.position;

        // Apply absolute rotation to Y axis only
        Vector3 currentEuler = bodyTransform.eulerAngles;
        bodyTransform.eulerAngles = new Vector3(currentEuler.x, targetRotation, currentEuler.z);

        // After rotation, adjust position to maintain hand positions
        Vector3 leftOffset = leftGrabWorldPosition - leftHandTransform.position;
        Vector3 rightOffset = rightGrabWorldPosition - rightHandTransform.position;

        // Use average of both hand offsets to move the body
        Vector3 averageOffset = (leftOffset + rightOffset) * 0.5f;
        bodyTransform.position = oldPosition + averageOffset;

        // Update hand positions for next frame
        lastLeftHandLocal = GetLocalHandPosition(leftHandTransform);
        lastRightHandLocal = GetLocalHandPosition(rightHandTransform);
    }

    private float CalculateHandsAngle(Vector3 leftPos, Vector3 rightPos)
    {
        // Project positions onto XZ plane
        Vector2 leftFlat = new Vector2(leftPos.x, leftPos.z);
        Vector2 rightFlat = new Vector2(rightPos.x, rightPos.z);
        Vector2 handsDirection = (rightFlat - leftFlat).normalized;

        // Calculate absolute angle
        return Mathf.Atan2(handsDirection.y, handsDirection.x) * Mathf.Rad2Deg;
    }

    private void ProcessTransitions(bool isControlling)
    {
        // Handle transition to kinematic
        if (isTransitioningToKinematic)
        {
            kinematicTransitionCounter++;
            if (kinematicTransitionCounter >= framesBeforeKinematic)
            {
                bodyRigidbody.isKinematic = true;
                ResetHandPositions();
                isTransitioningToKinematic = false;
            }
        }

        // Handle transition from kinematic
        if (isTransitioningFromKinematic)
        {
            nonKinematicTransitionCounter++;
            if (nonKinematicTransitionCounter >= framesBeforeNonKinematic)
            {
                bodyRigidbody.isKinematic = false;
                bodyRigidbody.linearVelocity = lastVelocity;
                isTransitioningFromKinematic = false;
            }
        }

        // Cancel transitions if state changes during transition
        if (isTransitioningToKinematic && !isControlling)
        {
            isTransitioningToKinematic = false;
        }
        if (isTransitioningFromKinematic && isControlling)
        {
            isTransitioningFromKinematic = false;
        }
    }

    private void ProcessHandMovement()
    {
        Vector3 totalMovement = Vector3.zero;

        // Process left hand
        if (leftHandDetector.isTouchingClimbable)
        {
            if (needsLeftHandReset)
            {
                lastLeftHandLocal = GetLocalHandPosition(leftHandTransform);
                needsLeftHandReset = false;
            }
            else
            {
                Vector3 currentLeftLocal = GetLocalHandPosition(leftHandTransform);
                Vector3 leftMovement = currentLeftLocal - lastLeftHandLocal;
                totalMovement += leftMovement;
                lastLeftHandLocal = currentLeftLocal;
            }
        }
        else
        {
            needsLeftHandReset = true;
        }

        // Process right hand
        if (rightHandDetector.isTouchingClimbable)
        {
            if (needsRightHandReset)
            {
                lastRightHandLocal = GetLocalHandPosition(rightHandTransform);
                needsRightHandReset = false;
            }
            else
            {
                Vector3 currentRightLocal = GetLocalHandPosition(rightHandTransform);
                Vector3 rightMovement = currentRightLocal - lastRightHandLocal;
                totalMovement += rightMovement;
                lastRightHandLocal = currentRightLocal;
            }
        }
        else
        {
            needsRightHandReset = true;
        }

        // Apply movement
        if (totalMovement.magnitude > 0)
        {
            // Convert to world space and apply movement multiplier
            Vector3 worldMovement = bodyTransform.TransformDirection(totalMovement);
            Vector3 inverseMovement = -worldMovement * movementMultiplier;

            // Clamp maximum movement per frame
            if (inverseMovement.magnitude > maxMovementPerFrame)
            {
                inverseMovement = inverseMovement.normalized * maxMovementPerFrame;
            }

            // Move using MovePosition for smoother physics-based movement
            bodyRigidbody.MovePosition(bodyRigidbody.position + inverseMovement);

            // Store velocity for physics transition
            lastVelocity = inverseMovement / Time.fixedDeltaTime * velocityTransferMultiplier;
        }


        // Enerji t�ketimi
        if (totalMovement.magnitude > 0.01f)
        {
            energyDrainTimer += Time.fixedDeltaTime;

            if (totalMovement.magnitude > 0.01f && energyDrainTimer >= energyDrainCooldown)
            {
                if (playerEnergy != null)
                {
                    playerEnergy.UseEnergy(energyPerMove);

                    if (playerEnergy.IsOutOfEnergy)
                    {
                        bodyRigidbody.isKinematic = false;
                        return;
                    }
                }

                energyDrainTimer = 0f; // Reset the timer
            }

        }


    }

    private Vector3 GetLocalHandPosition(Transform handTransform)
    {
        return bodyTransform.InverseTransformPoint(handTransform.position);
    }

    private void ResetHandPositions()
    {
        lastLeftHandLocal = GetLocalHandPosition(leftHandTransform);
        lastRightHandLocal = GetLocalHandPosition(rightHandTransform);
        needsLeftHandReset = false;
        needsRightHandReset = false;
    }

    // for sticky platform
    public void SetMovementMultiplierTemporarily(float newMultiplier, float duration)
    {
        if (restoreCoroutine != null)
            StopCoroutine(restoreCoroutine);

        movementMultiplier = newMultiplier;
        restoreCoroutine = StartCoroutine(RestoreMovementMultiplier(duration));
    }

    // for sticky platform
    private IEnumerator RestoreMovementMultiplier(float delay)
    {
        yield return new WaitForSeconds(delay);
        movementMultiplier = defaultMovementMultiplier;
        restoreCoroutine = null;
    }

}