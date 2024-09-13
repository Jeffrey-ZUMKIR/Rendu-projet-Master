using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RemoteInput
{
    public int id;
    public Vector2 joystick;
    public bool btnA;
    public bool btnB;
}
public class RemoteManager : MonoBehaviour
{
    public int id;

    public TextMeshProUGUI namePlayer;

    /*public FixedJoystick joystickRemote;
    public Button btnA;
    public Button btnB;

    public RemoteInput actualInput = new RemoteInput();*/

    // Start is called before the first frame update
    void Start()
    {
        /*Input.gyro.enabled = true;
        actualInput.id = id;
        actualInput.joystick = joystickRemote.Direction;
        actualInput.btnA = false;
        actualInput.btnB = false;*/
    }

    private void Update()
    {
        
        //Input.gyro
    }

    /*protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
    }*/
    
    /*public void BtnAPressed()
    {
        actualInput.btnA = true;
    }

    public void BtnAReleased()
    {
        actualInput.btnA = false;
    }

    public void BtnBPressed()
    {
        actualInput.btnB = true;
    }

    public void BtnBReleased()
    {
        actualInput.btnB = false;
    }

    public RemoteInput GetRemoteInput()
    {
        actualInput.joystick = joystickRemote.Direction;

        return actualInput;
    }

    */
    public virtual void SetId(int new_id)
    {
        id = new_id;
        //namePlayer.text = "J" + id.ToString();
    }

    public virtual void SetName(int idPlayer)
    {
        namePlayer.text = "J" + idPlayer.ToString();
    }
    
    public virtual void ClickImage()
    {
        Debug.Log("Deconnexion");
        //GameObject.Find("Select&Remote").GetComponent<AnimationManager>().ChangeState(false);
        
        GameObject.Find("WS_Manager").GetComponent<WS_ClientStandar>().SendUsedState(id, false);
        GameObject.Find("WS_Manager").GetComponent<WS_ClientStandar>().ReloadVBots();
        id = 0;
    }
}
