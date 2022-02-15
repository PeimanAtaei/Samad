using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

	public int SelectedLevel = 1;
	private Animator startDialog,loadingDialog,transmitPage,uiElements;
	public Text loadingText;
	public Slider loadingSlider;
	public bool loadingNewScene = false;

    // Start is called before the first frame update
    void Start()
    {
		startDialog = GameObject.Find ("Canvas/UI Elements/StartDialog").GetComponent<Animator> ();
		loadingDialog = GameObject.Find ("Canvas/UI Elements/LoadGamePage").GetComponent<Animator> ();
		transmitPage = GameObject.Find ("Canvas/UI Elements/TransmitPage").GetComponent<Animator> ();
		uiElements = GameObject.Find ("Canvas/UI Elements").GetComponent<Animator> ();

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

	public void CancelLevel()
    {
		startDialog.Play("CloseStartDialog");
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

	public void UpdatedAlarm(int num)
    {
		if (num == 1)
			uiElements.Play("UpdateAlarmShow");
		else
			uiElements.Play("UpdateAlarmClose");
	}
	private IEnumerator SceneLoadingDelay(string levelName)
	{
		transmitPage.Play ("TransmitFadeIn");
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(levelName);
	}

}
