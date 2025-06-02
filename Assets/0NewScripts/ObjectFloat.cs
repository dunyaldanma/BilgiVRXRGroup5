using UnityEngine;

public class ObjectFloat : MonoBehaviour
{
    public float amplitude = 1f;       // Height of the wave
    public float frequency = 1f;       // Speed of the wave
    private Vector3 startPosition;
    private float phaseOffset;

    void Start()
    {
        startPosition = transform.position;

        // Add a random phase offset to desynchronize the movement
        phaseOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        float sinValue = Mathf.Sin(Time.time * frequency * 2f * Mathf.PI - Mathf.PI / 2f + phaseOffset);
        float yOffset = (sinValue + 1f) / 2f * amplitude;
        transform.position = startPosition + new Vector3(0f, yOffset, 0f);
    }
}
