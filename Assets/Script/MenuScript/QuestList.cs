using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
	public static QuestList instance;

	public List<QuestGroups> questGroups;

	[System.Serializable]
	public class QuestGroups
	{
		public string category;
		public List<QuestItems> questItems;
	}

	[System.Serializable]
	public class QuestItems
	{
		public string title, description;
		public int rank, coin;
		public Sprite icon;
		public bool unLocked;
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
