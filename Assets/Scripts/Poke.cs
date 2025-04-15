using UnityEngine;

public class Poke : MonoBehaviour
{

    public bool poked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(poked);
    }

    private void OnTriggerEnter(Collider other)
    {
        poked = true;
    }

    private void OnTriggerExit(Collider other)
    {
        poked = false;
    }
}
