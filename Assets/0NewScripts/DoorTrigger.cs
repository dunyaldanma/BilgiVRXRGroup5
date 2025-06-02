using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] DoorOpen door1;
    [SerializeField] DoorOpen door2;
    private void OnTriggerEnter(Collider other)
    {
        door1.ToggleDoor();
        door2.ToggleDoor();
    }
}
