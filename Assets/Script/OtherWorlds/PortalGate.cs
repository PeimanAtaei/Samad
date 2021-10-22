using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalGate : MonoBehaviour
{

    private Animator playerAnim;
    private Rigidbody2D playerRigid;
    private bool canEnter = true;
    public string worldName;
    public int portalNumber;
    private Animator loadingPage, transmitPage;
    public Text loadingText;
    public Slider loadingSlider;
    public bool loadingNewScene = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("portal" + portalNumber) == 1)
        {
            gameObject.SetActive(false);
        }

        loadingPage = GameObject.Find("Canvas/LoadGamePage").GetComponent<Animator>();
        transmitPage = GameObject.Find("Canvas/TransmitPage").GetComponent<Animator>();

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player" && canEnter)
        {
            canEnter = false;
            Debug.Log("Portal");
            
            playerAnim = other.GetComponent<Animator>();
            playerRigid = other.GetComponent<Rigidbody2D>();

            playerRigid.constraints = RigidbodyConstraints2D.FreezeAll;
            playerAnim.Play("Portal");

            PlayerPrefs.SetInt("portal" + portalNumber, 1);

            StartLevel();
        }
    }

    public void StartLevel()
    {
        if (!loadingNewScene)
        {
            loadingNewScene = true;
            StartCoroutine(LoadingNewScene());
        }
    }

    IEnumerator LoadingNewScene()
    {
        
        yield return new WaitForSeconds(2);
        loadingPage.Play("CloseLoadingPage");

        yield return new WaitForSeconds(4);
        AsyncOperation async = SceneManager.LoadSceneAsync(worldName);
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            loadingSlider.value = progress;
            loadingText.text = progress * 100f + "%";
            yield return null;
        }
    }


}
