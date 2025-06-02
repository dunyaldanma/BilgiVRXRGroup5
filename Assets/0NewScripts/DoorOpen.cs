using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool openToRight = true;        // Toggle to open right or left
    public float slideDistance = 2f;       // How far the door moves
    public float duration = 1f;            // Time it takes to open/close
    public AnimationCurve easeCurve;       // Curve for ease in/out

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        closedPosition = transform.position;

        // Calculate open position based on direction
        float direction = openToRight ? 1f : -1f;
        openPosition = closedPosition + new Vector3(direction * slideDistance, 0f, 0f);

        // Default to ease in/out if no curve is set
        if (easeCurve == null || easeCurve.length == 0)
        {
            easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        }
    }

    public void ToggleDoor()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveDoor(isOpen ? openPosition : closedPosition, isOpen ? closedPosition : openPosition));
            isOpen = !isOpen;
        }
    }

    private System.Collections.IEnumerator MoveDoor(Vector3 from, Vector3 to)
    {
        isMoving = true;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float easedT = easeCurve.Evaluate(t);
            transform.position = Vector3.Lerp(from, to, easedT);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;
        isMoving = false;
    }
}
