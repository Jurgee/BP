using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapControl : MonoBehaviour
{

    [SerializeField] public Image minimapFrame;
    [SerializeField] public Image minimapMask;
    [SerializeField] public RawImage maximapRender;
    public bool minimapAcquired;
    
    void Start()
    {
        minimapFrame.enabled = false;
        minimapMask.enabled = false;
        maximapRender.enabled = false;
        minimapAcquired = false; // should be false
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && minimapAcquired && !PauseMenu.gameIsPaused)
        {
            MinimapVisibility();
        }
    }

    void MinimapVisibility()
    {
        minimapFrame.enabled = !minimapFrame.enabled;
        minimapMask.enabled = !minimapMask.enabled;
        maximapRender.enabled = !maximapRender.enabled;
    }

}
