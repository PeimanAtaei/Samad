using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLands : MonoBehaviour {

	public GameObject objectToMove;
	public Transform startObj, endObj;
	public float moveSpeed = 2f;
	Vector3 currentPosition;


	void Start () {

		currentPosition = endObj.position;
		
	}
	

	void Update () {

		objectToMove.transform.position = Vector3.MoveTowards (objectToMove.transform.position,currentPosition,moveSpeed*Time.deltaTime);

		if(objectToMove.transform.position == endObj.position)
		{
			currentPosition = startObj.position;
		}
		else if(objectToMove.transform.position == startObj.position)
		{
			currentPosition = endObj.position;
		}

	}
}
