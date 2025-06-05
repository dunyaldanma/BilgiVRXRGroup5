using UnityEngine;

public class RePlank : MonoBehaviour
{
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
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            other.gameObject.tag = "Untagged";
            grab.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
