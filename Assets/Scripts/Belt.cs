using UnityEngine;

public class Belt : MonoBehaviour
{
    public float size = 0.1f;
    [SerializeField] Transform plankPos;
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
            rb.isKinematic = true;
            other.transform.parent = this.transform;

        }
        else if (other.gameObject.GetComponent("Plank")!= null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            other.transform.localScale = new Vector3(size,size,size);
            other.transform.position = plankPos.position;
            other.transform.parent = this.transform;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            other.transform.parent = null;

        }
        else if (other.gameObject.GetComponent("Plank") != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            other.transform.localScale = new Vector3(1,1,1);
            
            other.transform.parent = null;
        }
    }
}
