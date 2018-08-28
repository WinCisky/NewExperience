using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Two.ECS
{
    public class GameManager : MonoBehaviour
    {
        //Boilerplat game manager stuff

        //GameManager reference
        public static GameManager GM;

        [Header("Ship stuff")]
        public Transform target;

        //ECS stuff
        EntityManager manager;

        [Header("Default stuff")]
        public int level = 1;
        public GameObject enemy_prefab;

        const int MAX_RANDOM_Z = 750;
        const int MIN_RANDOM_Z = 300;
        const int RANDOM_X = 1000;
        const int RANDOM_Y = 1000;
        const int MAX_RANDOM_SPEED = 150;
        const int MIN_RANDOM_SPEED = 70;

        // Use this for initialization
        void Start()
        {
            GM = this;
            Time.timeScale = 1;
            manager = World.Active.GetOrCreateManager<EntityManager>();
            //AddStuff(level);
        }

        public void GameOver()
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
        }

        public void AddStuff(int type)
        {
            //spawn 96 enemies after 1 seconds
            StartCoroutine(AddMoreStuff(0, 1, 32000));
        }

        private IEnumerator AddMoreStuff(int type, int time, int amount)
        {
            yield return new WaitForSeconds(time);
            NativeArray<Entity> entities = new NativeArray<Entity>(amount, Allocator.Temp);
            manager.Instantiate(enemy_prefab, entities);

            float xPos = target.transform.position.x;
            float yPos = target.transform.position.y;

            for (int i = 0; i < amount; i++)
            {
                float xVal = Random.Range(xPos - RANDOM_X, xPos + RANDOM_X);
                float yVal = Random.Range(yPos - RANDOM_Y, yPos + RANDOM_Y);
                manager.SetComponentData(entities[i], new Position { Value = new float3(xVal, yVal, Random.Range(MIN_RANDOM_Z, MAX_RANDOM_Z)) });
                manager.SetComponentData(entities[i], new Rotation { Value = new quaternion(0, 1, 0, 0) });
                manager.SetComponentData(entities[i], new MoveSpeed { value = Random.Range(MIN_RANDOM_SPEED, MAX_RANDOM_SPEED) });
            }
            entities.Dispose();
        }
    }
}

