using UnityEngine;

public class PlankUnplacement : MonoBehaviour
{
    [SerializeField] GameObject grab, plank;

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
        if (other.CompareTag("Player"))
        {
            plank.tag = "Plank";
            Rigidbody rb = plank.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            grab.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
