using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingSc : MonoBehaviour
{

	private int languageNumber = 0;
	private GameObject flagIcon,soundIcon,musicIcon;
	public  bool sound = true, music = true;
	public  Sprite[] soundIcons;
	

    // Start is called before the first frame update
    void Start()
    {
		flagIcon = GameObject.Find ("Canvas/MainMenu/Setting/Board/Flag");
		soundIcon = GameObject.Find ("Canvas/MainMenu/Setting/Board/Sound");
		musicIcon = GameObject.Find ("Canvas/MainMenu/Setting/Board/Music");
		languageNumber = PlayerPrefs.GetInt ("language");
    }
		
	/*public void OnFlagRightClick()
	{
		if (LanguageSc.instance.languages.Count - 1 == languageNumber)
			languageNumber = 0;
		else
			languageNumber++;

		SetLanguage ();
		
	}

	public void OnFlagLeftClick()
	{
		if (languageNumber == 0)
			languageNumber = LanguageSc.instance.languages.Count - 1;
		else
			languageNumber--;

		SetLanguage ();
	}

	private void SetLanguage()
	{
		flagIcon.GetComponent<Image> ().sprite = LanguageSc.instance.languages [languageNumber].flag;
	}*/

	public void ChangeSounds(int num)
	{
		if (num == 1) {
			
			if (sound) {
				soundIcon.GetComponent<Image> ().sprite = soundIcons [1];
			} else {
				soundIcon.GetComponent<Image> ().sprite = soundIcons [0];
			}
			sound = !sound;
		}
		else{

			if (music) {
				musicIcon.GetComponent<Image> ().sprite = soundIcons [1];
			} else {
				musicIcon.GetComponent<Image> ().sprite = soundIcons [0];
			}
			music = !music;
		}
	}

	public void LoadLanguage()
    {
		SceneManager.LoadScene("language");
    }
}
