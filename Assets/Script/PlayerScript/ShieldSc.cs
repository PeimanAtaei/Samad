using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSc : MonoBehaviour
{

	public GameObject explotionPrefabe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "neizeh":
				{
					StartCoroutine("InstantiateExplotion", other.gameObject);
					Destroy(other.gameObject);
					break;
				}
			case "box":
				{
					StartCoroutine("InstantiateExplotion", other.gameObject);
					Destroy(other.gameObject);
					break;
				}

		}
	}

	private IEnumerator InstantiateExplotion(GameObject other)
    {
		GameObject explotionInstance =
			Instantiate(explotionPrefabe,
				other.transform.position,
				other.transform.rotation) as GameObject;
		yield return new WaitForSeconds(0.2f);
		Destroy(explotionInstance);

	}
}
