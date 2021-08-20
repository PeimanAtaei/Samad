using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenPlat : MonoBehaviour
{
    public Animator anim;
    public bool canCollapse = true;
    public string clipName;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (canCollapse)
        {
            canCollapse = false;
            StartCoroutine("Collapse");
        }
    }

    public IEnumerator Collapse()
    {
        yield return new WaitForSeconds(0.8f);
        anim.Play(clipName);
    }
}
