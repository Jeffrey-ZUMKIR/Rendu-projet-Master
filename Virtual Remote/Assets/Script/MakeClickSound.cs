using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    public RectTransform middleImage;

    bool didPlayed;
    // Start is called before the first frame update
    void Start()
    {
        didPlayed = false;
        audioSource = GetComponent<AudioSource>();
        middleImage = GameObject.Find("MiddlePoint").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(GetComponent<RectTransform>().position, middleImage.position);
        if (distance < 350)
        {
            if (!didPlayed)
            {
                didPlayed = true;
                audioSource.Play();
            }
        }
        else
        {
            didPlayed = false;
        }
        
    }
}
