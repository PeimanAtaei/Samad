using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MasterController : MonoBehaviour {

	public GameObject[] playerPrefabes;
	public bool canDie , saveSound = true;
	public AudioClip[] gameSounds;
	public AudioSource masterAudio,enginAudio,speakAudio,effectAudio,turboAudio;
	public GameObject canvas,deadScreen,musicAudio,player;
	public Motory theMotory;
	private Turbo turboSc;
	public GameUIController gameUIController;
	public GunController gunController;
	public AlarmManager alarmManager;
	public int      levelNumber,coinCount, bulletCount, shieldCount, rockCount,checkPointRocks,deadCount = 0,heartCount;
	public Text     coinText,bulletText,ShieldText,rockText,heartText;
	public GameObject loadingPage,finishPage;
	private Animator finishAnim,playerAnim;
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
		theMotory = FindObjectOfType<Motory>();
		gameUIController = FindObjectOfType<GameUIController>();
		gunController = FindObjectOfType<GunController>();
		alarmManager = FindObjectOfType<AlarmManager>();


		enginAudio = GameObject.Find ("MasterController/EnginAudio").GetComponent<AudioSource> ();
		effectAudio = GameObject.Find ("MasterController/EffectsAudio").GetComponent<AudioSource> ();
		masterAudio = GameObject.Find ("MasterController/Master Audio").GetComponent<AudioSource> ();
		speakAudio = GameObject.Find ("MasterController/CharactorAudio").GetComponent<AudioSource> ();
		turboAudio = GameObject.Find ("MasterController/TurboAudio").GetComponent<AudioSource> ();

		CM1 = FindObjectOfType<CinemachineVirtualCamera>();
		canvas = GameObject.Find("Canvas");
		loadingPage = GameObject.Find("gameLoading");
		musicAudio = GameObject.Find("BackGroundMusic");

		checkPointRocks = rockCount;
		
		bulletCount = PlayerPrefs.GetInt ("Bullet");
		shieldCount = PlayerPrefs.GetInt ("Shield");

		LoadCheckPoint ();
		Setdata ();
		MuteSound();

	}

	public void Setdata()
	{

		coinText.text = coinCount + "";
		bulletText.text = bulletCount + "";
		ShieldText.text = shieldCount + "";
		rockText.text = rockCount + "";
	}

	public void IncreaseHeart(int num)
    {
		if (heartCount <= 0)
			heartCount = 0;
		heartCount += num;
		heartText.text = heartCount + "";
    }
	public void DecreaseHeart(int damage)
    {

		
		if (heartCount > 0 && canDie)
		{
			canDie = false;
			heartCount -= damage;
			Debug.Log("Damage");
			effectAudio.clip = gameSounds[2];
			effectAudio.Play();

			if (heartCount <=0)
            {
				Debug.Log("Dead");
				heartText.text = "0";
				endGame();
			}
			else
            {
				Debug.Log("Decrease");
				heartText.text = heartCount + "";
				player = GameObject.FindGameObjectWithTag("player");
				playerAnim = player.GetComponent<Animator>();
				playerAnim.Play("Damage");
				StartCoroutine("DamageDelay");
			}
		
		}

	}

	public void endGame()
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
			alarmManager.PlayCharactorVoice(Random.Range(0,5));

			turboSc.backJoint.enabled = false;
			turboSc.frontJoint.enabled = false;
			
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


		turbo = GameObject.Find ("TakeOff");
		turboSc = (Turbo)turbo.GetComponent (typeof(Turbo));
		turboSc.SetTaregt ();

		if (gunController.gunIsUsed) {
			gameUIController.GunBtn ();
		}

		canDie = true;
		enginAudio.Play();
		rockCount = checkPointRocks;
		Setdata ();
		//SaveData();
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

	}

	public IEnumerator LoadingDelay()
	{
		loadingPage.SetActive(true);
		yield return new WaitForSeconds(1.1f);
		loadingPage.SetActive(false);
	}

	public IEnumerator DamageDelay()
	{
		yield return new WaitForSeconds(1.5f);
		canDie = true;
	}

	public void MuteSound()

	{
		if (PlayerPrefs.GetInt("sound") == 1)
        {
			Debug.Log("mute");
			masterAudio.mute = true;
			enginAudio.mute = true;
			speakAudio.mute = true;
			effectAudio.mute = true;
			turboAudio.mute = true;
        }
        else
        {
			Debug.Log("not mute");
			masterAudio.mute = false;
			enginAudio.mute = false;
			speakAudio.mute = false;
			effectAudio.mute = false;
			turboAudio.mute = false;
		}
		
	}

	private void DestroyCloneObjects()
	{
		rockObjects = GameObject.FindObjectsOfType<AutoDestroyObject> ();
		foreach (var item in rockObjects) {
			Destroy (item.gameObject);
		}
	}


}
