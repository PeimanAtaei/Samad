﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExployScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine ("Effect");
    }

	public IEnumerator Effect()
	{

		yield return new WaitForSeconds(0.2f);
		Destroy (gameObject);

	}
}
