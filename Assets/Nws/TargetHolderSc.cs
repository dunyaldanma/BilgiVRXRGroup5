using System.Collections;
using UnityEngine;

public class TargetHolderSc : MonoBehaviour
{
    GameObject target;
    void Start()
    {
        target = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Move1()
    {
        target.GetComponent<Animator>().Play("TargetBack");
        yield return new WaitForSeconds(2);
    }

    public IEnumerator Move2() 
    {
        target.GetComponent<Animator>().Play("TargetForward");
        yield return new WaitForSeconds(2);
    }

    public void Move1Start() 
    {
        StartCoroutine(Move1());
    }
    public void Move2Start()
    {
        StartCoroutine(Move2());
    }
}
