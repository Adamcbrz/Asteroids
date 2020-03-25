﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcuclideanTorusInitialization : MonoBehaviour
{
    void Awake()
    {
        Camera cam = Camera.main;

        float sceneWidth = cam.orthographicSize * 2 * cam.aspect;
        float sceneHeight = cam.orthographicSize * 2;
        EuclideanTorus.current.Setup(sceneWidth, sceneHeight);
        
    }

}