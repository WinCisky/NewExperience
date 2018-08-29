using Unity.Collections;
using System.Collections.Generic;
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

        [Header("ECS stuff")]
        public GameObject asteroid_prefab;
        EntityManager manager;

        [Header("Default stuff")]
        public int level = 1;
        public GameObject enemy_prefab;

        List<GameObject> asteroids_list;

        const int MAX_RANDOM_Z = 3000;
        const int MIN_RANDOM_Z = 1500;
        const int RANDOM_X = 500;
        const int RANDOM_Y = 500;
        const int MAX_RANDOM_SPEED = 150;
        const int MIN_RANDOM_SPEED = 70;

        // Use this for initialization
        void Start()
        {
            GM = this;
            Time.timeScale = 1;
            manager = World.Active.GetOrCreateManager<EntityManager>();
            //AddStuff(level);
            asteroids_list = new List<GameObject>();
        }

        public void GameOver()
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
        }

        public void AddStuff(int type)
        {
            //PURE ECS
            //spawn 96 enemies after 1 seconds
            //StartCoroutine(AddMoreStuff(0, 1, 32000));

            //HYBRID ECS
            asteroids_list = InstantiateStuff(asteroids_list, asteroid_prefab, 500);
        }

        private List<GameObject> InstantiateStuff(List<GameObject> list, GameObject go, int amount)
        {
            List<GameObject> result = list;
            for (int i = 0; i < amount; i++)
            {
                GameObject g = Instantiate(go);
                go.transform.position = new Vector3(
                    go.transform.position.x + Random.Range(target.transform.position.x - RANDOM_X, target.transform.position.x + RANDOM_X),
                    go.transform.position.y + Random.Range(target.transform.position.y - RANDOM_Y, target.transform.position.y + RANDOM_Y),
                    Random.Range(MIN_RANDOM_Z, MAX_RANDOM_Z));
                go.GetComponent<Movement>().speed = Random.Range(120, 180);
                result.Add(g);
            }
            return result;
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

