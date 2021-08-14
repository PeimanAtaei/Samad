using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelQuestsManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject visualQuest, achievementNotifParent;
    public Dictionary<string, Quests> questDictionary = new Dictionary<string, Quests>();
    private MasterController master;


    void Start()
    {
        master = FindObjectOfType<MasterController>();
        //PlayerPrefs.DeleteKey("Gold Seeker");
        //PlayerPrefs.DeleteAll();

        

        foreach (var item in QuestList.instance.questGroups)
        {
            for (int i = 0; i < item.questItems.Count; i++)
            {
                CreateQuest(item.category, item.questItems[i].title, item.questItems[i].description, item.questItems[i].rank, item.questItems[i].coin, item.questItems[i].icon);
            }
        }
    }

    // Update is called once per frame
    private void CreateQuest(string category, string title, string description, int rank, int coin, Sprite icon)
    {
        Quests newQuest = new Quests(category, title, description, rank, coin, visualQuest, icon);
        questDictionary.Add(title, newQuest);
    }

    public void ErnQuests(string title)
    {
        if (!questDictionary[title].UnLocked)
        {
            questDictionary[title].SaveQuest(true);
    
            GameObject questNotif = (GameObject)Instantiate(visualQuest);
            questNotif.name = "QuestNotification";

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

    // CheckQuests -----------------------------------------------------------------------------------------------

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

    public void CheckChickenQuests()
    {
        if (PlayerPrefs.GetInt("totalChicken") >= 5 && PlayerPrefs.GetInt("Tired chicken") == 0)
        {
            ErnQuests("Tired chicken");
        }
        else if (PlayerPrefs.GetInt("totalChicken") >= 15 && PlayerPrefs.GetInt("Fat chicken") == 0)
        {
            ErnQuests("Fat chicken");
        }
        else if (PlayerPrefs.GetInt("totalChicken") >= 30 && PlayerPrefs.GetInt("Agile chicken") == 0)
        {
            ErnQuests("Agile chicken");
        }
        else if (PlayerPrefs.GetInt("totalChicken") >= 60 && PlayerPrefs.GetInt("Bold chicken") == 0)
        {
            ErnQuests("Bold chicken");
        }
        else if (PlayerPrefs.GetInt("totalChicken") >= 100 && PlayerPrefs.GetInt("Proud chicken") == 0)
        {
            ErnQuests("Proud chicken");
        }
    }

    public void CheckPortalQuests()
    {
        if (PlayerPrefs.GetInt("totalPortals") >= 1 && PlayerPrefs.GetInt("Searcher") == 0)
        {
            ErnQuests("Searcher");
        }
        else if (PlayerPrefs.GetInt("totalPortals") >= 3 && PlayerPrefs.GetInt("Prier") == 0)
        {
            ErnQuests("Prier");
        }
        else if (PlayerPrefs.GetInt("totalPortals") >= 6 && PlayerPrefs.GetInt("Voyeur") == 0)
        {
            ErnQuests("Voyeur");
        }
        else if (PlayerPrefs.GetInt("totalPortals") >= 12 && PlayerPrefs.GetInt("Uninvited") == 0)
        {
            ErnQuests("Uninvited");
        }
        else if (PlayerPrefs.GetInt("totalPortals") >= 24 && PlayerPrefs.GetInt("Explorer") == 0)
        {
            ErnQuests("Explorer");
        }
    }

    public void CheckRingQuests()
    {
        if (PlayerPrefs.GetInt("totalRings") >= 20 && PlayerPrefs.GetInt("Ring Seeker") == 0)
        {
            ErnQuests("Ring Seeker");
        }
        else if (PlayerPrefs.GetInt("totalRings") >= 50 && PlayerPrefs.GetInt("Ring Detector") == 0)
        {
            ErnQuests("Ring Detector");
        }
        else if (PlayerPrefs.GetInt("totalRings") >= 150 && PlayerPrefs.GetInt("Ring seller") == 0)
        {
            ErnQuests("Ring seller");
        }
        else if (PlayerPrefs.GetInt("totalRings") >= 300 && PlayerPrefs.GetInt("Ring eater") == 0)
        {
            ErnQuests("Ring eater");
        }
        else if (PlayerPrefs.GetInt("totalRings") >= 600 && PlayerPrefs.GetInt("Lord of the ring") == 0)
        {
            ErnQuests("Lord of the ring");
        }
    }

    public void CheckDiamondQuests()
    {
        if (PlayerPrefs.GetInt("totalDiamonds") >= 5 && PlayerPrefs.GetInt("Diamond maker") == 0)
        {
            ErnQuests("Diamond maker");
        }
        else if (PlayerPrefs.GetInt("totalDiamonds") >= 15 && PlayerPrefs.GetInt("Diamond Seeker") == 0)
        {
            ErnQuests("Diamond Seeker");
        }
        else if (PlayerPrefs.GetInt("totalDiamonds") >= 30 && PlayerPrefs.GetInt("Diamond digger") == 0)
        {
            ErnQuests("Diamond digger");
        }
        else if (PlayerPrefs.GetInt("totalDiamonds") >= 60 && PlayerPrefs.GetInt("Diamond quester") == 0)
        {
            ErnQuests("Diamond quester");
        }
        else if (PlayerPrefs.GetInt("totalDiamonds") >= 120 && PlayerPrefs.GetInt("Treasure hunter") == 0)
        {
            ErnQuests("Treasure hunter");
        }
    }
}
