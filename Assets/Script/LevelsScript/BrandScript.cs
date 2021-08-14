using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrandScript : MonoBehaviour {

    public string menuScene;
	void Start () {
        PlayerPrefs.SetInt("totalPlay", PlayerPrefs.GetInt("totalPlay")+1);
        StartCoroutine("BrandDelay");
	}

    public IEnumerator BrandDelay()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(menuScene);
    }

}
