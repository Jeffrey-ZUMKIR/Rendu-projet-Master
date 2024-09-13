using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    Touch[] touches;
    Vector2[] positions;

    public List<ButtonBehavior> buttons = new List<ButtonBehavior>();
    public ButtonPositionTouch button2;

    // Update is called once per frame
    void Update()
    {
        touches = Input.touches;
        if (touches.Length > 0)
        {
            positions = new Vector2[touches.Length];
            for (int i = 0; i < touches.Length; i++)
            {
                positions[i] = touches[i].position;
            }

            foreach (ButtonBehavior button in buttons)
            {
                button.CheckPositionTouch(positions);
            }

            button2.CheckTouchPosition(positions);
        }
        else
        {
            foreach (ButtonBehavior button in buttons)
            {
                button.btnPressed = false;
            }
            button2.btnPressed = false;
            button2.cursor.localPosition = new Vector3(0, 0, 0);
        }
    }
}
