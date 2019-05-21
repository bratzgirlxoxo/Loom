using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ScreenShotTaker : MonoBehaviour
{
    private int idx = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            idx++;
            ScreenCapture.CaptureScreenshot(String.Format("LoomScreenshot{0}.png", idx), 2);
        }
    }
}
