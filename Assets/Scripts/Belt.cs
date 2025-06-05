using UnityEngine;

public class Belt : MonoBehaviour
{
    public float size = 0.1f;
    [SerializeField] Transform plankPos, gunPos, magPos, katPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            //rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            other.transform.parent = gunPos.transform;
            other.transform.position = gunPos.position;
            other.transform.rotation = gunPos.rotation;

        }
        else if (other.gameObject.GetComponent("Plank")!= null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            //rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            other.transform.localScale = new Vector3(size,size,size);
            other.transform.position = plankPos.position;
            other.transform.parent = this.transform;
        }
        else if (other.gameObject.GetComponent("Mag") != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            //rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            other.transform.position = magPos.position;
            other.transform.parent = this.transform;
        }
        else if (other.CompareTag("Sword"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            //rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            other.transform.parent = katPos.transform;
            other.transform.position = katPos.position;
            other.transform.rotation = katPos.rotation;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            //rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
            other.transform.parent = null;
            rb = null;


        }
        else if (other.gameObject.GetComponent("Plank") != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            //rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
            other.transform.localScale = new Vector3(1,1,1);
            
            other.transform.parent = null;
            rb = null;
        }
        else if (other.CompareTag("Mag"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            //rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
            other.transform.parent = null;
            rb = null;


        }
        else if (other.CompareTag("Sword"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            //rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
            other.transform.parent = null;
            rb = null;


        }
    }
}
