using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hp = 100f;
    //public GameObject gameover;
    public TextMeshProUGUI text;

    void Start()
    {
        text.text = hp.ToString();
        //DontDestroyOnLoad(gameObject);
    }
    public void GetDamage(float damage)
    {
        hp -= damage;
        text.text = hp.ToString();
        if (hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }

    /*public void HpUp (float up)
    {
        hp += up;
        text.text = hp.ToString();
    }*/

    /*void OnParticleCollision(GameObject other)
    {
        ParticleHit();
    }

    void ParticleHit()
    {
        hp -= 5;
        text.text = hp.ToString();
        if (hp <= 0)
        {
            gameover.SetActive(true);
        }
    }*/
}
