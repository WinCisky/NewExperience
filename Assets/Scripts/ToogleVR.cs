using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ToogleVR : MonoBehaviour {

    public GameObject MyCam;
    bool vrMode = false;

    public void ToggleVRMode()
    {
        vrMode = !vrMode;
        if (vrMode)
            StartCoroutine(LoadDevice(XRSettings.supportedDevices[1]));
        else
            StartCoroutine(LoadDevice(XRSettings.supportedDevices[0]));
        MyCam.GetComponent<GyroInput>().enabled = !vrMode;
    }

    void Start()
    {
        
    }

    IEnumerator LoadDevice(string newDevice)
    {
        if (String.Compare(XRSettings.loadedDeviceName, newDevice, true) != 0)
        {
            XRSettings.LoadDeviceByName(newDevice);
            yield return null;
            XRSettings.enabled = true;
        }
    }
}
