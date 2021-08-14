using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/* Apache License. Copyright (C) Bobardo Studio - All Rights Reserved.
 * Unauthorized publishing the plugin with different name is strictly prohibited.
 * This plugin is free and no one has right to sell it to others.
 * http://bobardo.com
 * http://opensource.org/licenses/Apache-2.0
 */

[RequireComponent(typeof(StoreHandler))]
public class InAppStore : MonoBehaviour
{
	public string[] dialogsTextArray;
	public Text dialogText,coins;
	public GameObject dialogBox;
    public Product[] products;
	private Motory player;
	public Animator closeBuy;

    private int selectedProductIndex;

    void Start()
    {
		player = FindObjectOfType<Motory>();
    }

    public void purchasedSuccessful(Purchase purchase)
    {
        // purchase was successful, give user the pruduct


        switch (selectedProductIndex)
        {
		case 0: // first product
			{
				int coins = PlayerPrefs.GetInt("Coins") + 10000;
				PlayerPrefs.SetInt("Coins",coins);
				closeBuy.Play("fadeOutAlarms");
				showDialogs (0);

				break;
			}
		case 1: 
			{// second product
				PlayerPrefs.SetInt("Map2",1);
				closeBuy.Play("fadeOutAlarms");
				showDialogs (0);
				break;
			}

		case 2: 
			{// second product
				PlayerPrefs.SetInt("Map3",1);
				closeBuy.Play("fadeOutAlarms");
				showDialogs (0);
				break;
			}

		case 3:
			break;

            default:
                throw new UnassignedReferenceException("you forgot to give user the product after purchase. product: " + purchase.productId);
        }
        
    }

    public void purchasedFailed(int errorCode, string info)
    {
        // purchase failed. show user the proper message
		//showDialogs (info);
        switch (errorCode)
        {
		case 1: // error connecting cafeBazaar
		case 2: // error connecting cafeBazaar
		case 4: // error connecting cafeBazaar
		case 5: // error connecting cafeBazaar
			{
				Debug.Log ("erorr in connecting to bazar");
				showDialogs (1);
				//dialogText.text = info;
				break;
			}

		case 6: // user canceled the purchase
			{
				Debug.Log ("user canceled the purchase");
				showDialogs (2);
				//dialogText.text = info;
				break;
			}
            case 7: // purchase failed
			{
				Debug.Log ("purchase failed");
				showDialogs (3);
				break;
			}
            case 8: // failed to consume product. but the purchase was successful.
			{
				Debug.Log ("failed to consume product. but the purchase was successful.");
				showDialogs (3);
				break;
			}
            case 12: // error setup cafebazaar billing
            case 13: // error setup cafebazaar billing
            case 14: // error setup cafebazaar billing
			{
				Debug.Log ("error setup cafebazaar billing");
				showDialogs (3);
				break;
			}
            case 15: // you should enter your public key
			{
				Debug.Log ("you should enter your public key");
				showDialogs (3);
				break;
			}
            case 16: // unkown error happened
			{
				Debug.Log (" unkown error happened");
				showDialogs (3);
				break;
			}
            case 17: // the result from cafeBazaar is not valid.
			{
				Debug.Log ("the result from cafeBazaar is not valid.");
				showDialogs (3);
				break;
			}
        }

    }

    public void userHasThisProduct(Purchase purchase)
    {
        // user already has this product
        switch (selectedProductIndex)
        {
            case 0: // first product
			{
				Debug.Log ("purchase failed");
				showDialogs (3);
				break;
			}
            case 1: // second product

                break;
            default:
                throw new UnassignedReferenceException("you forgot to give user the product after purchase. product: " + purchase.productId);
        }
    }

    public void failToGetUserInventory(int errorCode, string info)
    {
        // user has not this product or some error happened
		//showDialogs (info);
        switch (errorCode)
        {
            case 3:  // error connecting cafeBazaar
            case 10: // error connecting cafeBazaar
			{
				Debug.Log ("erorr in connecting to bazar");
				showDialogs (1);
				break;
			}
            case 9: // user didn't login to cafeBazaar
			{
				Debug.Log (" user didn't login to cafeBazaar");
				showDialogs (4);
				break;
			}
            case 11: // user has not this product

                break;
            case 12: // error setup cafebazaar billing
            case 13: // error setup cafebazaar billing
            case 14: // error setup cafebazaar billing
			{
				Debug.Log ("error setup cafebazaar billing");
				showDialogs (3);
				break;
			}
            case 15: // you should enter your public key
			{
				Debug.Log ("you should enter your public key");
				break;
			}
            case 16: // unkown error happened
			{
				Debug.Log ("unkown error happened");
				showDialogs (3);
				break;
			}
            case 17: // the result from cafeBazaar is not valid.
			{
				Debug.Log ("the result from cafeBazaar is not valid.");
				showDialogs (3);
				break;
			}
        }

    }

    public void purchaseProduct(int productIndex)
    {
        selectedProductIndex = productIndex;
        Product product = products[productIndex];
        if (product.type == Product.ProductType.Consumable)
        {
            GetComponent<StoreHandler>().BuyAndConsume(product.productId);
        }
        else if (product.type == Product.ProductType.NonConsumable)
        {
            GetComponent<StoreHandler>().BuyProduct(product.productId);
        }
    }

    public void checkIfUserHasProduct(int productIndex)
    {
        selectedProductIndex = productIndex;
        GetComponent<StoreHandler>().CheckInventory(products[productIndex].productId);
    }

	public void showDialogs(int index)
	{
		dialogText.text = dialogsTextArray[index];
		//dialogText.text = index;
		StopCoroutine ("showDialogDelay");
		StartCoroutine ("showDialogDelay");

	}
	public IEnumerator showDialogDelay()
	{
		dialogBox.SetActive (true);
		yield return new WaitForSeconds (5f);
		dialogBox.SetActive (false);
	}

	public void buyFirstProduct()
	{
		purchaseProduct (0);
	}
}

