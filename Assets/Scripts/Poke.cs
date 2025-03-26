using UnityEngine;

public class Poke : MonoBehaviour
{

    public bool Button0 = false;
    public bool Button1 = false;
    public bool Button2 = false;
    public bool Button3 = false;

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
        if (gameObject.tag == "Button0")
        {
            Debug.Log("BUTTON 0 ENTER");
            Button0 = true;
        }
        
        if (gameObject.tag == "Button1")
        {
            Debug.Log("BUTTON 1 ENTER");
            Button1 = true;
        }
        
        if (gameObject.tag == "Button2")
        {
            Debug.Log("BUTTON 2 ENTER");
            Button2 = true;
        }
        
        if (gameObject.tag == "Button3")
        {
            Debug.Log("BUTTON 3 ENTER");
            Button3 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "Button0")
        {
            Debug.Log("BUTTON 0 RELEASE");
            Button0 = false;
        }

        if (gameObject.tag == "Button1")
        {
            Debug.Log("BUTTON 1 RELEASE");
            Button1 = false;
        }

        if (gameObject.tag == "Button2")
        {
            Debug.Log("BUTTON 2 RELEASE");
            Button2 = false;
        }

        if (gameObject.tag == "Button3")
        {
            Debug.Log("BUTTON 3 RELEASE");
            Button3 = false;
        }
    }
}
