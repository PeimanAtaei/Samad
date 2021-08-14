using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSc : MonoBehaviour
{
	public static LanguageSc instance;

	public List<Language> languages ;

	[System.Serializable]
	public class Language
	{
		public string name;
		public Sprite flag;
	}

	void Start()
	{
		
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
