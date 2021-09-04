using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMiner : MonoBehaviour
{
    public int speedRotate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * speedRotate * Time.deltaTime);
    }
}
