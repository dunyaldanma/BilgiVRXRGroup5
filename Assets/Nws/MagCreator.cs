using UnityEngine;

public class MagCreator : MonoBehaviour
{
    [SerializeField] GameObject mag;
    [SerializeField] GameObject magDispencer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked()
    {
        Vector3 v3 = magDispencer.transform.position;
        Instantiate(mag, v3, Quaternion.identity);
    }
}
