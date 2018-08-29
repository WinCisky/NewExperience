using System.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Burst;


namespace Two.ECS
{

    public class MovementSystem : JobComponentSystem
    {
        
        const float SENSIBILITY = 0.3f;
        const int MIN_Z = -150;
        const int MAX_Z = 500;
        const int JOB_TO_SCHEDULE = 320;

        [BurstCompile]
        struct MovementJob : IJobProcessComponentData<Position, Rotation, MoveSpeed>
        {
            public float x_pos;
            public float y_pos;
            public float deltaTime;
            public bool dead;
            public void Execute(ref Position position, ref Rotation rotation, ref MoveSpeed speed)
            {
                float3 value = position.Value;
                value += deltaTime * speed.value * math.forward(rotation.Value);


                if ((new Vector2(value.x, value.y) - new Vector2(x_pos, y_pos)).magnitude < SENSIBILITY)
                    //GameManager.GM.GameOver();
                    dead = true;
                

                if (value.z < MIN_Z)
                    value.z = MAX_Z;
                position.Value = value;
            }
        }

        /*
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            MovementJob moveJob = new MovementJob
            {
                player_position = GameManager.GM.target.position,
                deltaTime = Time.deltaTime
            };
            return moveJob.Schedule(this, 10, inputDeps);
        }
        */

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            MovementJob movementJob = new MovementJob
            {
                x_pos = GameManager.GM.target.position.x,
                y_pos = GameManager.GM.target.position.y,
                deltaTime = Time.deltaTime,
                dead = false
            };
            JobHandle jobHandle = movementJob.Schedule(this, JOB_TO_SCHEDULE, inputDeps);

            jobHandle.Complete();

            if (movementJob.dead)
                GameManager.GM.GameOver();

            return jobHandle;
        }
    }
}
