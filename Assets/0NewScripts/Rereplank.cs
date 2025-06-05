using UnityEngine;

public class Rereplank : MonoBehaviour
{
    [SerializeField] RePlank win1;
    private void OnTriggerEnter(Collider other)
    {
        win1.activate();
    }
}
