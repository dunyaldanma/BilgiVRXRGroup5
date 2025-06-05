using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 5f;
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
        Animator animator = this.transform.root.GetComponent<Animator>();
        Animation clip = animator.GetComponent<Animation>();
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Z_Attack")) 
        {
            if (animator != null)
            {
                if (other.CompareTag("Player"))
                {
                    PlayerHealth pH = other.GetComponent<PlayerHealth>();
                    if (pH != null) { pH.GetDamage(damage);}
                
                }
            }
            else
            {
                return;
            }
        }
        
    }
}
