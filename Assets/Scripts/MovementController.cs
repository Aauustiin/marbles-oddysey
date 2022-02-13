using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Vector3[] positionKeyframes;
    public Vector3[] rotationKeyframes;
    public float playbackDuration;

    float startTime;
    float t;
    int i = 0;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        t = (Time.time - startTime) / playbackDuration;
        transform.localPosition = Vector3.Lerp(positionKeyframes[i], positionKeyframes[(i + 1) % positionKeyframes.Length], t);
        transform.localRotation = Quaternion.Euler(Vector3.Lerp(rotationKeyframes[i], rotationKeyframes[(i + 1) % rotationKeyframes.Length], t));
        if (t > 1)
        {
            i = (i + 1) % positionKeyframes.Length;
            startTime = Time.time;
        }
    }
}
