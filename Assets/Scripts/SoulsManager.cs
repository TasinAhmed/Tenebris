﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulsManager : MonoBehaviour
{
    public static int souls;
    Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        souls = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "SOULS: " + souls;
    }
}
