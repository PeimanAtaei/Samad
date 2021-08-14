using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
	public float time;
    void Start()
    {
		StartCoroutine ("Destroy");
    }
	public IEnumerator Destroy()
	{

		yield return new WaitForSeconds(time);
		Destroy (gameObject);

	}
}
