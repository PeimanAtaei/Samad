using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour {

	private MasterController master;
	private Motory motor;
	private AlarmManager alarmManager;
	private GunController gunController;
	public GameObject pusePage,deadPage,finishPage,musicPage;
	private Animator transmitPage;
	public string nextLevel;
	private bool gunIsOpen = false;


	// Road Progress Value
	public Vector3 startPoint, finishPoint;
	public int roadProgressMaxValue = 0,readProgressCurrentValue;
	public Slider roadProgress;
	public GameObject camera;




	// Use this for initialization
	void Start () {
		master = FindObjectOfType<MasterController>();
		motor = FindObjectOfType<Motory>();
		alarmManager = FindObjectOfType<AlarmManager>();
		gunController = FindObjectOfType<GunController>();
		transmitPage = GameObject.Find("Canvas/TransmitPage").GetComponent<Animator>();


		Time.timeScale = 1;

		// Road Progress Value
		startPoint = GameObject.Find ("StartPoint").GetComponent<Transform> ().position;
		finishPoint = GameObject.Find ("FinishPoint").GetComponent<Transform> ().position;
		roadProgressMaxValue = (int)(finishPoint.x - startPoint.x) / 10;
		roadProgress.maxValue = roadProgressMaxValue;

	}
	
	// Update is called once per frame
	void Update () {
		UpdateRoadProgressBar ();
		if (motor == null)
			FindMotor();
	}

	private void FindMotor()
    {
		motor = FindObjectOfType<Motory>();
	}

	// Game Menu Buttons -------------------------------------------------------------------------

	public void CountinioGame()
	{
		pusePage.SetActive(false);
		Time.timeScale = 1;
	}

	public void VideoGame()
	{
		Time.timeScale = 1f;
		deadPage.SetActive (false);
		master.LoadCheckPoint ();
		master.canDie = true;
	}

	public void ResetGame()
	{
		Time.timeScale = 1f;
		deadPage.SetActive (false);
		finishPage.SetActive (false);
		pusePage.SetActive (false);
		master.LoadCheckPoint ();
	}

	public void PuseGame()
	{
		pusePage.SetActive(true);
		Time.timeScale = 0;
	}

	public void OpenMusic(int num)
	{
		if (num == 1)
		{ 
			musicPage.SetActive(true);
			Time.timeScale = 0;
        }
        else
        {
			musicPage.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void GunBtn()
	{
		gunIsOpen = gunController.gunIsUsed;
		if (master.bulletCount > 0) {
			if (!gunIsOpen) {
				
				OpenGun ();

			} else {
				
				CloseGun ();
			}
		} else {
			alarmManager.ShowMessege ("no enough bullet!");
			if (gunIsOpen) {
				
				CloseGun ();

				}
		}

	}


	public void ShieldBtn()
    {
		StartCoroutine("ShieldMethod");
    }
	public IEnumerator ShieldMethod()
    {
		if(!motor.shield && master.shieldCount>0)
        {
			Debug.Log("use shield");
			motor.shield = true;
			Animator shieldAnim = GameObject.Find("Player/Shield").GetComponent<Animator>();
			shieldAnim.Play("ShieldAnim");
			master.shieldCount--;
			master.Setdata();
			yield return new WaitForSeconds(20f);
			motor.shield = false;
		}else if(master.shieldCount == 0)
        {
			Debug.Log("no enough Shield");
			alarmManager.ShowMessege("no enough Shield!");
		}else
			Debug.Log("no");

	}

	public void OpenGun()
	{
		gunController.gunIsUsed = true;
		gunController.FindGunObjects ();
		gunController.gunAnim.Play ("GunOpen");
		gunController.gunSounds.clip = gunController.shootingSounds [0];
		gunController.gunSounds.loop = false;
		gunController.gunSounds.Play ();
		gunController.joystickObj.SetActive (true);
	}

	public void CloseGun()
	{
		gunController.gunIsUsed = false;
		gunController.FindGunObjects ();
		gunController.gunAnim.Play ("GunClose");
		gunController.gunSounds.clip = gunController.shootingSounds [0];
		gunController.gunSounds.loop = false;
		gunController.gunSounds.Play ();
		gunController.joystickObj.SetActive (false);
	}

	public void StartMainMenu()
	{
		master.SaveData();
		StartCoroutine("StartScene","menu");

	}

	public void StartNextLevel()
	{
		StartCoroutine("StartScene",nextLevel);
	}




	// Top Items -----------------------------------------------------------------------------------

	private void UpdateRoadProgressBar()
	{
		readProgressCurrentValue = (int)(camera.transform.position.x - startPoint.x)/10;
		roadProgress.value = readProgressCurrentValue;
	}

	// Aditional methods ---------------------------------------------------------------------------

	public IEnumerator StartScene(string sceneName)
	{
		Destroy(GameObject.Find("Player"));
		Time.timeScale = 1;
		transmitPage.Play("TransmitFadeIn");
		yield return new WaitForSeconds(1.1f);
		SceneManager.LoadScene(sceneName);
	}
}
