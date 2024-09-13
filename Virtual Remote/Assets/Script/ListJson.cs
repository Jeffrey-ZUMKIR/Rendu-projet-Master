using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListJson
{
    [System.Serializable]
    public class Item
    {
        public int id;
        //public byte[] vbotTexture;
        public string name;
        public bool used;

        public Item(int id, /*byte[] vbotTexture,*/ string name, bool used)
        {
            this.id = id;
            //this.vbotTexture = vbotTexture;
            this.name = name;
            this.used = used;
        }
    }

    public Item[] items;

    public ListJson(Item[] items)
    {
        this.items = items;
    }
}
