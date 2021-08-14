using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Motory : MonoBehaviour {

   
	public bool canDie = true,shield = false;
	private MasterController masterController;
	private LevelQuestsManager questManager;


	void Start () {
		masterController = FindObjectOfType<MasterController>();
		questManager = FindObjectOfType<LevelQuestsManager>();
	}


   void OnTriggerEnter2D(Collider2D other)
    {
		switch (other.tag) {
			
		case "neizeh":
			{
					if (!shield)
					{
						canDie = false;
						masterController.endGame();
						Time.timeScale = 0.05f;
					}
					break;
			}
			/*case "ground":
				{
					canDie = false;
					masterController.endGame ();
					Time.timeScale = 0.05f;
					break;
				}
			case "roof":
				{
					canDie = false;
					masterController.endGame ();
					Time.timeScale = 0.05f;
					break;
				}*/
			case "checkPoint":
			{
				masterController.lastCheckPoint = other.transform;
				masterController.checkPointRocks = masterController.rockCount;
				masterController.Setdata ();
				break;
			}
		case "finishPoint":
			{
				masterController.FinishGame ();
				break;
			}
			
		}
		/*if (other.CompareTag("neizeh") && canDie)
        {
			canDie = false;
			masterController.endGame();
			Time.timeScale = 0.05f;
			//Destroy (GameObject.Find("motor(Clone)"));
        }

		else if (other.CompareTag("ground") && canDie )
		{
			canDie = false;
			masterController.endGame ();
			Time.timeScale = 0.05f;
			//Destroy (GameObject.Find("motor(Clone)"));

		}

		else if (other.CompareTag("roof") && canDie)
		{
			canDie = false;
			masterController.endGame ();
			Time.timeScale = 0.05f;

			//Destroy (GameObject.Find("motor(Clone)"));
		}

		else if (other.CompareTag("checkPoint") && canDie)
		{
			
			masterController.lastCheckPoint = other.transform;
		}

		else if (other.CompareTag("finishPoint"))
		{
			masterController.FinishGame ();
		}

		else if (other.CompareTag("speaking"))
		{
			//int sNumber = Mathf.RoundToInt(Random.Range(0, 7.9f));
			//speakAudio.clip = speakSounds[sNumber];
			//speakAudio.Play();
			//Destroy(other.gameObject);
		}*/
    }
		

    /*void RecordManager()
    {
        recordText.text = (Mathf.RoundToInt((motor.transform.position.x)/10)) + "";
    }*/


    /*private void SetupOptions()
    {

        if (PlayerPrefs.GetInt("CurrentTire") == 2)
        {
            wheelRenderer = backWGO.GetComponent<SpriteRenderer>();
            wheelRenderer.sprite = optionSprites[0];
            wheelRenderer = frontBGO.GetComponent<SpriteRenderer>();
            wheelRenderer.sprite = optionSprites[0];

            Debug.Log("wheel sprite changed");
        }

		if (PlayerPrefs.GetInt("Casket") == 1)
		{
			casket.SetActive (true);

			Debug.Log("casket sprite changed");
		}

        if (PlayerPrefs.GetInt("CurrentMotor") == 2)
        {
            motorRendrer = motorGO.GetComponent<SpriteRenderer>();
            motorRendrer.sprite = optionSprites[1];
            Debug.Log("motor sprite changed to honda");
        }
        else if (PlayerPrefs.GetInt("CurrentMotor") == 3)
        {
            motorRendrer = motorGO.GetComponent<SpriteRenderer>();
            motorRendrer.sprite = optionSprites[2];
            Debug.Log("motor sprite changed to trail");
        }
        /*else if (PlayerPrefs.GetInt("CurrentMotor") == 4)
        {
            motorRendrer = motorGO.GetComponent<SpriteRenderer>();
            motorRendrer.sprite = optionSprites[3];
            Debug.Log("motor sprite changed to 1000");
        }*/
}


   
