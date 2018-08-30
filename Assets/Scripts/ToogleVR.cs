using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ToogleVR : MonoBehaviour {

    bool vrMode = true;

    public void ToggleVRMode()
    {
        vrMode = !vrMode;
        XRSettings.enabled = vrMode;
    }
}
