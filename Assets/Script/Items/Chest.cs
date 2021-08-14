using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private MasterController masterController;
    private LevelQuestsManager questManager;
    private Animator chestAnim;
    private bool canReward = true;
    public int rewardCount;
    public AudioClip rewardSound;
    private AudioSource masterAudio;
    void Start()
    {
        masterController = FindObjectOfType<MasterController>();
        questManager = FindObjectOfType<LevelQuestsManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "player":
                {
                    if (canReward)
                    {
                        canReward = false;
                        InsertReward();
                    }
                    break;
                }
        }
    }


	private void InsertReward()
	{
        chestAnim = gameObject.GetComponent<Animator>();
        chestAnim.Play("ChestOpen");
        

        masterAudio = GameObject.Find("MasterController/Master Audio").GetComponent<AudioSource>();
        masterAudio.clip = rewardSound;
        masterAudio.loop = false;
        masterAudio.Play();

        
        PlayerPrefs.SetInt("totalDiamonds", PlayerPrefs.GetInt("totalDiamonds") + rewardCount);
        questManager.CheckDiamondQuests();
        Debug.Log(PlayerPrefs.GetInt("totalDiamonds"));

    }
}
