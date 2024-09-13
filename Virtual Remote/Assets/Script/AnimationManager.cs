using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public void ChangeState(bool state)
    {
        GetComponent<Animator>().SetBool("showingRemote", state);
        //GameObject.Find("UDP_Manager").GetComponent<UDP_Client>().remoteOn = state;
        GameObject.Find("WS_Manager").GetComponent<WS_ClientStandar>().remoteOn = state;
    }
}
