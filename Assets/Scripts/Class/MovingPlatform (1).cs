using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.forward; // movement direction
    public float moveDistance = 2f;                 // total travel distance
    public float moveSpeed = 1f;                    // speed

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = startPosition + moveDirection.normalized * offset;
    }
}
