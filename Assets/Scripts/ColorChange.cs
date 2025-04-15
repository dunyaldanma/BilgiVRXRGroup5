using UnityEngine;
using UnityEngine.UI;


public class ColorChange : MonoBehaviour
{
    public Material targetMaterial; // Bu Gun_MAT olacak

    public void ChangeColorFromButton(Button clickedButton)
    {
        Color colorToApply = clickedButton.colors.normalColor;


        targetMaterial.SetColor("_BaseColor", colorToApply);
    }
}
