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


	void Start () {
        Time.timeScale = 1;
		Setdata();
		//Debug.Log(PlayerPrefs.GetInt("totalCoins"));
		//PlayerPrefs.DeleteKey("Gold Seeker");
		//PlayerPrefs.DeleteAll();

		transmitPage = GameObject.Find ("Canvas/TransmitPage").GetComponent<Animator> ();
		musicAudio = GameObject.Find ("BackGroundMusic");
		/*if (!musicAudio.GetComponent<AudioSource> ().isPlaying)
			musicAudio.GetComponent<AudioSource> ().Play ();*/
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

	public void Quests(int num)
	{
		if (num == 1)
			questPannel.SetActive(true);
		else
			questPannel.SetActive(false);

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



}
