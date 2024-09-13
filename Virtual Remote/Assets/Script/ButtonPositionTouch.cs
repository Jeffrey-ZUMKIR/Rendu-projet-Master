using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonPositionTouch : MonoBehaviour
{
    private RectTransform buttonRect;
    public bool btnPressed = false;
    public Transform cursor;
    private float maxLeft, maxRight;
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        buttonRect = GetComponent<RectTransform>();

        maxLeft = ((buttonRect.rect.width / 2) - (buttonRect.rect.width / 4)) * (-1);
        maxRight = (buttonRect.rect.width / 2) - (buttonRect.rect.width / 4);
    }

    // Update is called once per frame
    void Update()
    {
        value = Remap(cursor.localPosition.x, maxLeft, maxRight, -1, 1);
    }

    public void CheckTouchPosition(Vector2[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(buttonRect, positions[i], null, out Vector2 localPoint);

            // Vérifiez si la position locale est à l'intérieur des limites du RectTransform
            if (buttonRect.rect.Contains(localPoint))
            {
                btnPressed = true;
                // Calculez la position relative par rapport au centre de l'image
                float distanceFromCenterX = localPoint.x;

                if (distanceFromCenterX < 0)
                {
                    // Le touché est à gauche du centre de l'image
                    Debug.Log("Left");
                }
                else if (distanceFromCenterX > 0)
                {
                    // Le touché est à droite du centre de l'image
                    Debug.Log("Right");
                }
                else
                {
                    // Le touché est au centre de l'image
                    Debug.Log("Center");
                }
                cursor.localPosition = new Vector3(Mathf.Clamp(localPoint.x, maxLeft, maxRight), 0, 0);
                break;
            }
            else if (i == positions.Length - 1)
            {
                cursor.localPosition = new Vector3(0, 0, 0);
                btnPressed = false;
            }
        }
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    
}
