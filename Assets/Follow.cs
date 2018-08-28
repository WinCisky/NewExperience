using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform target;
    public float speed = 1;
    
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z), Time.fixedDeltaTime * speed);
	}
}
