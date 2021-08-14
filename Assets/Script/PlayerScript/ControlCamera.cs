using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCamera : MonoBehaviour {

	public GameObject cameraTarget = null;
    public bool endGame = false;

	void Update () {

		if (cameraTarget == null)
			cameraTarget = GameObject.Find ("Player");
		else if(!endGame)
		{
					transform.position = new Vector3(cameraTarget.transform.position.x + 22, cameraTarget.transform.position.y, transform.position.z);
		}
			
	}
}
