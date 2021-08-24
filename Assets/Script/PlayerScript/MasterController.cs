using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MasterController : MonoBehaviour {

	public GameObject[] playerPrefabes;
	public bool canDie ;
	public AudioClip[] gameSounds;
	public AudioSource masterAudio,enginAudio,speakAudio;
	public GameObject canvas,deadScreen,motor,musicAudio;
	private Rigidbody2D playerRG;
	private Motory theMotory;
	private GameUIController gameUIController;
	private GunController gunController;
	public int      levelNumber,coinCount, bulletCount, shieldCount, rockCount,checkPointRocks,deadCount = 0;
	public Text     coinText,bulletText,ShieldText,rockText;
	public GameObject loadingPage,finishPage;
	private Animator finishAnim;
	public CinemachineVirtualCamera CM1;


	// LoadCheckPoint

	public Transform lastCheckPoint;
	public GameObject mainCamera,backGround,turbo;
	private AutoDestroyObject[] rockObjects;

	// FinishGame
	public WheelJoint2D wheelFront,wheelBack;


	// Use this for initialization
	void Start () {

		canDie = true;
		playerRG = GetComponent<Rigidbody2D>();
		theMotory = FindObjectOfType<Motory>();
		gameUIController = FindObjectOfType<GameUIController>();
		gunController = FindObjectOfType<GunController>();
		enginAudio = GameObject.Find ("MasterController/EnginAudio").GetComponent<AudioSource> ();
		checkPointRocks = rockCount;
		CM1 = FindObjectOfType<CinemachineVirtualCamera>();

		canvas = GameObject.Find ("Canvas");
		loadingPage = GameObject.Find("gameLoading");
		musicAudio = GameObject.Find ("BackGroundMusic");
		if(!musicAudio == null)
			musicAudio.GetComponent<AudioSource> ().Stop ();


		bulletCount = PlayerPrefs.GetInt ("Bullet");
		shieldCount = PlayerPrefs.GetInt ("Shield");

		//PlayerPrefs.SetInt ("Bullet",0);
		//PlayerPrefs.SetInt ("Shield",0);


		LoadCheckPoint ();
		Setdata ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Setdata()
	{

		coinText.text = coinCount + "";
		bulletText.text = bulletCount + "";
		ShieldText.text = shieldCount + "";
		rockText.text = rockCount + "";
	}


	public void endGame()
	{
		if(canDie)
		{
			canDie = false;
			Debug.Log("Game Over");
			deadCount++;
			masterAudio.clip = gameSounds[0];
			masterAudio.Play();
			enginAudio.Stop();
			deadScreen.SetActive(true);
			SaveData();
			PlayerPrefs.SetInt("totalDead", PlayerPrefs.GetInt("totalDead") + 1);
		}
	}

	public void FinishGame()
	{
		if(canDie)
        {
			canDie = false;
			masterAudio.clip = gameSounds[1];
			masterAudio.Play();
			finishPage.SetActive(true);
			finishAnim = finishPage.GetComponent<Animator>();

			if (deadCount == 0)
			{
				finishAnim.Play("Finish3Star");
				PlayerPrefs.SetInt("starLevel" + levelNumber, 3);
				PlayerPrefs.SetInt("total3Star", PlayerPrefs.GetInt("total3Star") + 1);
			}
			else if (deadCount < 4)
			{
				finishAnim.Play("Finish2Star");
				PlayerPrefs.SetInt("starLevel" + levelNumber, 2);
			}
			else
			{
				finishAnim.Play("Finish1Star");
				PlayerPrefs.SetInt("starLevel" + levelNumber, 1);
			}
			SaveData();
			PlayerPrefs.SetInt("currentLevel", levelNumber);
			wheelFront = GameObject.Find("Player/motor_front_wheel").GetComponent<WheelJoint2D>();
			wheelBack = GameObject.Find("Player/motor_back_wheel").GetComponent<WheelJoint2D>();
			wheelFront.useMotor = false;
			wheelBack.useMotor = false;
		}
	}


	public void LoadCheckPoint()
	{
		//Destroy (GameObject.Find("motor(Clone)"));
		Destroy (GameObject.FindGameObjectWithTag("player"));

		GameObject motorInstance =
			Instantiate(playerPrefabes[PlayerPrefs.GetInt("playerPrefabe")],
				lastCheckPoint.position,
				lastCheckPoint.rotation) as GameObject;
		motorInstance.name = "Player";
		CM1.Follow = motorInstance.transform;


		backGround = GameObject.Find ("BackGround");
		backgroundControll controlback = (backgroundControll)backGround.GetComponent (typeof(backgroundControll));
		controlback.SetTarget ();

		turbo = GameObject.Find ("TakeOff");
		Turbo turboSc = (Turbo)turbo.GetComponent (typeof(Turbo));
		turboSc.SetTaregt ();

		if (gunController.gunIsUsed) {
			gameUIController.GunBtn ();
		}

		canDie = true;
		enginAudio.Play();
		rockCount = checkPointRocks;
		Setdata ();
		SaveData();
		DestroyCloneObjects ();
	}

	public void SaveData()
	{
		PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin") + coinCount);
		PlayerPrefs.SetInt("totalCoins", PlayerPrefs.GetInt("totalCoins") + coinCount);
		coinCount = 0;

		if (bulletCount<PlayerPrefs.GetInt("Bullet"))
			PlayerPrefs.SetInt("Bullet", bulletCount);
		if(shieldCount<PlayerPrefs.GetInt("Shield"))
			PlayerPrefs.SetInt("Shield", shieldCount);

		//int Cups = (Mathf.RoundToInt(motor.transform.position.x/10));
		//Debug.Log(" My Cups : " + Cups);
		/*if (PlayerPrefs.GetInt("Cups") < Cups)
		{
			PlayerPrefs.SetInt("Cups", Cups);
		}*/
	}

	public IEnumerator LoadingDelay()
	{
		loadingPage.SetActive(true);
		yield return new WaitForSeconds(1.1f);
		loadingPage.SetActive(false);
	}

	public void MuteSound()

	{
		enginAudio = GameObject.FindGameObjectWithTag ("MasterController/EngineAudio").GetComponent<AudioSource> ();
		masterAudio.mute = !masterAudio.mute;
		enginAudio.mute = !enginAudio.mute;
		speakAudio.mute = !speakAudio.mute;
	}

	private void DestroyCloneObjects()
	{
		rockObjects = GameObject.FindObjectsOfType<AutoDestroyObject> ();
		foreach (var item in rockObjects) {
			Destroy (item.gameObject);
		}
	}
}
