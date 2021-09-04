using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmManager : MonoBehaviour
{
    // Start is called before the first frame update
	public Animator messegeAnim;
	public Text messegeText;
	public AudioSource audioSource;
	public AudioClip[] voices;

	public void ShowMessege(string newText)
	{
		messegeText.text = newText;
		messegeAnim.Play ("messegeAnim");
	}

	public void PlayCharactorVoice(int msg)
	{
		audioSource.clip = voices[msg];
		audioSource.Play();
	}

}
