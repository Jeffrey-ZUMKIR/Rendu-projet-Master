using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBotsInfo : MonoBehaviour
{
    public int id;
    public bool used;

    public void ClickImage()
    {
        //GameObject.Find("Select&Remote").GetComponent<AnimationManager>().ChangeState(true);
        GameObject.Find("WS_Manager").GetComponent<WS_ClientStandar>().SendUsedState(id, true);
        //GameObject.Find("Remote").GetComponent<RemoteManager>().SetId(id);
        /*GameObject.Find("Remote").SetActive(true);
        GameObject.Find("SelectionZone").SetActive(false);*/
        
        
    }
}
