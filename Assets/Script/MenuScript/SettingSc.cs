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
	private MainMenu mainMenu;
	

    // Start is called before the first frame update
    void Start()
    {
		mainMenu = FindObjectOfType<MainMenu>();
		flagIcon = GameObject.Find ("Canvas/MainMenu/Setting/Board/Flag");
		soundIcon = GameObject.Find ("Canvas/MainMenu/Setting/Board/Sound");
		musicIcon = GameObject.Find ("Canvas/MainMenu/Setting/Board/Music");
		languageNumber = PlayerPrefs.GetInt ("language");
		CheckSoundSetting();

	}

	private void CheckSoundSetting()
    {
		if(PlayerPrefs.GetInt("sound") == 1)
        {
			soundIcon.GetComponent<Image>().sprite = soundIcons[1];
		}
		if (PlayerPrefs.GetInt("music") == 1)
		{
			musicIcon.GetComponent<Image>().sprite = soundIcons[1];
		}
		
	}

	public void ChangeSounds(int num)
	{
		if (num == 1) {
			
			if (PlayerPrefs.GetInt("sound") == 0) {
				soundIcon.GetComponent<Image> ().sprite = soundIcons [1];
				PlayerPrefs.SetInt("sound", 1);
			} else {
				soundIcon.GetComponent<Image> ().sprite = soundIcons [0];
				PlayerPrefs.SetInt("sound", 0);
			}
		}
		else{

			if (PlayerPrefs.GetInt("music") == 0) {
				musicIcon.GetComponent<Image> ().sprite = soundIcons [1];
				PlayerPrefs.SetInt("music",1);
			} else {
				musicIcon.GetComponent<Image> ().sprite = soundIcons [0];
				PlayerPrefs.SetInt("music", 0);
			}
			mainMenu.GetBackGroundSource();
		}
	}

	public void LoadLanguage()
    {
		SceneManager.LoadScene("language");
    }
}
