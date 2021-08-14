using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

	public int SelectedLevel = 1;
	private Animator startDialog,loadingDialog,transmitPage;
	public Text loadingText;
	public Slider loadingSlider;
	public bool loadingNewScene = false;

    // Start is called before the first frame update
    void Start()
    {
		startDialog = GameObject.Find ("Canvas/StartDialog").GetComponent<Animator> ();
		loadingDialog = GameObject.Find ("Canvas/LoadGamePage").GetComponent<Animator> ();
		transmitPage = GameObject.Find ("Canvas/TransmitPage").GetComponent<Animator> ();

    }
		

	public void StartLevel()
	{
		if (!loadingNewScene) {
			loadingNewScene = true;
			startDialog.Play ("CloseStartDialog");
			loadingDialog.Play ("CloseLoadingPage");
			StartCoroutine (LoadingNewScene());
		}
	}

	IEnumerator LoadingNewScene()
	{
		yield return new WaitForSeconds(1);

		AsyncOperation async = SceneManager.LoadSceneAsync ("level"+SelectedLevel);
		while(!async.isDone)
		{
			float progress = Mathf.Clamp01 (async.progress / 0.9f);
			loadingSlider.value = progress;
			loadingText.text = 	progress*100f +"%";
			yield return null;
		}
	}

	public void OnBackClick()
	{
		StartCoroutine ("SceneLoadingDelay","menu");
	}

	public void OnStoryClick()
	{
		StartCoroutine ("SceneLoadingDelay","FirstStory");
	}

	private IEnumerator SceneLoadingDelay(string levelName)
	{
		transmitPage.Play ("TransmitFadeIn");
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(levelName);
	}

}
