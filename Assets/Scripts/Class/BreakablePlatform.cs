using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public GameObject wholeMesh;
    public GameObject fracturedParent;
    public float breakDelay = 2f;
    public float pieceLifetime = 5f; // parts will disappear after 5 seconds
    private bool isBreaking = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision occured: " + collision.gameObject.name);

        if (isBreaking) return;

        if (!isBreaking)
        {
            isBreaking = true;
            Invoke(nameof(Break), breakDelay);
        }
    }

    void Break()
    {
        wholeMesh.SetActive(false);
        fracturedParent.SetActive(true);

        foreach (Rigidbody rb in fracturedParent.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;

            // light explosion + rotation
            rb.AddExplosionForce(150f, transform.position, 1f);
            rb.AddTorque(Random.onUnitSphere * 50f, ForceMode.Impulse);

            // automatic destruction
            Destroy(rb.gameObject, pieceLifetime);
        }
    }
}
