using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour
{
    private bool dirRight = true;
    public float speed = 2.0f;
    public int scale = 1;
    public Transform start, end;
    public GameObject bird;

    void Update()
    {
        if (dirRight)
            bird.transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            bird.transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (bird.transform.position.x >= end.position.x)
        {
            dirRight = false;
            bird.transform.localScale = new Vector2(scale, scale);
        }

        if (bird.transform.position.x <= start.position.x)
        {
            dirRight = true;
            bird.transform.localScale = new Vector2(-scale, scale);
        }
    }
}
