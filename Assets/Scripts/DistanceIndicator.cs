using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceIndicator : MonoBehaviour {

    public TextMeshProUGUI remaining_distance_text;
    public Transform a, b;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(Vector3.Distance(a.position, b.position) - (b.transform.localScale.x / 2));
        float distance = (b.position.x) - Mathf.Sqrt(Mathf.Pow((b.transform.localScale.x / 2), 2) - Mathf.Pow((b.position.z - a.position.z), 2));
        remaining_distance_text.SetText(((int)(distance - a.transform.position.x)).ToString());
        //Debug.Log(cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x);
        /*
        float pos = Mathf.Min(cam.ViewportToWorldPoint(new Vector3(1, 1, cam.farClipPlane)).x, distance);
        indicator.transform.position = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, cam.nearClipPlane));
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
        Debug.Log(cam.ViewportToWorldPoint(new Vector3(1, 1, cam.farClipPlane)).z);
        */
    }
}
