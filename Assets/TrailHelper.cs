using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailHelper : MonoBehaviour {

    public LineRenderer trail;
    public Transform target;
    public Vector3[] points;
    private int segments, acquired_segments;

    // Use this for initialization
	void Start () {
        segments = trail.positionCount;
        acquired_segments = 0;
        points = new Vector3[segments];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = acquired_segments - 1; i > 0; i--)
            points[i] = points[i - 1];
        points[0] = target.transform.position + (transform.position - target.transform.position);
        if (acquired_segments < segments)
            acquired_segments++;
        for (int i = 0; i < acquired_segments; i++)
            trail.SetPosition(i, new Vector3(points[i].x, points[i].y, (points[i].z - (i * i)/1.5f)));
        Gradient gradient = new Gradient();
        float alpha = Mathf.Clamp01(Mathf.Abs(target.transform.rotation.x) + Mathf.Abs(target.transform.rotation.y) * 3);
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(trail.colorGradient.colorKeys[0].color, 0.0f),
                new GradientColorKey(trail.colorGradient.colorKeys[1].color, 0.25f),
                new GradientColorKey(trail.colorGradient.colorKeys[2].color, 0.50f),
                new GradientColorKey(trail.colorGradient.colorKeys[3].color, 0.75f),
                new GradientColorKey(trail.colorGradient.colorKeys[4].color, 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(alpha, 0.0f),
                new GradientAlphaKey(alpha, 1f)
            }
        );
        trail.colorGradient = gradient;
    }

}
