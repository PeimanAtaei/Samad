using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour {

	private MasterController masterController;
	private GameObject motory;
	private bool canDie = true;


	// Use this for initialization
	void Start () {
		masterController = FindObjectOfType<MasterController>();
	}
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.CompareTag("ground") && canDie )
		{
			canDie = false;
			masterController.endGame ();
			Time.timeScale = 0.05f;

			//Destroy (GameObject.Find("motor(Clone)"));

		}

		else if (other.CompareTag("roof") && canDie)
		{
			canDie = false;
			masterController.endGame ();
			Time.timeScale = 0.05f;

			//Destroy (GameObject.Find("motor(Clone)"));
		}
			
	}
}
