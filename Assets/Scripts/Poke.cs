using UnityEngine;

public class Poke : MonoBehaviour
{

    public bool bluePoke = false;
    public bool greenPoke = false;
    public bool redPoke = false;
    public bool yellowPoke = false;
    public bool magPoke = false;
    public bool highDifficultyPoke = false;
    public bool lowDifficultyPoke = false;
    public bool restartPoke = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(bluePoke);
        Debug.Log(greenPoke);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("BluePoke"))
        {
            bluePoke = true;
        }
        if (gameObject.CompareTag("GreenPoke"))
        {
            greenPoke = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("BluePoke"))
        {
            bluePoke = false;
        }
        if (gameObject.CompareTag("GreenPoke"))
        {
            greenPoke = false;
        }
    }
}
