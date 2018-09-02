using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlanetSlowly : MonoBehaviour {
    public float speed = 1;
	void FixedUpdate () {
        transform.rotation *= Quaternion.Euler(Vector3.down * speed * Time.fixedDeltaTime);
	}
}
