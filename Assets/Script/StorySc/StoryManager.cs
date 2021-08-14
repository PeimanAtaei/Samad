using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    // Start is called before the first frame update

	public int sceneNumber = 0,sceneCount = 0;
	public Animator storyAnim,transmitPage;
	public GameObject nextButton;
	public AudioClip[] Voices;
	public AudioSource audioSource;
    void Start()
    {
		transmitPage = GameObject.Find ("Canvas/TransmitPage").GetComponent<Animator> ();
		PlayScene ();
    }

	public void NextScene()
	{
		nextButton.SetActive(false);
		sceneNumber++;
		PlayScene ();
		audioSource.clip = Voices[sceneNumber];
		audioSource.Play();

	}

	private void PlayScene()
	{
		if(sceneNumber == sceneCount)
		{
			LoadMap ();
			PlayerPrefs.SetInt ("FirstStory", 1);
		}
		else
		{
			string sceneName = "Scene" + sceneNumber;
			storyAnim.Play (sceneName);
			StartCoroutine ("ShowNextButton");
		}
	}

	public IEnumerator ShowNextButton()
	{
		
		yield return new WaitForSeconds(5f);
		nextButton.SetActive(true);
	}

	public void LoadMap()
	{
		StartCoroutine("MenuLoadingDelay","map");
	}

	public IEnumerator MenuLoadingDelay(string levelName)
	{
		transmitPage.Play ("TransmitFadeIn");
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(levelName);
	}
}
