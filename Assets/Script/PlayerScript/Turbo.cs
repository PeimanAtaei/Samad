using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : MonoBehaviour {

	public AudioSource motorSound = null;
	public Animator cameraAnim,fireAnim;
	public WheelJoint2D backJoint , frontJoint ;
	private JointMotor2D backMotor , frontMotor ;
	private TestLine addLine;
	private float backSpeed, frontSpeed;

	private void Start()
	{
		addLine = FindObjectOfType<TestLine> ();
	}

	private void Update()
	{
		if (backJoint == null) {
			SetTaregt ();
		}
	}

	public void SetTaregt()
	{
		Debug.Log ("Turbo set target");
		motorSound = GameObject.Find ("MasterController/TurboAudio").GetComponent<AudioSource> ();
		cameraAnim = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Animator>();
		fireAnim = GameObject.Find("Player/Fire").GetComponent<Animator>();

		backJoint = GameObject.Find ("Player/motor_back_wheel").GetComponent<WheelJoint2D> ();
		frontJoint = GameObject.Find ("Player/motor_front_wheel").GetComponent<WheelJoint2D> ();

		backMotor = backJoint.motor;
		frontMotor = frontJoint.motor;
		frontSpeed = frontMotor.motorSpeed;
		backSpeed = backMotor.motorSpeed;

	}

	public void onPress()
	{
		
		addLine.canInstantiate = false;
		backMotor.motorSpeed = backSpeed*2.5f;
		backMotor.maxMotorTorque = 10000;

		frontMotor.motorSpeed = frontSpeed*1.5f;
		frontMotor.maxMotorTorque = 10000;

		backJoint.motor = backMotor;
		frontJoint.motor = frontMotor;

		motorSound.Play();
	
		cameraAnim.Play ("TurboZoom");
		fireAnim.Play("FireStart");

		Debug.Log ("press");
	}
	public void onRelease()
	{
		addLine.canInstantiate = true;

		backMotor.motorSpeed = backSpeed*1.5f;
		backMotor.maxMotorTorque = 10000;

		frontMotor.motorSpeed = frontSpeed;
		frontMotor.maxMotorTorque = 10000;

		backJoint.motor = backMotor;
		frontJoint.motor = frontMotor;
		Debug.Log ("release");

		cameraAnim.Play ("TurboZoomOut");
		fireAnim.Play("FireFinish");

		motorSound.Stop ();

	}
}
