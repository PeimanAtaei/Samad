using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Motory : MonoBehaviour {


	public bool canDie = true, shield = false;
	private MasterController masterController;


	void Start () {
		masterController = FindObjectOfType<MasterController>();
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{

			case "neizeh":
				{
					if (!shield)
					{
						canDie = false;
						masterController.DecreaseHeart(1);
					}
					break;
				}

			case "checkPoint":
				{
					masterController.lastCheckPoint = other.transform;
					masterController.checkPointRocks = masterController.rockCount;
					if (masterController.saveSound)
					{
						masterController.saveSound = false;
						masterController.alarmManager.PlayCharactorVoice(8);
						masterController.alarmManager.ShowMessege("Game Saved");
					}
					//masterController.Setdata ();
					break;
				}
			case "finishPoint":
				{
					masterController.FinishGame();
					break;
				}

		}
	}
		
}


   
