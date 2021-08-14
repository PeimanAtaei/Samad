using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

	private MasterController masterController;
	private LevelQuestsManager questManager;
    public AudioSource coinSoundSource;
    public AudioClip coinsSound;
	public Animator rewardAnim;
	private bool canPick = true,isPicked = false;
	private GameObject target;
	private float speed = 70f;


    


	void Start () {
		masterController = FindObjectOfType<MasterController> ();
		questManager = FindObjectOfType<LevelQuestsManager> ();
		coinSoundSource = GameObject.Find ("Master Audio").GetComponent<AudioSource> ();
		target = GameObject.Find ("Canvas/TopItems/Items/Coins");
	}
	

	void Update () {
		if(isPicked)
		{
			target = GameObject.Find ("Canvas/TopItems/Items/Coins");
			float step = speed * Time.deltaTime;
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Camera.main.ScreenToWorldPoint(target.transform.position), step);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("player") && canPick == true)
		{
			canPick = false;
			coinSoundSource.clip = coinsSound;
			coinSoundSource.Play();
			masterController.coinCount ++;
			masterController.Setdata ();
			rewardAnim = GameObject.Find ("Canvas/TopItems/Items/Coins").GetComponent<Animator> ();
			rewardAnim.Play ("topCoinsItem");
			isPicked = true;
			questManager.CheckCoinQuests();
			StartCoroutine ("PickDelay");
		}

	}

	public IEnumerator PickDelay()
	{
		yield return new WaitForSeconds(1.5f);
		Destroy (gameObject);
	}
   


}
