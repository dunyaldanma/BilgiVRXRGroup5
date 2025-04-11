using UnityEngine;

public class Raycastinger : MonoBehaviour
{

    [SerializeField] GameObject particle;
    
    void Update()
    {
        // Define the starting point and direction of the ray
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        // Perform the raycast
        bool hitSomething = Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo);

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        // Output the result
        if (hitInfo.collider.CompareTag("Target"))
        {
            Debug.Log("Raycast hit: " + hitInfo.collider.name);
            particle.SetActive(true);
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
            particle.SetActive(false);
        }
    }
}




/*
RaycastHit raycastHit;
if(Physicas.Raycast(from, to , out raycastHit, maxDistance)){
Collider = raycastHitCollider = raycastHit.collider;
    if (raycastHitCollider.gameObject.tag == "Your tag here"){
    //do stuff
    }
}
 */