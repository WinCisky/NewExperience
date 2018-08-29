using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform target;
    public float speed = 1;
    public Vector3 padding;
    
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x + padding.x, target.transform.position.y + padding.y, transform.position.z + padding.z), Time.fixedDeltaTime * speed);
	}
}
