using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TighUp : MonoBehaviour {

	public Animator[] tighAnim;

	void OnTriggerEnter2D(Collider2D other)
	{
		for (int i = 0; i < tighAnim.Length; i++) {
			tighAnim [i].Play ("SmallTigh");
		}

	}
}
