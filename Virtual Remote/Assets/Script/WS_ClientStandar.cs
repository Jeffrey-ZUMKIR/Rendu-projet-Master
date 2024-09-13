using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using NativeWebSocket;
using TMPro;
using UnityEngine.Networking;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Reflection;
using System.IO;

[System.Serializable]
public class VBotsState
{
    public int id;
    public bool used;
}

public class WS_ClientStandar : MonoBehaviour
{
    WebSocket ws;
    public string ipServer;
    public string path = "http://192.168.0.100/Image/";

    public ListJson ListVBots;

    public List<GameObject> vbotsObj = new List<GameObject>();
    List<VBotsInfo> vbotsInfo = new List<VBotsInfo>();

    public GameObject parentList;
    public GameObject vbotsImage;

    bool canSpawn = true;
    bool checkState = false;

    //public GameObject panelClickable;

    public InfiniteScroll carrousel;

    public ReloadVBotsManager reloadVbot;

    VBotsState newState;

    //Remote
    public RemoteManager remoteM;

    public bool remoteOn = false;

    public GameObject Remote;
    public GameObject SelectionZone;
    public GameObject Btn_Deco;

    // Start is called before the first frame update
    async void Start()
    {
        ws = new WebSocket("ws://" + ipServer + ":8080");

        ws.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        ws.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        ws.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        ws.OnMessage += OnMessageReceived;

        // Keep sending messages at every 0.3s
        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        // waiting for messages
        await ws.Connect();

        //await ws.SendText("02/null");
    }

    // Update is called once per frame
    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            ws.DispatchMessageQueue();
        #endif
        //Premier spawn des VBots sélectionnables
        if (canSpawn && ListVBots.items.Length != 0)
        {
            Debug.Log("SpawnObject");
            canSpawn = false;
            StartCoroutine(DestroyVBotsImage());
        }

        //Appel à une update de l'état des personnages
        if (checkState)
        {
            checkState = false;
            CheckUsed();
        }

