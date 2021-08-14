using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour {

    private bool radioOn = false;
    public GameObject Radio;
    public AudioSource RadioSource;
    public AudioClip[] newClips;
    public List<AudioClip> clips;
    public int musicCount = 0;
    public static RadioManager Instance;

	// Use this for initialization
	void Start () {

        AddMusics();

        RadioSource.clip = clips[0];

	}
	

    public void StartRadio()
    {
        
        if (!radioOn)
        {
            RadioSource.Play();
            radioOn = !radioOn;
        }
    }

    public void StopRadio()
    {
        if (radioOn)
        {
            RadioSource.Pause();
            radioOn = !radioOn;
        }
    }

    public void nextMusic()
    {
        if (musicCount == clips.Count-1)
        {
            musicCount = 0;
        }
        else
        {
            musicCount++;
        }

        RadioSource.clip = clips[musicCount];
        RadioSource.Play();
    }

    public void lastMusic()
    {
        if (musicCount == 0)
        {
            musicCount = clips.Count - 1;
        }
        else
        {
            musicCount--;
        }

        RadioSource.clip = clips[musicCount];
        RadioSource.Play();
    }

    private void AddMusics()
    {

        Debug.Log("add music");
    
        if (PlayerPrefs.GetInt("Hendy") == 1)
        {
            clips.Add(newClips[0]);
        }

        if (PlayerPrefs.GetInt("Torky") == 1)
        {
            clips.Add(newClips[1]);
        }
        
    }
}
