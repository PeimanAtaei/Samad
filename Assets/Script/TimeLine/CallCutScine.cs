using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCutScine : MonoBehaviour
{
	private CutScinesManager scinesManager;

	public int scineNumber,duration;
	
    void Start()
    {
		scinesManager = FindObjectOfType<CutScinesManager>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{

			case "player":
				{
					scinesManager.PlayCutScine(scineNumber, duration);
					StartCoroutine("Destroy");
					break;
				}
		}
	}

	public IEnumerator Destroy()
	{
		
		yield return new WaitForSeconds(2f);
		gameObject.SetActive(false);
	}
}
