using UnityEngine;

public class PlankPlacement : MonoBehaviour
{
    [SerializeField] Transform pos;
    [SerializeField] GameObject grab;
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
        if (other.CompareTag("Plank"))
        {
            other.transform.position = pos.position;
            other.transform.rotation = pos.rotation;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            other.gameObject.tag = "Climbable";
            grab.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
