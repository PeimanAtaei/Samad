using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManagerSc : MonoBehaviour
{
	private MainMenu master;
	public GameObject questPrefabe,generalCategory,achievementNotifParent,visualQuest;
	public Sprite[] icons;
	public ScrollRect scrollRect;
	public GameObject[] QuestLists;
	public Dictionary<string, Quests> questDictionary = new Dictionary<string, Quests>();

	private static QuestManagerSc instance;
	public static QuestManagerSc Instance
    {
		get 
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<QuestManagerSc>();
			}
			return QuestManagerSc.instance;
		}
    }

	void Start()
    {
		//PlayerPrefs.DeleteAll();
		master = GameObject.FindObjectOfType<MainMenu>();
		
		

		//CreateQuest ("General","Test Title","Test description",2,10,icons[0]);

		foreach (var item in QuestList.instance.questGroups)
        {
            for (int i = 0; i < item.questItems.Count; i++)
            {
				CreateQuest(item.category, item.questItems[i].title, item.questItems[i].description, item.questItems[i].rank, item.questItems[i].coin, item.questItems[i].icon);
			}
        }

		Debug.Log(PlayerPrefs.GetInt("total3Star") + " total 3 star games");
		Debug.Log(PlayerPrefs.GetInt("totalDead") + " totalDead");

		CheckPlayQuests();
		CheckRankQuests();
		CheckLevelQuests();
		Check3StarQuests();
		CheckNoobQuests();
	}

    // Update is called once per frame
   /* void Update()
    {
		if (Input.GetKeyDown(KeyCode.W))
		{
			CheckErn("Gold Seeker");
		}
	}*/

	public void LoadOuestsInCategories(string category)
    {
		questDictionary.Clear();
		GameObject[] currntQuests = GameObject.FindGameObjectsWithTag("quest");
        foreach (var item in currntQuests)
        {
			Destroy(item);
        }

		if (category.Equals("General"))
		{
			foreach (var item in QuestList.instance.questGroups)
			{
				for (int i = 0; i < item.questItems.Count; i++)
				{
					CreateQuest(item.category, item.questItems[i].title, item.questItems[i].description, item.questItems[i].rank, item.questItems[i].coin, item.questItems[i].icon);
				}
			}
		}
		else if(category.Equals("Achieved"))
        {
			foreach (var item in QuestList.instance.questGroups)
			{
				for (int i = 0; i < item.questItems.Count; i++)
				{
					if(PlayerPrefs.GetInt(item.questItems[i].title) == 1)
                    {
						CreateQuest(item.category, item.questItems[i].title, item.questItems[i].description, item.questItems[i].rank, item.questItems[i].coin, item.questItems[i].icon);
					}
					
				}
			}
		}
		else
        {
			foreach (var item in QuestList.instance.questGroups)
			{
				if (item.category.Equals(category))
				{
					Debug.Log(item.category);
					for (int i = 0; i < item.questItems.Count; i++)
					{
						Debug.Log(item.questItems[i].title);
						CreateQuest(item.category, item.questItems[i].title, item.questItems[i].description, item.questItems[i].rank, item.questItems[i].coin, item.questItems[i].icon);
					}
				}
			}
		}
		
	}

	public void CheckErn(string title)
    {
		ErnQuests(title);
	}

	private void CreateQuest(string category,string title,string description,int rank,int coin,Sprite icon)
	{
		GameObject quest = (GameObject) Instantiate (questPrefabe);
		quest.name = "Quest";
		Quests newQuest = new Quests(category,title,description,rank,coin,quest, icon);
		questDictionary.Add(title,newQuest);
		SetQuestInfo(category,quest,title);
	}

	public void ErnQuests(string title)
    {
		if(questDictionary[title].ErnQuest())
        {
			GameObject questNotif = (GameObject)Instantiate(visualQuest);
			questNotif.name = "QuestNotification";

			master.Setdata();

			Debug.Log(questDictionary[title].Coin+"");

			SetQuestInfo("Achieved",questNotif, title);
			StartCoroutine(HideQuestNotification(questNotif));
        }
    }

	private IEnumerator HideQuestNotification(GameObject visualQuest)
    {
		yield return new WaitForSeconds(4);
		Destroy(visualQuest);
    }

	private void SetQuestInfo(string category,GameObject quest,string title)
	{
		if (!category.Equals ("Achieved")) {
			quest.transform.SetParent (generalCategory.transform);
			quest.transform.localScale = new Vector3 (1,1,1);
		}else
        {
			quest.transform.SetParent(achievementNotifParent.transform);
			quest.transform.localScale = new Vector3(1, 1, 1);
		}
		quest.transform.GetChild (0).GetComponent<Image> ().sprite = questDictionary[title].Icon;
		quest.transform.GetChild (1).GetComponent<Text> ().text = title;	
		quest.transform.GetChild (2).GetComponent<Text> ().text = questDictionary[title].Description;
		quest.transform.GetChild (3).GetComponent<Text> ().text = questDictionary[title].Rank + "";
		quest.transform.GetChild (4).GetComponent<Text> ().text = questDictionary[title].Coin + "";
	}

	public void ChangeCategory(int num)
	{
		scrollRect.content = QuestLists [num].GetComponent<RectTransform> ();
	}

	public void CheckLevelQuests()
	{
		if (PlayerPrefs.GetInt("currentLevel") + 1 >=5 && PlayerPrefs.GetInt("Passenger") == 0)
		{
			ErnQuests("Passenger");
		}
		else if (PlayerPrefs.GetInt("currentLevel") + 1 >= 10 && PlayerPrefs.GetInt("Rubberneck") == 0)
		{
			ErnQuests("Rubberneck");
		}
		else if (PlayerPrefs.GetInt("currentLevel") + 1 >= 20 && PlayerPrefs.GetInt("Tenacious") == 0)
		{
			ErnQuests("Tenacious");
		}
		else if (PlayerPrefs.GetInt("currentLevel") + 1 >= 40 && PlayerPrefs.GetInt("Stubborn") == 0)
		{
			ErnQuests("Stubborn");
		}
		else if (PlayerPrefs.GetInt("currentLevel") + 1 >= 60 && PlayerPrefs.GetInt("Rebellious") == 0)
		{
			ErnQuests("Rebellious");
		}
	}

	public void CheckPlayQuests()
	{
		if (PlayerPrefs.GetInt("totalPlay") >= 5 && PlayerPrefs.GetInt("Rookie player") == 0)
		{
			ErnQuests("Rookie player");
		}
		else if (PlayerPrefs.GetInt("totalPlay") >= 15 && PlayerPrefs.GetInt("Ordinary player") == 0)
		{
			ErnQuests("Ordinary player");
		}
		else if (PlayerPrefs.GetInt("totalPlay") >= 30 && PlayerPrefs.GetInt("Talented player") == 0)
		{
			ErnQuests("Talented player");
		}
		else if (PlayerPrefs.GetInt("totalPlay") >= 60 && PlayerPrefs.GetInt("Loyal player") == 0)
		{
			ErnQuests("Loyal player");
		}
		else if (PlayerPrefs.GetInt("totalPlay") >= 100 && PlayerPrefs.GetInt("My Friend") == 0)
		{
			ErnQuests("My Friend");
		}
	}

	public void CheckRankQuests()
	{
		if (PlayerPrefs.GetInt("Rank") >= 10 && PlayerPrefs.GetInt("Fresh man") == 0)
		{
			ErnQuests("Fresh man");
		}
		else if (PlayerPrefs.GetInt("Rank") >= 36 && PlayerPrefs.GetInt("Experianced") == 0)
		{
			ErnQuests("Experianced");
		}
		else if (PlayerPrefs.GetInt("Rank") >= 76 && PlayerPrefs.GetInt("Professional") == 0)
		{
			ErnQuests("Professional");
		}
		else if (PlayerPrefs.GetInt("Rank") >= 132 && PlayerPrefs.GetInt("Champion") == 0)
		{
			ErnQuests("Champion");
		}
		else if (PlayerPrefs.GetInt("Rank") >= 205 && PlayerPrefs.GetInt("Master") == 0)
		{
			ErnQuests("Master");
		}
	}

	public void Check3StarQuests()
	{
		if (PlayerPrefs.GetInt("total3Star") >= 5 && PlayerPrefs.GetInt("Star picker") == 0)
		{
			ErnQuests("Star picker");
		}
		else if (PlayerPrefs.GetInt("total3Star") >= 10 && PlayerPrefs.GetInt("Star Eater") == 0)
		{
			ErnQuests("Star Eater");
		}
		else if (PlayerPrefs.GetInt("total3Star") >= 17 && PlayerPrefs.GetInt("Star coocker") == 0)
		{
			ErnQuests("Star coocker");
		}
		else if (PlayerPrefs.GetInt("total3Star") >= 25 && PlayerPrefs.GetInt("Star hunter") == 0)
		{
			ErnQuests("Star hunter");
		}
		else if (PlayerPrefs.GetInt("total3Star") >= 35 && PlayerPrefs.GetInt("Star Tamer") == 0)
		{
			ErnQuests("Star Tamer");
		}
	}

	public void CheckNoobQuests()
	{
		if (PlayerPrefs.GetInt("totalDead") >= 10 && PlayerPrefs.GetInt("Noob Player") == 0)
		{
			ErnQuests("Noob Player");
		}
		else if (PlayerPrefs.GetInt("totalDead") >= 20 && PlayerPrefs.GetInt("Real Noob") == 0)
		{
			ErnQuests("Real Noob");
		}
		else if (PlayerPrefs.GetInt("totalDead") >= 40 && PlayerPrefs.GetInt("Super Noob") == 0)
		{
			ErnQuests("Super Noob");
		}
		else if (PlayerPrefs.GetInt("totalDead") >= 80 && PlayerPrefs.GetInt("Goddess of Noobs") == 0)
		{
			ErnQuests("Goddess of Noobs");
		}
		else if (PlayerPrefs.GetInt("totalDead") >= 100 && PlayerPrefs.GetInt("My Noob Friend") == 0)
		{
			ErnQuests("My Noob Friend");
		}
	}
}
