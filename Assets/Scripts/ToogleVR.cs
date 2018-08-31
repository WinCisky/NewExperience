using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ToogleVR : MonoBehaviour {

    public GameObject MyCam;
    bool vrMode = true;

    private void Start()
    {
        MyCam.GetComponent<GyroInput>().enabled = !vrMode;
    }

    public void ToggleVRMode()
    {
        vrMode = !vrMode;
        XRSettings.enabled = vrMode;
        
        MyCam.GetComponent<GyroInput>().enabled = !vrMode;
    }
}
