using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using TMPro;

public class RemoteInputMod2
{
    public int id;
    public Vector2 joystick;
    //public Vector3 rotation;
    //public Quaternion rotQuat;
    public bool btnA;
}

public class RM_Mod2 : RemoteManager
{
    /*[DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void GetGyroscopeRotation(float x, float y, float z);*/

    public Button btnA;
    public FixedJoystick joystickRemote;

    public RemoteInputMod2 actualInput = new RemoteInputMod2();

    /*public float Z;
    public AnimationCurve courbeZ;*/

    //Vector3 rotationData;

    //public TextMeshProUGUI title;
    // Start is called before the first frame update
    void Start()
    {
        //Input.gyro.enabled = true;
        actualInput.id = id;

        actualInput.btnA = false;
        //CalibrateAngle();

        //InitGyroscope();

    }

    private void Update()
    {
        /*actualInput.rotation = new Vector3(Mathf.Round(-NormalizeToRange(Input.gyro.attitude.eulerAngles.y) * 100)/100,
            Mathf.Round(- NormalizeToRange(NormalizeAngle(Input.gyro.attitude.eulerAngles.z, Z)) * 100)/100,
            Mathf.Round(NormalizeToRange(Input.gyro.attitude.eulerAngles.x) * 100)/100);

        actualInput.rotQuat = Input.gyro.attitude;*/

        /*actualInput.rotation = new Vector3(ChangeMaxValue(Mathf.Round(Input.gyro.attitude.eulerAngles.x)),
            ChangeMaxValue(Mathf.Round(Input.gyro.attitude.eulerAngles.y)),
            ChangeMaxValue(Mathf.Round(NormalizeAngle(Input.gyro.attitude.eulerAngles.z, Z))));*/
        
    }

    protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;
        GUI.color = Color.red;

        /*GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);*/
        //GUILayout.Label("euler angle: " + Input.gyro.attitude.eulerAngles);

        /*GUILayout.Label("Rotation X: " + rotationData.x);
        GUILayout.Label("Rotation Y: " + rotationData.y);
        GUILayout.Label("Rotation Z: " + rotationData.z);*/
    }

    public void BtnAPressed()
    {
        actualInput.btnA = !actualInput.btnA;
    }

    public RemoteInputMod2 GetRemoteInput()
    {
        actualInput.joystick = joystickRemote.Direction;
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

    /*public void CalibrateAngle()
    {
        Z = Mathf.Round(Input.gyro.attitude.eulerAngles.z);
    }*/

    float NormalizeAngle(float angle, float origin)
    {
        // Soustraire l'angle d'origine
        float normalized = angle - origin;

        // Assurer que l'angle reste dans la plage [0, 360]
        normalized = (normalized + 360) % 360;

        return normalized;
    }

    float NormalizeToRange(float value)
    {
        if (value > 180)
        {
            value -= 360;
        }
        value = (value / 180);

        return value;
    }

    float ChangeMaxValue(float value)
    {
        if (value > 180)
        {
            value -= 360;
        }

        return value;
    }

    /*private void InitGyroscope()
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
            initGyroscope();
    #endif
    }

    private Vector3 GetRotationData()
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
            var rotationData = getRotationData();
            return new Vector3(rotationData.x, rotationData.y, rotationData.z);
    #else
            return Vector3.zero;
    #endif
    }

    #if UNITY_WEBGL && !UNITY_EDITOR
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void initGyroscope();

        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern Vector3 getRotationData();
    #endif*/
}
