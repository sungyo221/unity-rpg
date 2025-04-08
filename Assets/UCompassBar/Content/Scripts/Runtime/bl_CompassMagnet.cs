using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bl_CompassMagnet : MonoBehaviour
{
    private Transform Compass;
    private Vector3 euler;

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        if (Compass == null)
        {
            GameObject g = new GameObject("Compass");
            Compass = g.transform;
            Compass.parent = transform;
            Compass.localPosition = Vector3.zero;
            Compass.localRotation = Quaternion.identity;
        }
        CompassMarkEvent.SetCompassCamera(Compass);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (Compass == null) return;

        euler = Compass.eulerAngles;
        euler.x = 0;
        Compass.eulerAngles = euler;
        Debug.DrawRay(Compass.position, Compass.forward, Color.red);
    }
}