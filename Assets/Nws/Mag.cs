using UnityEngine;

public class Mag : MonoBehaviour
{
    Rigidbody rb;
    MagBulletCount mbc;
    CLOWN74 cl;
    GameObject clown;
    bool A = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mbc = GetComponent<MagBulletCount>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MagPlace"))
        {
            A = true;
            rb.isKinematic = true;
            mbc.enabled = true;
            transform.position = other.transform.position;
            this.transform.parent = other.transform;
            this.transform.rotation = other.transform.rotation;
            cl = GetComponentInParent<CLOWN74>();
            cl.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MagPlace"))
        {
           
            mbc.enabled = false;
            if (mbc.enabled == false && rb.isKinematic == false)
            {
                cl.enabled = false;
                cl = null;
            }
            A = false;
            rb.isKinematic = false;
            transform.parent = null;
        }

    }
}
