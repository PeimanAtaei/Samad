using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonSc : MonoBehaviour
{
    // Start is called before the first frame update
	private MapManager mapManager;
	public int levelNumber;
	public int star;
	public GameObject[] stars;
	private Vector2 scale;
	private Animator startDialog;
	private Text startDialogTitle;
    void Start()
    {
		mapManager = FindObjectOfType<MapManager> ();
		GetCurrentLevel ();
		ShowStarts ();
		startDialog = GameObject.Find ("Canvas/StartDialog").GetComponent<Animator> ();
		startDialogTitle = GameObject.Find ("Canvas/StartDialog/Title").GetComponent<Text> ();

    }
		

	private void GetCurrentLevel()
	{
		Debug.Log (PlayerPrefs.GetInt("currentLevel"));
		if(levelNumber-1 <= (PlayerPrefs.GetInt("currentLevel")))
		{
			scale = new Vector2 (1,1);
			gameObject.transform.localScale = scale;
		}
	}

	private void ShowStarts()
	{
		star = PlayerPrefs.GetInt ("starLevel"+levelNumber);
		if (star >= 1)
			stars [0].SetActive (true);
		if (star >= 2)
			stars [1].SetActive (true);
		if (star >= 3)
			stars [2].SetActive (true);
	}

	public void OnSelectLevel()
	{
		startDialog.Play ("OpenStartDialog");
		mapManager.SelectedLevel = levelNumber;
		startDialogTitle.text = "LEVEL " + levelNumber;
	}
}
