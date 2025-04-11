using UnityEngine;

public class CLOWN74 : MonoBehaviour
{
    LayerMask layerMask;
    void Awake()
    {
        layerMask = LayerMask.GetMask("Target");
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward * -1), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * -1) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * -1) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
