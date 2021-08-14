using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet : MonoBehaviour
{
	public float speed = 20f;
	public int damage = 40;
	public Rigidbody2D rb;
	public Animator anim;
	public GameObject empactPrefabe;
    // Start is called before the first frame update
    void Start()
    {
		rb.velocity = transform.right * speed;
		StartCoroutine ("Destroy");
    }

    // Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "ground") {
			Instantiate (empactPrefabe, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
	public IEnumerator Destroy()
	{

		yield return new WaitForSeconds(1f);
		Destroy (gameObject);

	}
}
