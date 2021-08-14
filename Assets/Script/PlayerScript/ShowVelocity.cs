using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVelocity : MonoBehaviour {

	Vector3 lastPosition = Vector3.zero;
	public float speed;
	public GameObject flash = null;
	public Transform tr;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		speed = (transform.position - lastPosition).magnitude;
		lastPosition = transform.position;
		if(flash == null)
			flash = GameObject.Find ("Canvas/Dashboard/board/speed/flash");
		
		else if (speed > 0) {
			tr = flash.GetComponent<Transform> ();
			tr.eulerAngles = new Vector3 (0, 0, 150 - (speed * 100));
		}
	}
}
