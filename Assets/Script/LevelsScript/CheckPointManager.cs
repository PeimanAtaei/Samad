using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public int number;
    private PlatformManager platformManager;


    public void Start()
    {
		platformManager = FindObjectOfType<PlatformManager>();
	}


    void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{

			case "player":
				{
					platformManager.VisiblePlatform(number);
					break;
				}
		}
		
	}
}
