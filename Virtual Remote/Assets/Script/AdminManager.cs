using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdminManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject adminButtons;
    public GameObject connexion;
    public GameObject resetForm;
    public TMP_InputField resetInput;
    public GameObject keyboard;

    public WS_ClientStandar wsClient;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetKeyboardForConnexion()
    {
        keyboard.SetActive(true);
        keyboard.GetComponent<KeyboardManager>().inputField = inputField;
    }

    public void SetKeyboardForReset()
    {
        keyboard.SetActive(true);
        keyboard.GetComponent<KeyboardManager>().inputField = resetInput;
    }

    public void HideKeyboard()
    {
        keyboard.SetActive(false);
    }

    public void CheckLogin()
    {
        if(inputField.text == "100hgodot?")
        {
            Debug.Log("Login success");
            adminButtons.SetActive(true);
            connexion.SetActive(false);
        }
        else
        {
            Debug.Log("Login failed");
        }
    }

    public void ChangeCam()
    {
        wsClient.SendData("09/changeCam");
    }

    public void ShowCam()
    {
        wsClient.SendData("09/showCam");
    }

    public void ResetForm()
    {
        if(resetForm.activeSelf)
        {
            resetForm.SetActive(false);
        }
        else
        {
            resetForm.SetActive(true);
        }
    }

    public void ValidateResetForm()
    {
        if(resetInput.text == "reset")
        {
            wsClient.SendData("08/reset");
            resetForm.SetActive(false);
        }
    }


}
