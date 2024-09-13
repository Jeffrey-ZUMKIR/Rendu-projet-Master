using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    private RectTransform buttonRect;
    public Image buttonImage;

    public bool btnPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        buttonRect = GetComponent<RectTransform>();
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (btnPressed)
            buttonImage.color = new Color(0.8f, 0.8f, 0.8f);
        else
            buttonImage.color = new Color(1, 1, 1);
    }

    public void CheckPositionTouch(Vector2[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(buttonRect, positions[i], null, out Vector2 localPoint);

            // Vérifiez si la position locale est à l'intérieur des limites du RectTransform
            if (buttonRect.rect.Contains(localPoint))
            {
                IsPressed();
                break;
            }
            else if(i == positions.Length - 1)
            {
                btnPressed = false;
            }
        }
    }

    public void IsPressed()
    {
        btnPressed = true;
    }
}
