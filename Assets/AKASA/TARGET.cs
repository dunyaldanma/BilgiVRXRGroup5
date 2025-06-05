using UnityEngine;

public class TARGET : MonoBehaviour
{
    int x;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        x += 1;
        if (x > 10)
        {
            Destroy(gameObject);
        }
    }
}
