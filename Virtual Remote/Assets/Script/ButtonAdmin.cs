using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAdmin : MonoBehaviour
{
    int nbClick = 0;

    public GameObject AdminPage;
    public GameObject SelectRemote;

    public void Clicked()
    {
        nbClick++;
        if(nbClick >= 10)
        {
            Debug.Log("click");
            AdminPage.SetActive(true);
            SelectRemote.SetActive(false);
        }
    }
}
