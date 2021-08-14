using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entsShower : MonoBehaviour {


    public Animator entsAnim;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            entsAnim.Play("EntsAnim");
        }
    }
}
