using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class HelpSc : MonoBehaviour
{
    public GameObject[] sections;
    public VideoPlayer vid;
    public VideoClip[] vidClip;
    private int step=0;
    public GameObject next;
    public Animator transmitPage;

    public void Start()
    {
        StartCoroutine("NextButtonDelay");
    }

    public IEnumerator NextButtonDelay()
    {
        next.SetActive(false);
        yield return new WaitForSeconds(5f);
        next.SetActive(true);
    }

    public void NextBtnClick()
    {
        switch (step)
        {
            case 0:
                {
                    step++;
                    StartCoroutine("NextButtonDelay");
                    vid.clip = vidClip[0];
                    vid.Play();
                    sections[1].SetActive(true);
                    sections[0].SetActive(false);
                    break;
                }
            case 1:
                {
                    step++;
                    StartCoroutine("NextButtonDelay");
                    vid.clip = vidClip[1];
                    vid.Play();
                    sections[1].SetActive(false);
                    sections[2].SetActive(true);
                    break;
                }
            case 2:
                {
                    step++;
                    StartCoroutine("NextButtonDelay");
                    vid.Pause();
                    sections[2].SetActive(false);
                    sections[3].SetActive(true);
                    break;
                }
            case 3:
                {
                    step++;
                    StartCoroutine("NextButtonDelay");
                    sections[3].SetActive(false);
                    sections[4].SetActive(true);
                    break;
                }
            case 4:
                {
                    step++;
                    StartCoroutine("NextButtonDelay");
                    sections[4].SetActive(false);
                    sections[5].SetActive(true);
                    sections[6].SetActive(true);
                    break;
                }
            case 5:
                {
                    step++;
                    StartCoroutine("NextButtonDelay");
                    sections[6].SetActive(false);
                    sections[7].SetActive(true);
                    break;
                }
            case 6:
                {
                    step++;
                    StartCoroutine("NextButtonDelay");
                    sections[7].SetActive(false);
                    sections[8].SetActive(true);
                    break;
                }
            case 7:
                {
                    step++;
                    StartCoroutine("NextButtonDelay");
                    sections[8].SetActive(false);
                    sections[9].SetActive(true);
                    break;
                }
            case 8:
                {
                    StartCoroutine("MenuLoadingDelay", "menu");
                    break;
                }
        }
    }

    public IEnumerator MenuLoadingDelay(string levelName)
    {
        transmitPage.Play("TransmitFadeIn");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelName);
    }

}
