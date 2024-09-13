using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadVBotsManager : MonoBehaviour
{
    public void ReloadVBots()
    {
        GetComponent<Animator>().Play("anim1");
    }
}
