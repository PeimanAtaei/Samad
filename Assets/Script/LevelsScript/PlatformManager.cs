using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject firstDetail, secondDetail, centerDetail,centerDetail2, topDetail,topDetail2, downDetail, downDetail2;

    public void VisiblePlatform(int number)
    {
        switch(number)
        {
            case 0:
                {
                    secondDetail.SetActive(false);
                    break;
                }
            case 1:
                {
                    secondDetail.SetActive(true);
                    break;
                }
            case 2:
                {
                    firstDetail.SetActive(false);
                    centerDetail.SetActive(true);
                    break;
                }
            case 3:
                {
                    firstDetail.SetActive(false);
                    topDetail.SetActive(true);
                    break;
                }
            case 4:
                {
                    firstDetail.SetActive(false);
                    downDetail.SetActive(true);
                    break;
                }

            case 5:
                {
                    secondDetail.SetActive(false);
                    centerDetail2.SetActive(true);
                    break;
                }

            case 6:
                {
                    secondDetail.SetActive(false);
                    downDetail2.SetActive(true);
                    break;
                }

            case 7:
                {
                    secondDetail.SetActive(false);
                    topDetail2.SetActive(true);
                    break;
                }
        }
    }
}
