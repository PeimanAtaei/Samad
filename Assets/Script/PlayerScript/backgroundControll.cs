using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundControll : MonoBehaviour {

	public GameObject target = null;
	public float moveSpeed;


	// Use this for initialization
	void Start () {
		transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, target.transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
		if (target == null)
			SetTarget ();
		else
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, target.transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);
	}

	public void SetTarget()
	{
		target = GameObject.Find ("Main Camera");
	}
}
