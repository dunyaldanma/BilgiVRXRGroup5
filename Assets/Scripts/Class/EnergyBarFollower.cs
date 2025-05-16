using UnityEngine;

public class EnergyBarFollower : MonoBehaviour
{
    public Vector3 offset = new Vector3(0f, -0.1f, 0.5f); // Slightly below and in front of the camera

    void LateUpdate()
    {
        if (Camera.main == null) return;

        Transform cam = Camera.main.transform;

        // Position: world-space location offset relative to the camera
        transform.position = cam.position + cam.rotation * offset;

        // Rotation: oriented to face the camera, rotating only on the Y plane
        Quaternion targetRot = Quaternion.Euler(0, cam.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
    }
}

