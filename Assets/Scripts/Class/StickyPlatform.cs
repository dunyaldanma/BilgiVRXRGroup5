using UnityEngine;


public class StickyPlatform : MonoBehaviour
{
    public HandLocomotion locomotion; // manually assign in scene
    public float stickiness = 0.3f;
    public float restoreDelay = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (locomotion != null)
            {
                locomotion.SetMovementMultiplierTemporarily(stickiness, restoreDelay);
            }
        }
    }

}
