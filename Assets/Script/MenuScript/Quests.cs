using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{

	

	private string title,description,category;

	public string Title
    {
		get { return title; }
		set { title = value; }
    }
	public string Description
	{
		get { return description; }
		set { description = value; }
	}
	public string Category
	{
		get { return category; }
		set { category = value; }
	}

	private int rank, coin;
	public int Rank
	{
		get { return rank; }
		set { rank = value; }
	}
	public int Coin
	{
		get { return coin; }
		set { coin = value; }
	}

	private Sprite icon;
	public Sprite Icon
	{
		get { return icon; }
		set { icon = value; }
	}

	private GameObject questRef;
	public GameObject QuestRef
	{
		get { return questRef; }
		set { questRef = value; }
	}

	private bool unLocked;
	public bool UnLocked
	{
		get { return unLocked; }
		set { unLocked = value; }
	}

    public Quests(string category,string title,string description,int rank,int coin,GameObject questRef,Sprite icon)
    {
        
        this.category = category;
        this.title = title;
        this.description = description;
        this.unLocked = false;
        this.rank = rank;
        this.coin = coin;
        this.questRef = questRef;
		this.icon = icon;
		LoadQuest();
    }

    public bool ErnQuest()
    {
        if(!unLocked)
        {
			Transform tick = questRef.transform.GetChild(7);
			tick.gameObject.SetActive(true);
            unLocked = true;
			SaveQuest(unLocked);
			
			return true;
        }
        return false;
    }

	public void SaveQuest (bool value)
    {
		unLocked = value;
		int tmpCoins = PlayerPrefs.GetInt("Coin");
		PlayerPrefs.SetInt("Coin",tmpCoins+=coin);
		int tmptotalCoins = PlayerPrefs.GetInt("totalCoins");
		PlayerPrefs.SetInt("totalCoins", tmptotalCoins += coin);
		int tmpRanks = PlayerPrefs.GetInt("Rank");
		PlayerPrefs.SetInt("Rank", tmpRanks += rank);
		PlayerPrefs.SetInt(title,value ? 1:0);
		PlayerPrefs.Save();
    }

	public void LoadQuest()
    {
		unLocked = PlayerPrefs.GetInt(title) == 1 ? true : false;
		if(unLocked)
        {
			Transform tick = questRef.transform.GetChild(7);
			tick.gameObject.SetActive(true);
		}
    }

}
