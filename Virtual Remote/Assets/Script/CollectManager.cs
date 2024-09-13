using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public void StartAnim()
    {
        GetComponent<Animator>().SetTrigger("Play");
        GetComponent<AudioSource>().Play();
    }
}
