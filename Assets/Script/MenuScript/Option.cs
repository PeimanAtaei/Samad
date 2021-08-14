using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Option : MonoBehaviour {

    public bool isMute;
    public AudioSource menuSounds;
	public GameObject freeCoinImage,stars,balls,safes;
	public Animator freeCoinAnim;

	void Start () {
		if(PlayerPrefs.GetInt("freeCoin")==1)
		{
			freeCoinImage.SetActive (false);
		}
		if(PlayerPrefs.GetInt("Safes")==1)
		{
			safes.SetActive (false);
		}
		if(PlayerPrefs.GetInt("Balls")==1)
		{
			balls.SetActive (false);
		}
		if(PlayerPrefs.GetInt("Stars")==1)
		{
			stars.SetActive (false);
		}
	}

	public void Instagram()
    {
        Application.OpenURL("instagram://user?username=atisapp");
    }

    public void Telegram()
    {
        Application.OpenURL("https://t.me/atisapp");
    }

    public void AtisApp()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.atisapp.android&hl=en");
    }

	public void SafeApp()
	{
		Application.OpenURL("https://cafebazaar.ir/app/com.atisapp.safes/?l=fa");
		PlayerPrefs.SetInt ("Safes", 1);
		safes.SetActive (false);
		TurnOffFreeCoin ();
	}

	public void BallsApp()
	{
		Application.OpenURL("https://cafebazaar.ir/app/com.atisapp.ballescape/?l=fa");
		PlayerPrefs.SetInt ("Balls", 1);
		balls.SetActive (false);
		TurnOffFreeCoin ();

	}

    public void MuteSounds()
    {
        menuSounds.mute = !menuSounds.mute;
        isMute = menuSounds.mute;
    }

	public void ShowFreeCoinDialog()
	{
		freeCoinAnim.Play ("freeCoinAnim");
	}

	public void CloseFreeCoinDialog()
	{
		freeCoinAnim.Play ("freeCoinCloseAnim");
	}


	public void FreeCoin()
	{
		PlayerPrefs.SetInt ("Stars", 1);
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");

		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");

		intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_EDIT"));
		intentObject.Call<AndroidJavaObject> ("setData", uriClass.CallStatic<AndroidJavaObject>("parse","bazaar://details?id=com.atisapp.samad"));
		intentObject.Call<AndroidJavaObject>("setPackage", "com.farsitel.bazaar");

		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
		currentActivity.Call ("startActivity", intentObject);
		stars.SetActive (false);
		TurnOffFreeCoin ();
	}

	public void TurnOffFreeCoin()
	{
		int coins = PlayerPrefs.GetInt ("Coins");
		PlayerPrefs.SetInt ("Coins",coins+3000);
		if(PlayerPrefs.GetInt("Stars") == 1 && PlayerPrefs.GetInt("Safes") == 1 && PlayerPrefs.GetInt("Balls") == 1)
		{
			PlayerPrefs.SetInt ("freeCoin",1);
			freeCoinImage.SetActive (false);
			freeCoinAnim.Play ("freeCoinCloseAnim");

		}
	}

}
