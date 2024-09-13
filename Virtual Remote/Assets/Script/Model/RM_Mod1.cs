using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RemoteInputMod1
{
    public int id;
    public Vector2 joystick;
    public bool btnUp;
    public bool btnDown;
    public bool btnRight;
    public bool btnLeft;
}

public class RM_Mod1 : RemoteManager
{
    //public int id;

    public FixedJoystick joystickRemote;
   /* public Button btnUp;
    public Button btnDown;
    public Button btnRight;
    public Button btnLeft;*/

    public ButtonBehavior btnUp;
    public ButtonBehavior btnDown;
    public ButtonBehavior btnLeft;
    public ButtonBehavior btnRight;
    public ButtonPositionTouch btnLeftRight;
    

    public RemoteInputMod1 actualInput = new RemoteInputMod1();
    // Start is called before the first frame update
    void Start()
    {
        actualInput.id = id;
        actualInput.joystick = joystickRemote.Direction;
        actualInput.btnUp = false;
        actualInput.btnDown = false;
        actualInput.btnRight = false;
        actualInput.btnLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void BtnUpClick()
    {
        btnDown.interactable = false;
        btnDown.interactable = true;
    }

    public void BtnUpPressed()
    {
        actualInput.btnUp = true;
    }

    public void BtnUpReleased()
    {
        actualInput.btnUp = false;
    }

    public void BtnDownClick()
    {
        btnUp.interactable = false;
        btnUp.interactable = true;
    }

    public void BtnDownPressed()
    {
        actualInput.btnDown = true;
    }

    public void BtnDownReleased()
    {
        actualInput.btnDown = false;
    }

    public void BtnRightClick()
    {
        btnLeft.interactable = false;
        btnLeft.interactable = true;
    }

    public void BtnRightPressed()
    {
        actualInput.btnRight = true;
    }

    public void BtnRightReleased()
    {
        actualInput.btnRight = false;
    }

    public void BtnLeftClick()
    {
        btnRight.interactable = false;
        btnRight.interactable = true;
    }

    public void BtnLeftPressed()
    {
        actualInput.btnLeft = true;
    }

    public void BtnLeftReleased()
    {
        actualInput.btnLeft = false;
    }*/


    public RemoteInputMod1 GetRemoteInput()
    {
        //actualInput.joystick = joystickRemote.Direction;
        actualInput.joystick.x = btnLeftRight.btnPressed ? btnLeftRight.value : 0;
        actualInput.joystick.y = (btnUp.btnPressed ? 1 : 0) + (btnDown.btnPressed ? -1 : 0);

        return actualInput;
    }


    public override void SetId(int new_id)
    {
        base.SetId(new_id);
        actualInput.id = new_id;
    }

    public override void ClickImage()
    {
        base.ClickImage();
    }
}
