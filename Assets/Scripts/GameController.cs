﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Debug.Log("Started game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
