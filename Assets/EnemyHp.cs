
using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyHp : MonoBehaviour
{

    [SerializeField]
    AudioClip Explosion;
    public float Health = 50f;
    [SerializeField] ParticleSystem failureEffect;

    [SerializeField] int kill = 50;
    [SerializeField] Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        
        if (Health <= 0)
        {
            Invoke("Die", 1);

        }
    }

    private void Die()
    {

        anim.enabled = false;
        CapsuleCollider cC = this.GetComponent<CapsuleCollider>();
        Rigidbody rbC = this.GetComponent<Rigidbody>();
        rbC.isKinematic = true;
        cC.enabled = false;
       

    }
}
