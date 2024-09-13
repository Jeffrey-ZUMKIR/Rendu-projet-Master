using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public RectTransform contentPanelTransform;
    public HorizontalLayoutGroup HLG;
    public AudioSource audioSource;

    public List<RectTransform> ItemList = new List<RectTransform>();

    Vector2 oldVelocity;
    bool isUpdated;
    // Start is called before the first frame update
    void Start()
    {
        ItemList = new List<RectTransform>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUpdated)
        {
            isUpdated = false;
            scrollRect.velocity = oldVelocity;
        }
        //Gérer le mouvement du carrousel
        if(ItemList.Count != 0)
        {
            if (contentPanelTransform.localPosition.x > 0)
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scrollRect.velocity;
                contentPanelTransform.localPosition -= new Vector3(ItemList.Count * (ItemList[0].rect.width + HLG.spacing), 0, 0);
                isUpdated = true;
            }

            if (contentPanelTransform.localPosition.x < 0 - (ItemList.Count * (ItemList[0].rect.width + HLG.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scrollRect.velocity;
                contentPanelTransform.localPosition += new Vector3(ItemList.Count * (ItemList[0].rect.width + HLG.spacing), 0, 0);
                isUpdated = true;
            }
        }
    }

    //Initialiser les éléments du carrousel
    public void Init()
    {
        isUpdated = false;
        oldVelocity = Vector2.zero;
        if(ItemList.Count != 0)
        {
            int itemsToAdd = Mathf.CeilToInt(viewPortTransform.rect.width / (ItemList[0].rect.width + HLG.spacing));

            for (int i = 0; i < itemsToAdd; i++)
            {
                RectTransform RT = Instantiate(ItemList[i % ItemList.Count], contentPanelTransform);
                RT.SetAsLastSibling();
            }

            for (int i = 0; i < itemsToAdd; i++)
            {
                int num = ItemList.Count - i - 1;
                while (num < 0)
                {
                    num += ItemList.Count;
                }
                RectTransform RT = Instantiate(ItemList[num], contentPanelTransform);
                RT.SetAsFirstSibling();
            }

            contentPanelTransform.localPosition = new Vector3((0 - (ItemList[0].rect.width + HLG.spacing) * itemsToAdd) + viewPortTransform.rect.width / 2 - ItemList[0].rect.width / 2,
                contentPanelTransform.localPosition.y,
                contentPanelTransform.localPosition.z);
        }
        
    }

    //Vider la liste
    public void ClearList()
    {
        ItemList = new List<RectTransform>();
    }

    //Mettre à jour la liste des éléments 
    public void UpdateList(List<GameObject> vbotObj)
    {
        foreach(GameObject vbot in vbotObj)
        {
            ItemList.Add(vbot.GetComponent<RectTransform>());
        }
        Init();
    }

}
