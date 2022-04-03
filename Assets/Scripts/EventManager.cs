using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class EventManager : MonoBehaviour
{
    public static event Action PlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void TriggerPlayerDeath()
    {
        PlayerDeath?.Invoke();
    }
}
