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
        float random = Random.Range(-0.01f, 0.01f);
        foreach (var e in GetEntities<Components>())
        {
            e.transform.position += new Vector3(random, random, deltaTime * e.movement.speed);
        }
    }
} 