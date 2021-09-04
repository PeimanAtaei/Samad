using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private MasterController masterController;
    public AudioSource audioSource;
    public Animator anim;
    public bool canDestroy = true;
    public string animName;
    public AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        masterController = FindObjectOfType<MasterController>();
        anim = gameObject.GetComponent<Animator>();
        audioSource = GameObject.Find("MasterController/Master Audio").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canDestroy)
        {
            Debug.Log("Explotion");
            canDestroy = false;
            anim.Play(animName + "Explotion");
            audioSource.clip = sound;
            audioSource.Play();
        }/*
        switch (other.tag)
        {

            case "player":
                {
                    
                    break;
                }
        }*/
       
    }
}
