using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atis : MonoBehaviour {

	private 		Rigidbody2D atisRB;
	public bool 	isAlive = true;
    public AudioClip orgClip, goblinClip, skletonKingClip,currentClip;
    public AudioSource motorAudio;
    public GameObject coinPlus;
    Motory thePlayer;

	void Start () {
		atisRB = GetComponent<Rigidbody2D> ();
        thePlayer = FindObjectOfType<Motory>();
        if(gameObject.tag == "org")
        {
            Debug.Log("org");
            currentClip = orgClip;
        }
        else if (gameObject.tag == "skletonKing")
        {
            currentClip = skletonKingClip;
        }
        else if (gameObject.tag == "goblin")
        {
            Debug.Log("goblin");
            currentClip = orgClip;
        }
	}

	void Update () {
		//transform.Translate (speed*Time.deltaTime,0,0);
		//Walking();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (gameObject.tag == "bats" && isAlive)
        {
            isAlive = false;
            atisRB.constraints = RigidbodyConstraints2D.None;
            motorAudio.clip = currentClip;
            motorAudio.Play();
            //StartCoroutine("CoinPlus");
        }
		if (other.CompareTag ("player") && isAlive) {
			isAlive = false;
			atisRB.AddForce(new Vector2(100,0), ForceMode2D.Impulse);
            motorAudio.clip = currentClip;
            motorAudio.Play();
			Debug.Log("death");
            StartCoroutine("CoinPlus");
		}

        if (gameObject.name == "green_body")
        {
            atisRB.constraints = RigidbodyConstraints2D.None;
        }

	}
		
}
