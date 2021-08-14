using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageScript : MonoBehaviour
{
    public void SaveBtn()
    {
        SceneManager.LoadScene("menu");
    }
}
