using UnityEngine;
using UnityEngine.UIElements;

public class Katanaa : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider mk;
    public Vector3 speedL;
    public float realSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Update()
    {
        rb.gameObject.transform.position = this.transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        speedL = rb.GetRelativePointVelocity(Vector3.zero);
        realSpeed = Mathf.Sqrt((speedL.x * speedL.x) + (speedL.y * speedL.y) + (speedL.z * speedL.z));
    }
}
