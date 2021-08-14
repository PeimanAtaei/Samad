using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HouseController : MonoBehaviour
{
	private HouseQuestManager houseQuestManager;
	private Animator transmitPage,cameraAnim,backGroundAnim,houseBTNAnim,alarmsAnim;
	public GameObject[] playerPrefabs;
	public int[] prefabePrice;
	public Transform instantiatePoint;

	public int coinCount,shieldCount,bulletCount,prefabeNum = 0;
	private string name;

	public GameObject vehicleSelect,vehicleCancel,vehicleBuy;
	public Text coinText,shieldText,bulletText,vehiclePriceText;

	// products
	private string selectedProductName;
	private int selectedProdectPrice,vehiclePrice;

    // Start is called before the first frame update
    void Start()
    {

		houseQuestManager = FindObjectOfType<HouseQuestManager>();
		transmitPage = GameObject.Find ("Canvas/TransmitPage").GetComponent<Animator> ();
		cameraAnim = GameObject.Find ("Main Camera").GetComponent<Animator> ();
		backGroundAnim = GameObject.Find ("HouseBackGround").GetComponent<Animator> ();
		houseBTNAnim = GameObject.Find ("Canvas/HouseBTN").GetComponent<Animator> ();
		alarmsAnim = GameObject.Find ("Canvas/AlarmDialog").GetComponent<Animator> ();

		InstantiatePlayerPrefabe(PlayerPrefs.GetInt ("playerPrefabe"));
		coinCount = PlayerPrefs.GetInt ("Coin");
		shieldCount = PlayerPrefs.GetInt ("Shield");
		bulletCount = PlayerPrefs.GetInt ("Bullet");
		prefabeNum = PlayerPrefs.GetInt ("playerPrefabe");

		// defult vehicle for user
		PlayerPrefs.SetInt (playerPrefabs[0].name,1);

		SetData ();
    }



	// Items Select ---------------------------------------------------------------------------

	public void OnItemOptionClick()
	{
		houseBTNAnim.Play ("ItemsMoveUp");
	}



	// Purches Select ---------------------------------------------------------------------------

	public void OnPurchesClick()
	{
		houseBTNAnim.Play ("PurchesMoveUp");
	}





	// Vehicle Select ---------------------------------------------------------------------------

	public void OnVehicleOptionClick()
	{
		cameraAnim.Play ("HouseVehicleZoomCamera");
		backGroundAnim.Play ("BackGroundFadeIn");
		houseBTNAnim.Play ("OptionsMoveUp");
	}

	public void OnVehicleCencelClick()
	{
		cameraAnim.Play ("HouseVehicleZoomOutCamera");
		backGroundAnim.Play ("BackGroundFadeIOut");
		houseBTNAnim.Play ("OptionsMoveDown");
		InstantiatePlayerPrefabe(PlayerPrefs.GetInt ("playerPrefabe"));

	}

	public void OnVehicleSelectClick()
	{
		cameraAnim.Play ("HouseVehicleZoomOutCamera");
		backGroundAnim.Play ("BackGroundFadeIOut");
		houseBTNAnim.Play ("OptionsMoveDown");
		PlayerPrefs.SetInt ("playerPrefabe", prefabeNum);
	}

	public void OnVehicleBuyClick()
	{
		foreach (var item in ProductController.instance.products) {
			if (item.Name.Equals (name)) {
				vehiclePrice = item.price;
				if(vehiclePrice<=coinCount)
				{
					cameraAnim.Play ("HouseVehicleZoomOutCamera");
					backGroundAnim.Play ("BackGroundFadeIOut");
					houseBTNAnim.Play ("OptionsMoveDown");
					PlayerPrefs.SetInt ("playerPrefabe", prefabeNum);
					PlayerPrefs.SetInt (playerPrefabs[prefabeNum].name, 1);
					PlayerPrefs.SetInt("totalVehicle", PlayerPrefs.GetInt("totalVehicle")+1);
					coinCount -= vehiclePrice;
					SetData ();
					OnVehicleCheck ();
					Debug.Log ("you bought "+name+" with "+vehiclePrice+" coins");
					houseQuestManager.CheckVehicleQuests();
				}
				else{
					CreateAlarm ("showCoinAlarm");
				}
				break;
			}
		}

	}
		
	public void OnVehicleRightClick()
	{
		if (playerPrefabs.Length - 1 == prefabeNum)
			prefabeNum = 0;
		else
			prefabeNum++;
		OnVehicleCheck ();
		InstantiatePlayerPrefabe(prefabeNum);

	}

	public void OnVehicleLeftClick()
	{
		if (prefabeNum == 0)
			prefabeNum = playerPrefabs.Length - 1;
		else
			prefabeNum--;
		OnVehicleCheck ();
		InstantiatePlayerPrefabe(prefabeNum);

	}

	private void OnVehicleCheck()
	{
		name = playerPrefabs [prefabeNum].name;
		if (PlayerPrefs.GetInt (name) == 1) {
			vehicleBuy.SetActive (false);
			vehicleSelect.SetActive (true);
			vehicleCancel.SetActive (true);
		} else {
			vehicleBuy.SetActive (true);
			vehicleSelect.SetActive (false);
			vehicleCancel.SetActive (false);
			foreach (var item in ProductController.instance.products) {
				if (item.Name.Equals (name)) {
					vehiclePriceText.text = item.price+" Coins";
				}
			}
		}
	}





	// ------------------------------------------------------------------------------------------

	public void OnBackClick()
	{
		StartCoroutine ("SceneLoadingDelay","menu");
	}

	private IEnumerator SceneLoadingDelay(string levelName)
	{
		transmitPage.Play ("TransmitFadeIn");
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(levelName);
	}

	private void InstantiatePlayerPrefabe(int num)
	{
		Destroy (GameObject.FindGameObjectWithTag("player"));
		GameObject playerInstance =
			Instantiate(playerPrefabs[num],
				instantiatePoint.position,
				instantiatePoint.rotation) as GameObject;
	}
		
	public void SetData()
	{
		coinText.text = coinCount+"";
		shieldText.text = shieldCount+"";
		bulletText.text = bulletCount+"";

		PlayerPrefs.SetInt ("Coin",coinCount);
		PlayerPrefs.SetInt ("Bullet",bulletCount);
		PlayerPrefs.SetInt ("Shield",shieldCount);
	}

	public void BuyItem(string productId)
	{
		foreach (var item in ProductController.instance.products) {
			if (item.Name.Equals (productId)) {
				if (item.price <= coinCount) {
					coinCount -= item.price;
					AllocateProduct (item.Name);
				} else
					CreateAlarm ("showCoinAlarm");
			}
		}
	}

	private void AllocateProduct(string id)
	{
		switch (id) {
			case "Bullet100":
				{
					bulletCount += 100;
					PlayerPrefs.SetInt("totalBullets", PlayerPrefs.GetInt("totalBullets") + 100);
					SetData ();
					houseQuestManager.CheckBulletQuests();
					break;
				}
			case "Bullet500":
				{
					bulletCount += 500;
					PlayerPrefs.SetInt("totalBullets", PlayerPrefs.GetInt("totalBullets") + 500);
					SetData ();
					houseQuestManager.CheckBulletQuests();
					break;
				}
			case "Shield5":
				{
					shieldCount += 5;
					PlayerPrefs.SetInt("totalShields", PlayerPrefs.GetInt("totalShields") + 5);
					SetData ();
					houseQuestManager.CheckShieldQuests();
					break;
				}
			case "Shield15":
				{
					shieldCount += 15;
					PlayerPrefs.SetInt("totalShields", PlayerPrefs.GetInt("totalShields") + 15);
					SetData ();
					houseQuestManager.CheckShieldQuests();
					break;
				}
			
		}
	}

	public void BuyNewCoins(string id)
    {
		switch (id)
        {
			case "Coin100":
				{
					coinCount += 100;
					PlayerPrefs.SetInt("totalCoins", PlayerPrefs.GetInt("totalCoins") + 100);
					SetData();
					houseQuestManager.CheckCoinQuests();
					break;
				}
			case "Coin500":
				{
					coinCount += 500;
					PlayerPrefs.SetInt("totalCoins", PlayerPrefs.GetInt("totalCoins") + 500);
					SetData();
					houseQuestManager.CheckCoinQuests();
					break;
				}
			case "Coin2000":
				{
					coinCount += 2000;
					PlayerPrefs.SetInt("totalCoins", PlayerPrefs.GetInt("totalCoins") + 2000);
					SetData();
					houseQuestManager.CheckCoinQuests();
					break;
				}
			case "Coin8000":
				{
					coinCount += 8000;
					PlayerPrefs.SetInt("totalCoins", PlayerPrefs.GetInt("totalCoins") + 8000);
					SetData();
					houseQuestManager.CheckCoinQuests();
					break;
				}
		}

    }
	


	// Alarms ----------------------------------------------------------------------------------


	public void CreateAlarm(string type)
	{
		switch (type)
		{
		case "showCoinAlarm":
			{
				alarmsAnim.Play ("CoinAlarmShow");
				break;
			}
		case "closeCoinAlarm":
			{
				alarmsAnim.Play ("CoinAlarmClose");
				break;
			}
		}
			
	}

}
