using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseQuestManager : MonoBehaviour
{

	public GameObject visualQuest, achievementNotifParent;
	public Dictionary<string, Quests> questDictionary = new Dictionary<string, Quests>();
	private HouseController master;
	// Start is called before the first frame update
	void Start()
    {
		master = FindObjectOfType<HouseController>();
		//PlayerPrefs.DeleteKey("Gold Seeker");
		//PlayerPrefs.DeleteAll();

		foreach (var item in QuestList.instance.questGroups)
		{
			for (int i = 0; i < item.questItems.Count; i++)
			{
				CreateQuest(item.category, item.questItems[i].title, item.questItems[i].description, item.questItems[i].rank, item.questItems[i].coin, item.questItems[i].icon);
			}
		}

		CheckBulletQuests();
		CheckShieldQuests();

	}

	/*void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			CheckErn("Tired chicken");
		}
	}*/

	private void CreateQuest(string category, string title, string description, int rank, int coin, Sprite icon)
	{
		Quests newQuest = new Quests(category, title, description, rank, coin, visualQuest, icon);
		questDictionary.Add(title, newQuest);
	}

	/*public void CheckErn(string title)
	{
		ErnQuests(title);
	}*/

	public void ErnQuests(string title)
	{
		if (!questDictionary[title].UnLocked)
		{
			questDictionary[title].SaveQuest(true);

			GameObject questNotif = (GameObject)Instantiate(visualQuest);
			questNotif.name = "QuestNotification";

			master.coinCount += questDictionary[title].Coin;
			master.SetData();

			SetQuestInfo("Achieved", questNotif, title);
			StartCoroutine(HideQuestNotification(questNotif));
	
		}
	}

	private IEnumerator HideQuestNotification(GameObject visualQuest)
	{
		yield return new WaitForSeconds(4);
		Destroy(visualQuest);
	}

	private void SetQuestInfo(string category, GameObject quest, string title)
	{
		if (category.Equals("Achieved"))
		{
			quest.transform.SetParent(achievementNotifParent.transform);
			quest.transform.localScale = new Vector3(1, 1, 1);
		}

		quest.transform.GetChild(0).GetComponent<Image>().sprite = questDictionary[title].Icon;
		quest.transform.GetChild(1).GetComponent<Text>().text = title;
		quest.transform.GetChild(2).GetComponent<Text>().text = questDictionary[title].Description;
		quest.transform.GetChild(3).GetComponent<Text>().text = questDictionary[title].Rank + "";
		quest.transform.GetChild(4).GetComponent<Text>().text = questDictionary[title].Coin + "";
	}


	public void CheckCoinQuests()
    {
		if (PlayerPrefs.GetInt("totalCoins") >= 80 && PlayerPrefs.GetInt("Gold Seeker") == 0)
		{
			ErnQuests("Gold Seeker");
		}
		else if (PlayerPrefs.GetInt("totalCoins") >= 700 && PlayerPrefs.GetInt("Gold Detector") == 0)
		{
			ErnQuests("Gold Detector");
		}
		else if (PlayerPrefs.GetInt("totalCoins") >= 2000 && PlayerPrefs.GetInt("Goldsmith") == 0)
		{
			ErnQuests("Goldsmith");
		}
		else if (PlayerPrefs.GetInt("totalCoins") >= 4800 && PlayerPrefs.GetInt("Gold collector") == 0)
		{
			ErnQuests("Gold collector");
		}
		else if (PlayerPrefs.GetInt("totalCoins") >= 6000 && PlayerPrefs.GetInt("Coin Bank") == 0)
		{
			ErnQuests("Coin Bank");
		}
	}

	public void CheckBulletQuests()
	{
		if (PlayerPrefs.GetInt("totalBullets") >= 500 && PlayerPrefs.GetInt("Shooter") == 0)
		{
			ErnQuests("Shooter");
		}
		else if (PlayerPrefs.GetInt("totalBullets") >= 1500 && PlayerPrefs.GetInt("Commando") == 0)
		{
			ErnQuests("Commando");
		}
		else if (PlayerPrefs.GetInt("totalBullets") >= 3000 && PlayerPrefs.GetInt("Commander") == 0)
		{
			ErnQuests("Commander");
		}
		else if (PlayerPrefs.GetInt("totalBullets") >= 4500 && PlayerPrefs.GetInt("General") == 0)
		{
			ErnQuests("General");
		}
		else if (PlayerPrefs.GetInt("totalBullets") >= 7000 && PlayerPrefs.GetInt("Leader") == 0)
		{
			ErnQuests("Leader");
		}
	}

	public void CheckShieldQuests()
	{
		if (PlayerPrefs.GetInt("totalShields") >= 5 && PlayerPrefs.GetInt("Shield picker") == 0)
		{
			ErnQuests("Shield picker");
		}
		else if (PlayerPrefs.GetInt("totalShields") >= 10 && PlayerPrefs.GetInt("Shield maker") == 0)
		{
			ErnQuests("Shield maker");
		}
		else if (PlayerPrefs.GetInt("totalShields") >= 20 && PlayerPrefs.GetInt("Shield kisser") == 0)
		{
			ErnQuests("Shield kisser");
		}
		else if (PlayerPrefs.GetInt("totalShields") >= 40 && PlayerPrefs.GetInt("Shield lover") == 0)
		{
			ErnQuests("Shield lover");
		}
		else if (PlayerPrefs.GetInt("totalShields") >= 60 && PlayerPrefs.GetInt("Shield ------") == 0)
		{
			ErnQuests("Shield ------");
		}
	}

	public void CheckVehicleQuests()
	{
		if (PlayerPrefs.GetInt("totalVehicle") >= 1 && PlayerPrefs.GetInt("Car lover") == 0)
		{
			ErnQuests("Car lover");
		}
		else if (PlayerPrefs.GetInt("totalVehicle") >= 2 && PlayerPrefs.GetInt("Car collector") == 0)
		{
			ErnQuests("Car collector");
		}
		else if (PlayerPrefs.GetInt("totalVehicle") >= 3 && PlayerPrefs.GetInt("Car dealer") == 0)
		{
			ErnQuests("Car dealer");
		}
		else if (PlayerPrefs.GetInt("totalVehicle") >= 4 && PlayerPrefs.GetInt("Car Emperor") == 0)
		{
			ErnQuests("Car Emperor");
		}
		else if (PlayerPrefs.GetInt("totalVehicle") >= 5 && PlayerPrefs.GetInt("Princ of riders") == 0)
		{
			ErnQuests("Princ of riders");
		}
	}

	public void CheckDonateQuests()
	{
		if (PlayerPrefs.GetInt("totalDonate") >= 1 && PlayerPrefs.GetInt("Kind") == 0)
		{
			ErnQuests("Kind");
		}
		else if (PlayerPrefs.GetInt("totalDonate") >= 2 && PlayerPrefs.GetInt("Merciful") == 0)
		{
			ErnQuests("Merciful");
		}
		else if (PlayerPrefs.GetInt("totalDonate") >= 3 && PlayerPrefs.GetInt("Compassionate") == 0)
		{
			ErnQuests("Compassionate");
		}
		else if (PlayerPrefs.GetInt("totalDonate") >= 4 && PlayerPrefs.GetInt("Affectionate") == 0)
		{
			ErnQuests("Affectionate");
		}
		else if (PlayerPrefs.GetInt("totalDonate") >= 5 && PlayerPrefs.GetInt("hot blooded") == 0)
		{
			ErnQuests("hot blooded");
		}
	}

	public void CheckBuyQuests()
	{
		if (PlayerPrefs.GetInt("totalBuy") >= 1 && PlayerPrefs.GetInt("Rich kid") == 0)
		{
			ErnQuests("Rich kid");
		}
		else if (PlayerPrefs.GetInt("totalBuy") >= 2 && PlayerPrefs.GetInt("Capitalist") == 0)
		{
			ErnQuests("Capitalist");
		}
		else if (PlayerPrefs.GetInt("totalBuy") >= 3 && PlayerPrefs.GetInt("Financier") == 0)
		{
			ErnQuests("Financier");
		}
		else if (PlayerPrefs.GetInt("totalBuy") >= 4 && PlayerPrefs.GetInt("Investor") == 0)
		{
			ErnQuests("Investor");
		}
		else if (PlayerPrefs.GetInt("totalBuy") >= 5 && PlayerPrefs.GetInt("Silver spoon") == 0)
		{
			ErnQuests("Silver spoon");
		}
	}

}
