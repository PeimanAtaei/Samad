using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	private MasterController master;
	private GameUIController uiController;
	public Joystick joystick;
	public GameObject gun,joystickObj,bulletPrefabe;
	public Vector3 joy;
	public bool gunIsUsed = false;
	public Animator gunAnim;
	public AudioSource gunSounds;
	public AudioClip[] shootingSounds;
	private Transform firePoint;
	private TestLine addLine;
	//private TestLine addLineSc;



    // Start is called before the first frame update
    void Start()
    {
		joystick = FindObjectOfType<Joystick> ();
		master = FindObjectOfType<MasterController> ();
		uiController = FindObjectOfType<GameUIController> ();
		addLine = FindObjectOfType<TestLine> ();
		//addLineSc = FindObjectOfType<TestLine> ();

		gun = GameObject.Find ("Player/Gun");
		firePoint = GameObject.Find ("Player/Gun/firePoint").GetComponent<Transform>();
		gunAnim = GameObject.Find ("Player/Gun").GetComponent<Animator> ();
		gunSounds = GameObject.Find ("Player/Gun/gunSounds").GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
		//angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
		if (joystick == null || firePoint==null || gun==null || gunSounds==null ) {
			FindGunObjects ();
			
		} else if (gunIsUsed && joystick.Vertical != 0 && joystick.Horizontal != 0) {
			Shooting ();

		} else if (joystick.Vertical == 0 && joystick.Horizontal == 0 & gunAnim.GetCurrentAnimatorStateInfo (0).IsName ("GunShoot")) {
			gunAnim.PlayInFixedTime ("GunIdel");
			if (gunSounds.isPlaying)
				gunSounds.Stop ();
			addLine.canInstantiate = true;
		}
			

    }

	public void FindGunObjects()
	{
		joystick = FindObjectOfType<Joystick> ();
		gun = GameObject.Find ("Player/Gun");
		gunSounds = GameObject.Find ("Player/Gun/gunSounds").GetComponent<AudioSource> ();
		gunAnim = GameObject.Find ("Player/Gun").GetComponent<Animator> ();
		firePoint = GameObject.Find ("Player/Gun/firePoint").GetComponent<Transform>();
	}


	private void Shooting()
	{
		addLine.canInstantiate = false;
		if (master.bulletCount > 0) {
			Instantiate (bulletPrefabe, firePoint.position, firePoint.rotation);
			gunAnim.PlayInFixedTime ("GunShoot");
			master.bulletCount--;
			master.Setdata ();
			if (!gunSounds.isPlaying) {
				gunSounds.loop = true;
				gunSounds.clip = shootingSounds [1];
				gunSounds.Play ();
			}
		} else {
			uiController.GunBtn ();
		}
		joy = new Vector3 (0, 0, joystick.Vertical * 60);
		gun.transform.rotation = Quaternion.Euler (joy);
	}
}
