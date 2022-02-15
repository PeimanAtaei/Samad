using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour {

	private MasterController master;


	// Use this for initialization
	void Start () {
		master = FindObjectOfType<MasterController>();
		Debug.Log(master.canDie+"");
	}
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.CompareTag("ground") && master.canDie )
		{
			master.DecreaseHeart(10);
		}
			
	}
}
