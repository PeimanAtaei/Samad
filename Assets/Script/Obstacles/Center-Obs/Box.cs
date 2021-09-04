using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private MasterController masterController;
    public AudioSource audioSource;
	private GameObject rewardObj;
	public Animator anim, rewardAnim;
	public bool canReward,canDestroy=true;
	public string reward;
	public int rewardCount;
	public GameObject[] prefabs;

	// Start is called before the first frame update
	void Start()
    {
        masterController = FindObjectOfType<MasterController>();
		anim = gameObject.GetComponent<Animator>();
		audioSource = GameObject.Find("MasterController/Master Audio").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {

            case "bullet":
                {
					if(canDestroy)
                    {
						canDestroy = false;
						Debug.Log("Box Attack");
						StartCoroutine("ExplotionDelay");
					}
					break;
				}
        }
    }

    public IEnumerator ExplotionDelay()
    {
        anim.Play("BoxExplotion");
        
        if (canReward)
        {
            canReward = false;
            InsertReward();
            yield return new WaitForSeconds(0.26f);
            Destroy(rewardObj);
            Destroy(gameObject);
        }
    }

	private void InsertReward()
	{
		switch (reward)
		{
			case "bullet":
				{
					rewardObj =
						Instantiate(prefabs[0],
							gameObject.transform.position,
							gameObject.transform.rotation) as GameObject;
					
					masterController.bulletCount += rewardCount;
					masterController.Setdata();
					rewardAnim = GameObject.Find("Canvas/TopItems/Items/Bullet").GetComponent<Animator>();
					rewardAnim.Play("topBulletItem");

					break;
				}
			case "shield":
				{
					rewardObj =
						Instantiate(prefabs[1],
							gameObject.transform.position,
							gameObject.transform.rotation) as GameObject;
					
					masterController.shieldCount += rewardCount;
					masterController.Setdata();
					rewardAnim = GameObject.Find("Canvas/TopItems/Items/Shield").GetComponent<Animator>();
					rewardAnim.Play("topShieldItem");

					break;
				}
			case "rock":
				{
					rewardObj =
						Instantiate(prefabs[2],
							gameObject.transform.position,
							gameObject.transform.rotation) as GameObject;
					
					masterController.rockCount += rewardCount;
					masterController.Setdata();
					rewardAnim = GameObject.Find("Canvas/TopItems/Items/Rock").GetComponent<Animator>();
					rewardAnim.Play("topRockItem");

					break;
				}
			case "coins":
				{
					rewardObj =
						Instantiate(prefabs[3],
							gameObject.transform.position,
							gameObject.transform.rotation) as GameObject;
					
					masterController.coinCount += rewardCount;
					masterController.Setdata();
					rewardAnim = GameObject.Find("Canvas/TopItems/Items/Coins").GetComponent<Animator>();
					rewardAnim.Play("topCoinsItem");

					break;
				}
		}
	}
}
