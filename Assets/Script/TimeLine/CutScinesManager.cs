using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScinesManager : MonoBehaviour
{
	// Start is called before the first frame update
	private MasterController master;
	private Turbo turbo;
	public GameObject[] cutScines;
	public bool canPlay = true;
	private int scineNumber, scineDuration;


	void Start()
	{
		master = FindObjectOfType<MasterController>();
		turbo = FindObjectOfType<Turbo>();
	}

	public void PlayCutScine(int scineNum, int duration)
    {
		scineNumber = scineNum;
		scineDuration = duration;
		turbo.onRelease();
		canPlay = false;
		if (!canPlay)
        {
			StartCoroutine("FinishScene");
		}
	}

	public IEnumerator FinishScene()
	{
		cutScines[scineNumber].SetActive(true);
		turbo.backMotor.motorSpeed = turbo.backSpeed/10;
		turbo.frontMotor.motorSpeed = turbo.frontSpeed/10;
		turbo.backJoint.motor = turbo.backMotor;
		turbo.frontJoint.motor = turbo.frontMotor;

	
		yield return new WaitForSeconds(scineDuration);


		turbo.backMotor.motorSpeed = turbo.backSpeed;
		turbo.frontMotor.motorSpeed = turbo.frontSpeed;
		turbo.backJoint.motor = turbo.backMotor;
		turbo.frontJoint.motor = turbo.frontMotor;

		cutScines[scineNumber].SetActive(false);
		master.CM1.Follow = GameObject.FindGameObjectWithTag("player").transform;
		canPlay = true;

	}

	


}
