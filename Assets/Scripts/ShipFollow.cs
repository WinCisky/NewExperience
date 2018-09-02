using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFollow : MonoBehaviour {

    public Transform target;
    public Vector3 last_pos;
    public float speed = 25f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float step = speed * Time.fixedDeltaTime;
        if (transform.position != last_pos)
            transform.position = Vector3.MoveTowards(
                transform.position, 
                new Vector3(
                    target.position.x,
                    target.position.y,
                    50), 
                step);
        else
            last_pos = target.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "bullet")
        {
            foreach (var item in gameObject.GetComponentsInChildren<ShipBulletManager>())
            {
                item.ship_destroyed = true;
            }
            gameObject.SetActive(false);
        }
    }
}
