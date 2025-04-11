using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CLOWN74 : MonoBehaviour
{
    LayerMask layerMask;
    [SerializeField] GameObject bullet;
    bool onTarget;
    float miliseconds;
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
            onTarget = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * -1) * 1000, Color.white);
            Debug.Log("Did not Hit");
            onTarget = false;
        }
    }

    void Update()
    {
        miliseconds += Time.deltaTime * 1000;
        Debug.Log(miliseconds);
        if (miliseconds > 100)
        {
            miliseconds -= 100;
            if (onTarget)
            {
                GameObject projectile;
                projectile = Instantiate(bullet, transform.position, transform.rotation);
                projectile.GetComponent<Rigidbody>().linearVelocity = transform.TransformDirection(Vector3.forward * -100.0f);
            }
        }
    }
}
