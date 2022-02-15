using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string fistLevel;
    public Text Coins;
	public Animator menuAnim, transmitPage;
	public GameObject questPannel;
	private GameObject musicAudio;
	public int coinCount,rankCount;
	public Text coinText, rankText;
	public AudioSource backGroundSource;


	void Start () {
        Time.timeScale = 1;
		Setdata();

		transmitPage = GameObject.Find ("Canvas/TransmitPage").GetComponent<Animator> ();
		musicAudio = GameObject.Find ("BackGroundMusic");

		CheckFirstGuide();
		DetectPlatform();
		GetBackGroundSource();
		

	}

	public void StoryBotton()
	{
		if (PlayerPrefs.GetInt ("FirstStory") == 1) {
			StartCoroutine("MenuLoadingDelay","map");
		} else {
			StartCoroutine("MenuLoadingDelay","FirstStory");
		}
	}

	public void LoadHouse()
	{
		StartCoroutine("MenuLoadingDelay","house");
	}

	public void Help()
	{
		StartCoroutine("MenuLoadingDelay", "help");
	}

	public void FreeCoin(int num)
	{
		if(num == 1)
			menuAnim.Play("FreeCoinOpen");
		else
			menuAnim.Play("FreeCoinClose");
		
	}

	public void Setting(int num)
	{
		if(num == 1)
			menuAnim.Play("SettingOpen");
		else
			menuAnim.Play("SettingClose");

	}

	public void UpdateAlarm(int num)
	{
		if (num == 1)
			menuAnim.Play("UpdateOpen");
		else
			menuAnim.Play("UpdateClose");

	}

	public void Quests(int num)
	{
		if (num == 1)
			questPannel.SetActive(true);
		else
			questPannel.SetActive(false);

	}

	public void FreeCoinsOptions(int num)
    {
		switch (num)
		{
			case 1:
                {
					// Video
					break;
                }
			case 2:
				{
					// Instagram
					string instagram = "url:instagram://user?username=atisapp";
					Application.OpenURL(instagram);
					break;
				}
			case 3:
				{
					// Rate
					break;
				}
		}
    }

	public void CheckFirstGuide()
    {
		Debug.Log("start guide");
		if (PlayerPrefs.GetInt("firstGuide") == 0)
        {
			Debug.Log("start guide");
			menuAnim.Play("FirstGuide");
		}
    }

	public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator MenuLoadingDelay(string levelName)
    {
		transmitPage.Play ("TransmitFadeIn");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelName);
    }
    
	public void Setdata()
	{
		coinCount = PlayerPrefs.GetInt("Coin");
		rankCount = PlayerPrefs.GetInt("Rank");

		coinText.text = coinCount + "";
		rankText.text = rankCount + "";
		
	}

	public void DetectPlatform()
    {
		if(Application.platform == RuntimePlatform.Android)
        {
			Debug.Log("Android Player");
			PlayerPrefs.SetString("platform","android");
        }
		else if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
			Debug.Log("IPhone Player");
			PlayerPrefs.SetString("platform", "ios");
		}
		else
		{
			Debug.Log(Application.platform+"");
			PlayerPrefs.SetString("platform", "windows");
		}
	}

	public void GetBackGroundSource()
	{
		backGroundSource = GameObject.Find("BackGroundMusic").GetComponent<AudioSource>();
		Debug.Log(" not mute"+ PlayerPrefs.GetInt("music"));
		if (PlayerPrefs.GetInt("music") == 0 && !backGroundSource.isPlaying)
        {
			Debug.Log(" not mute");
			backGroundSource.Play();
		}
			
		if (PlayerPrefs.GetInt("music") == 1)
        {
			Debug.Log("mute");
			backGroundSource.Pause();
		}
			
	}



}