        if (remoteOn)
        {
            try
            {
                if (remoteM is RM_Mod1)
                {
                    RM_Mod1 temp = (RM_Mod1)remoteM;
                    ws.SendText("04/" + JsonUtility.ToJson((temp.GetRemoteInput())));
                }
                else if (remoteM is RM_Mod2)
                {
                    RM_Mod2 temp = (RM_Mod2)remoteM;
                    ws.SendText("04/" + JsonUtility.ToJson((temp.GetRemoteInput())));
                }


            }
            catch (Exception e)
            {
                print("Exception thrown " + e.Message);
            }
        }
    }

    async void SendWebSocketMessage()
    {
        if (ws.State == WebSocketState.Open)
        {
            // Sending plain text
            await ws.SendText("02/null");
            CancelInvoke();
        }
    }

    private void OnMessageReceived(byte[] msg)
    {
        string response = System.Text.Encoding.UTF8.GetString(msg);
        Debug.Log(response);
        string[] responseSplit = response.Split("/");

        //Reçois l'info de tout les VBots
        if (responseSplit[0] == "vbotInfo")
        {
            if (responseSplit[1] != "undefined")
            {
                ListVBots = JsonConvert.DeserializeObject<ListJson>(responseSplit[1]);
                Debug.Log(responseSplit[1]);
                canSpawn = true;
            }
            
        }
        //Reçois la nouvelle information d'un VBot
        else if (responseSplit[0] == "newVbotState")
        {
            
            newState = JsonUtility.FromJson<VBotsState>(responseSplit[1]);
            Debug.Log("newState");

            foreach (VBotsInfo vbot in vbotsInfo)
            {
                if (vbot.id == newState.id)
                {
                    vbot.used = newState.used;
                    checkState = true;
                }

            }
        }
        else if (responseSplit[0] == "idUserGetRing")
        {
            if (FindObjectOfType<RemoteManager>().id.ToString() == responseSplit[1])
            {
                FindObjectOfType<CollectManager>().StartAnim();
            }
            
        }
        else if (responseSplit[0] == "getControlAuthorized")
        {
            Remote.SetActive(true);
            SelectionZone.SetActive(false);
            remoteOn = true;
        }
        else if (responseSplit[0] == "getControlUnauthorized")
        {
            remoteM.SetId(0);
        }
        else if (responseSplit[0] == "gameState")
        {
            if (responseSplit[1] == "True")
            {
                Debug.Log("Début de la partie");
                string temp = responseSplit[2].Replace("[", "");
                temp = temp.Replace("]", "");
                string[] tempSplit = temp.Split(',');

                int index = Array.IndexOf(tempSplit, remoteM.id.ToString());

                remoteM.SetName(index + 1);
                //Bloqué le bouton de déconnexion
                Btn_Deco.SetActive(false);
            }
            else if (responseSplit[1] == "False")
            {
                Debug.Log("Fin de la partie");
                Remote.SetActive(false);
                SelectionZone.SetActive(true);

                remoteM.SetName(0);

                remoteOn = false;
                //Débloqué le bouton de déconnexion
                Btn_Deco.SetActive(true);
            }
        }
        
    }
    private IEnumerator DestroyVBotsImage()
    {
        foreach (Transform child in parentList.transform)
        {
            Destroy(child.gameObject);
        }
        vbotsObj = new List<GameObject>();
        vbotsInfo = new List<VBotsInfo>();

        carrousel.ClearList();

        StartCoroutine(SpawnVBotsImageCor());
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator SpawnVBotsImageCor()
    {
        foreach (ListJson.Item vbot in ListVBots.items)
        {
            GameObject vbotInstantiate = Instantiate(vbotsImage, transform.position, transform.rotation);
            vbotInstantiate.name = "VBot" + vbot.id;
            vbotInstantiate.transform.SetParent(parentList.transform);
            vbotInstantiate.GetComponent<VBotsInfo>().id = vbot.id;
            vbotInstantiate.GetComponent<VBotsInfo>().name = vbot.name;
            vbotInstantiate.GetComponent<VBotsInfo>().used = vbot.used;
            vbotInstantiate.transform.localScale = new Vector3(1, 1, 1);


            
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(path + vbot.name + ".png");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D textTemp = new Texture2D(421, 297);
                textTemp = ((DownloadHandlerTexture)www.downloadHandler).texture;
                vbotInstantiate.transform.Find("VBotScan").GetComponent<Image>().sprite = Sprite.Create(textTemp, new Rect(0, 0, textTemp.width, textTemp.height), Vector2.zero); ;
            }
            

            if (vbot.used)
            {
                vbotInstantiate.transform.Find("VBotScan").GetComponent<Image>().color = new Color(121, 121, 121);
                vbotInstantiate.transform.Find("VBotScan").GetComponent<Button>().interactable = false;
            }
            vbotsObj.Add(vbotInstantiate);
            vbotsInfo.Add(vbotInstantiate.GetComponent<VBotsInfo>());

            yield return new WaitForSeconds(0.1f);
        }

        carrousel.UpdateList(vbotsObj);
    }

    private void CheckUsed()
    {
        VBotsInfo[] VBotObj = GameObject.FindObjectsOfType<VBotsInfo>();

        // Parcourir les objets et faire quelque chose avec chacun
        foreach (VBotsInfo vbot in VBotObj)
        {
            if (vbot.id == newState.id)
            {
                vbot.used = newState.used;

                if (vbot.used)
                {
                    vbot.transform.Find("VBotScan").GetComponent<Image>().color = new Color(121, 121, 121);
                    vbot.transform.Find("VBotScan").GetComponent<Button>().interactable = false;
                }
                else
                {
                    vbot.transform.Find("VBotScan").GetComponent<Image>().color = new Color(255, 255, 255);
                    vbot.transform.Find("VBotScan").GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    public void SendUsedState(int id, bool used)
    {
        VBotsState newState = new VBotsState();
        newState.id = id;
        newState.used = used;

        string newData = JsonUtility.ToJson(newState);

        ws.SendText("03/" + newData);

        if(used == true)
        {
            //Remote.SetActive(true);
            remoteM.SetId(id);
            //SelectionZone.SetActive(false);
        }
        else
        {
            Remote.SetActive(false);
            SelectionZone.SetActive(true);
            remoteOn = false;
        }

        
    }

    public void ReloadVBots()
    {
        ws.SendText("02/null");
    }

    private void OnApplicationQuit()
    {
        ws.Close();
    }

    public void SendData(string data)
    {
        ws.SendText(data);
    }
    
}
