using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MovementAssistantBullet : MonoBehaviour {
    public bool reset;
    public int id;
    public bool usable;
}

//Hybrid ECS
public class MovementSystemBullet : ComponentSystem
{
    struct ComponentsBullet
    {
        public MovementAssistantBullet movement;
        public Transform transform;
    }

    List<Transform> targets = new List<Transform>();
    float deltaTime;

    protected override void OnUpdate()
    {
        deltaTime = Time.deltaTime;
        targets = Two.ECS.GameManager.GM.enemies_ships;
        foreach (var e in GetEntities<ComponentsBullet>())
        {
            if (!e.movement.usable)
            {
                //riposiziono l'oggetto
                if (e.movement.reset)
                {
                    e.movement.reset = false;
                    e.transform.position = targets[e.movement.id].position;
                }
                //spengo l'oggetto
                if (e.transform.position.z > 750)
                    e.movement.usable = true;
                //muovo l'oggetto
                e.transform.position += deltaTime * Vector3.forward * 500;
            }
        }
    }
}