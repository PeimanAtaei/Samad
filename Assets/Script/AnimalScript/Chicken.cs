using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {

	private MasterController masterController;
	private LevelQuestsManager questManager;
	private AlarmManager alarmManager;
	public Animator anim,rewardAnim;
	public AudioClip[] chickenSounds;
	public AudioSource audioSource,chickenSource;
	public string reward;
	public int rewardCount;
	private GameObject rewardObj;
	public GameObject[] prefabs; 
	private bool canReward = true;
	// Use this for initialization
	void Start () {
		masterController = FindObjectOfType<MasterController>();
		questManager = FindObjectOfType<LevelQuestsManager>();
		anim = gameObject.GetComponent<Animator> ();
		audioSource = GameObject.Find ("MasterController/Master Audio").GetComponent<AudioSource>();
		
	}
	

	void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag) {

		case "player":
			{
				StartCoroutine ("ExplotionDelay");
				break;
			}
		}
	}

	public IEnumerator ExplotionDelay()
	{
		chickenSource.clip = chickenSounds [0];
		chickenSource.loop = false;
		chickenSource.Play ();
		anim.Play ("Explotion");
		PlayerPrefs.SetInt("totalChicken", PlayerPrefs.GetInt("totalChicken")+1);
		if (canReward) {
			canReward = false;
			InsertReward ();
			yield return new WaitForSeconds(1f);
			Destroy (rewardObj);
			Destroy (gameObject);
		}
		questManager.CheckChickenQuests();
		
	}

	private void InsertReward()
	{
		switch(reward)
		{
			case "bullet":
			{
				rewardObj =
					Instantiate(prefabs[0],
						gameObject.transform.position,
						gameObject.transform.rotation) as GameObject;
				audioSource.clip = chickenSounds [1];
				audioSource.Play ();
				masterController.bulletCount += rewardCount;
				masterController.Setdata ();
				rewardAnim = GameObject.Find ("Canvas/TopItems/Items/Bullet").GetComponent<Animator> ();
				rewardAnim.Play ("topBulletItem");

				break;
			}
		case "shield":
			{
				rewardObj =
					Instantiate(prefabs[1],
						gameObject.transform.position,
						gameObject.transform.rotation) as GameObject;
				audioSource.clip = chickenSounds [2];
				audioSource.Play ();
				masterController.shieldCount += rewardCount;
				masterController.Setdata ();
				rewardAnim = GameObject.Find ("Canvas/TopItems/Items/Shield").GetComponent<Animator> ();
				rewardAnim.Play ("topShieldItem");

				break;
			}
		case "rock":
			{
				rewardObj =
					Instantiate(prefabs[2],
						gameObject.transform.position,
						gameObject.transform.rotation) as GameObject;
				audioSource.clip = chickenSounds [3];
				audioSource.Play ();
				masterController.rockCount += rewardCount;
				masterController.Setdata ();
				rewardAnim = GameObject.Find ("Canvas/TopItems/Items/Rock").GetComponent<Animator> ();
				rewardAnim.Play ("topRockItem");

				break;
			}
		case "coins":
			{
				rewardObj =
					Instantiate(prefabs[3],
						gameObject.transform.position,
						gameObject.transform.rotation) as GameObject;
				audioSource.clip = chickenSounds [4];
				audioSource.Play ();
				masterController.coinCount += rewardCount;
				masterController.Setdata ();
				rewardAnim = GameObject.Find ("Canvas/TopItems/Items/Coins").GetComponent<Animator> ();
				rewardAnim.Play ("topCoinsItem");

				break;
			}
		}
	}

}
