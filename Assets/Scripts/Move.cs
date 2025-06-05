using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Poke Command0;
    [SerializeField] Poke Command1;
    [SerializeField] Poke Command2;
    [SerializeField] Poke Command3;
    float speedo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speedo = Time.deltaTime;
        if (Command0.Button0)
        {
            //transform.position += new Vector3(0, 0, 10 * speedo);
            rb.AddRelativeForce(0,0,2000 * speedo);
        }
        if (Command1.Button1)
        {
            //transform.AddTorque(0, -50 * speedo, 0);
            rb.AddTorque(0, -300 * speedo, 0);
        }
        if (Command2.Button2)
        {
            //transform.position += new Vector3(0, 0, -10 * speedo);
            rb.AddRelativeForce(0, 0, -2000 * speedo);
        }
        if (Command3.Button3)
        {
            //transform.Rotate(0, 50 * speedo, 0);
            rb.AddTorque(0, 300 * speedo, 0);
        }
    }
}
