using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRotation : MonoBehaviour {

    public Transform target;
    public bool can_move, block_force;
    public new Rigidbody rigidbody;
    float max_x = Quaternion.Euler(90, 0, 0).x;
    float max_y = Quaternion.Euler(0, 90, 0).y;

    // Update is called once per frame
    void FixedUpdate () {
        float rot_y = target.transform.rotation.y;
        float rot_x = target.transform.rotation.x;
        if (Mathf.Abs(rot_x) < max_x && Mathf.Abs(rot_y) < max_y)
                transform.rotation = target.transform.rotation;

        if (block_force)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        if (can_move)
        {
            float x = target.transform.eulerAngles.x % 360;
            float y = target.transform.eulerAngles.y % 360;
            if (x > 180)
                x = x - 360;
            if (y > 180)
                y = y - 360;
            transform.position += new Vector3(y / 30, -x / 30, 0);
        }
            
    }
}
