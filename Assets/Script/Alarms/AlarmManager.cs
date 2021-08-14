using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmManager : MonoBehaviour
{
    // Start is called before the first frame update
	public Animator messegeAnim;
	public Text messegeText,charactorText;

	public void ShowMessege(string newText)
	{
		messegeText.text = newText;
		messegeAnim.Play ("messegeAnim");
	}

	public void ShowCharactorChat(string newText,int voice)
	{
		charactorText.text = newText;
		StartCoroutine("ChatDelay");
	}

	public IEnumerator ChatDelay()
	{
		messegeAnim.Play("chatOpenAnim");
		yield return new WaitForSeconds(10f);
		messegeAnim.Play("chatCloseAnim");
	}
}
