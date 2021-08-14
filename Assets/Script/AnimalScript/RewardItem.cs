using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardItem : MonoBehaviour {

	public GameObject target;
	private float speed = 50f;
	public string name;

	void Start()
	{
		//target = GameObject.Find ("Canvas/TopItems/RewardPoints/BPoint");
		DetectTarget();


	}
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position!=(Camera.main.ScreenToWorldPoint(target.transform.position))){
			DetectTarget ();
			float step = speed * Time.deltaTime;
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Camera.main.ScreenToWorldPoint(target.transform.position), step);
		}
	}

	private void DetectTarget()
	{
		switch (name) {

		case "bullet":
			{
				target = GameObject.Find ("Canvas/TopItems/Items/Bullet");
				break;
			}
		case "shield":
			{
				target = GameObject.Find ("Canvas/TopItems/Items/Shield");
				break;
			}

		case "rock":
			{
				target = GameObject.Find ("Canvas/TopItems/Items/Rock");
				break;
			}

		case "coins":
			{
				target = GameObject.Find ("Canvas/TopItems/Items/Coins");
				break;
			}


		}
	}

}
