using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{

    public int portalNumber;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("portal"+portalNumber) == 1)
        {
            gameObject.SetActive(false);
        }
    }
    public void SetPortalUsed()
    {
        PlayerPrefs.SetInt("portal" + portalNumber,1);
    }
}
