using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultySelect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum ButtonAction {Move1,Move2};
    public ButtonAction action;

    public TargetHolderSc tsc;
    private bool isheld = false;


    private void FixedUpdate()
    {
        isheld = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (!isheld || tsc == null) return;

        switch(action)
        {
            case ButtonAction.Move1:
                tsc.Move1Start();
                break;
            case ButtonAction.Move2:
                tsc.Move2Start();
                break;
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isheld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isheld = false;
    }

    public void ButtonPressed()
    {
        isheld = true;
    }

}
