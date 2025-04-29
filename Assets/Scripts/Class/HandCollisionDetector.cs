using UnityEngine;

public class HandCollisionDetector : MonoBehaviour
{
    [Tooltip("Tag that identifies surfaces the player can climb on")]
    public string climbableSurfaceTag = "Climbable";

    [Tooltip("Which controller this hand represents")]
    public OVRInput.Controller controller;

    private bool isTouchingClimbableSurface;
    public bool isTouchingClimbable 
    { 
        get 
        { 
            // Only return true if both touching surface AND grip button is pressed
            return isTouchingClimbableSurface && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controller);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(climbableSurfaceTag))
        {
            isTouchingClimbableSurface = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(climbableSurfaceTag))
        {
            isTouchingClimbableSurface = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(climbableSurfaceTag))
        {
            isTouchingClimbableSurface = true;
        }
    }
} 