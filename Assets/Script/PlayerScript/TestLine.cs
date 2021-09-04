using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLine : MonoBehaviour {
	
	
	public Transform baseDot;
	public bool canInstantiate = true,canSayDialog = true;
	private Vector3 mousepos;
	private MasterController master;


	private void Start()
	{
		master = FindObjectOfType<MasterController> ();
	}
	void FixedUpdate() {

		/*if(canInstantiate)
		{
			Vector2 myTouchPos = new Vector2 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y);
			Vector2 objPos = Camera.main.ScreenToWorldPoint (myTouchPos);

			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved ) {

				Instantiate (baseDot,objPos,baseDot.rotation);
			}
		}*/

		if(Input.GetMouseButton(0) && canInstantiate && master.rockCount>0){
			Debug.Log("Mouse button is pressed");
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

			Instantiate (baseDot,mousePosition,Quaternion.identity);
			master.rockCount--;
			master.Setdata ();
		}
		else if(master.rockCount<=0 && canSayDialog)
        {
			canSayDialog = false;
			master.alarmManager.PlayCharactorVoice(6);
			master.alarmManager.ShowMessege("You Have no stone");
        }

		else if (master.rockCount > 0 && !canSayDialog)
		{
			canSayDialog = true;
		}

	}
}
