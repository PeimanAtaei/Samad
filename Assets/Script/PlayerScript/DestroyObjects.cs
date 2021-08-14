using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(other.gameObject);
        other.gameObject.SetActive(false);
        Debug.Log("Destroy : "+other.tag);
    }
}
