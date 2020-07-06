using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    private float timeSinceStart;

    private void Update()
    {
        timeSinceStart += Time.deltaTime;
    }

    public float getTimeSinceStart()
    {
        return timeSinceStart;
    }
}
