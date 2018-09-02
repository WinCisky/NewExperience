using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class MovementAssistantEnemyShip : MonoBehaviour
{
    float2 
        x_bounds,
        y_bounds,
        target_position;
    float movement_speed;
}

public class MovementSystemEnemyShip : ComponentSystem {

    struct ComponentsComet
    {
        public MovementAssistantComet movement;
        public Transform transform;
    }

    protected override void OnUpdate()
    {
        Vector3 target = Two.ECS.GameManager.GM.target.transform.position;
        foreach (var e in GetEntities<ComponentsComet>())
        {
            e.transform.rotation *= Quaternion.Euler(1 * e.movement.rotating_speed.x, 1 * e.movement.rotating_speed.y, 1 * e.movement.rotating_speed.z);
            if (e.transform.position.z < -150)
            {
                e.transform.position = new Vector3(
                    target.x + Random.Range(-350, +350),
                    target.y + Random.Range(-350, +350),
                    1500);
                if (e.movement.has_trail)
                    e.movement.tr.Clear();
                
                e.movement.rb.velocity = Vector3.zero;
                e.movement.rb.angularVelocity = Vector3.zero;
                e.transform.rotation = Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), 0);
                e.movement.rb.AddForce(-e.transform.forward * e.movement.movementSpeed, ForceMode.Impulse);
            }
        }
    }

}
