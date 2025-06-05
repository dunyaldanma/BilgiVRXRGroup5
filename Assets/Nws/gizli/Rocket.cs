using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    bool bruh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bruh)
        {
            rb.AddForce(0, 0, 100);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")&&rb.isKinematic)
        {
            bruh = true;
        }
    }
}
