using UnityEngine;

public class Mag : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MagPlace"))
        {
            rb.isKinematic = true;
            transform.position = other.transform.position;
            this.transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MagPlace"))
        {
        rb.isKinematic = false;
        transform.parent = null;
        }

    }
}
