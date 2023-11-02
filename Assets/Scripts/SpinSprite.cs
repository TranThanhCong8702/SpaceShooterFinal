using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSprite : MonoBehaviour
{

    private void Spinning()
    {
        transform.Rotate(0, 0, 2);
    }
    void Start()
    {
        Spinning();
    }


    void Update()
    {
        Spinning();
    }
}
