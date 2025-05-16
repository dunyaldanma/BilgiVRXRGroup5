using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float hiz = 100f;
    public Vector3 yon = Vector3.up;

    void Update()
    {
        transform.Rotate(yon.normalized * hiz * Time.deltaTime);
    }
}
