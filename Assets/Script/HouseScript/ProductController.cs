using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductController : MonoBehaviour
{
    // Start is called before the first frame update

	public static ProductController instance;

	public List<Product> products ;

	[System.Serializable]
	public class Groups
	{
		public string groupName;
		public List<Product> products;
	}

	[System.Serializable]
	public class Product
	{
		public string Name;
		public int price;
	}

	public void Start()
	{
		
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
