using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Movement : MonoBehaviour {
    public float speed;
}

//Hybrid ECS
public class MovementSystem : ComponentSystem
{
    struct Components
    {
        public Movement movement;
        public Transform transform;
    }

    public Transform target;

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime * -1;
        Vector3 target = Two.ECS.GameManager.GM.target.transform.position;
        foreach (var e in GetEntities<Components>())
        {
            e.transform.position += new Vector3(0, 0, deltaTime * e.movement.speed);
            e.transform.rotation *= Quaternion.Euler(2, 2, 2);
            if (e.transform.position.z < -150)
                e.transform.position = new Vector3(
                    target.x + Random.Range(-500, +500),
                    target.y + Random.Range(-500, +500),
                    1500);
        }
    }
} 