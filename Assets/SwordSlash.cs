using UnityEditor.SceneManagement;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    [SerializeField] CharacterJoint cJ;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cJ = this.GetComponent<CharacterJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            Animator animator = this.transform.root.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }
            
            cJ.breakForce = 1;
        }
    }
    private void OnJointBreak(float breakForce)
    {
        this.transform.parent = null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Animator animator = this.transform.root.GetComponent<Animator>();
            if (OVRInput.Get(OVRInput.Button.Back)) 
            {
                if (animator != null)
                {
                    animator.enabled = false;
                }

            }
        }
    }
}
