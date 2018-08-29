using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MovementAssistantAsteroids : MonoBehaviour {
    public float moving_speed;
    public Vector3 rotating_speed;
}

//Hybrid ECS
public class MovementSystemAsteroids : ComponentSystem
{
    struct ComponentsAsteroids
    {
        public MovementAssistantAsteroids movement;
        public Transform transform;
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime * -1;
        Vector3 target = Two.ECS.GameManager.GM.target.transform.position;
        foreach (var e in GetEntities<ComponentsAsteroids>())
        {
            e.transform.position += new Vector3(0, 0, deltaTime * e.movement.moving_speed);
            e.transform.rotation *= Quaternion.Euler(e.movement.rotating_speed.x, e.movement.rotating_speed.y, e.movement.rotating_speed.z);
            if (e.transform.position.z < -150)
                e.transform.position = new Vector3(
                    target.x + Random.Range(-500, +500),
                    target.y + Random.Range(-500, +500),
                    1500);
        }
    }
} 