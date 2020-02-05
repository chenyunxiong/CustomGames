using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    public float time = 0.2f;
    public float min = 0.5f;
    public float max = 5;

    void  Start()
    {
        if (light) {
            InvokeRepeating("OneLightChange", time, time);
        } else {
            print("Please add a light component for light flicker");
        }
    }

    void  OneLightChange()
    {
        light.range = Random.Range(min, max);
    }

}