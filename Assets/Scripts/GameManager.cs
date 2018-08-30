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
        public GameObject asteroids_father;
        public Mesh[] asteroids_meshes;
        public GameObject comet_prefab;
        public GameObject comets_father;
        public Mesh[] comets_meshes;
        EntityManager manager;

        [Header("Default stuff")]
        public int level = 1;
        public GameObject enemy_prefab;

        List<GameObject> asteroids_list;
        List<GameObject> comets_list;

        const int MAX_RANDOM_Z = 3000;
        const int MIN_RANDOM_Z = 1500;
        const int RANDOM_X = 500;
        const int RANDOM_Y = 500;
        const int MAX_RANDOM_SPEED = 150;
        const int MIN_RANDOM_SPEED = 70;
        const int ROTATING_SPEED = 3;

        // Use this for initialization
        void Start()
        {
            GM = this;
            Time.timeScale = 1;
            manager = World.Active.GetOrCreateManager<EntityManager>();
            //AddStuff(level);
            asteroids_list = new List<GameObject>();
            comets_list = new List<GameObject>();
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
            asteroids_list = InstantiateStuff(0, asteroids_list, asteroids_meshes, asteroid_prefab, asteroids_father, 500, true);
            //comets_list = InstantiateStuff(1, comets_list, comets_meshes, comet_prefab, comets_father, 200, true);
        }

        private List<GameObject> InstantiateStuff(int type, List<GameObject> list, Mesh[] meshes, GameObject go, GameObject father, int amount, bool random_meshes)
        {
            List<GameObject> result = list;
            for (int i = 0; i < amount; i++)
            {
                GameObject g = Instantiate(go);
                g.transform.SetParent(father.transform);
                if (random_meshes)
                {
                    int random = Random.Range(0, meshes.Length);
                    g.GetComponent<MeshFilter>().mesh = meshes[random];
                    g.GetComponent<MeshCollider>().sharedMesh = meshes[random];
                }
                g.transform.position = new Vector3(
                    g.transform.position.x + Random.Range(target.transform.position.x - RANDOM_X, target.transform.position.x + RANDOM_X),
                    g.transform.position.y + Random.Range(target.transform.position.y - RANDOM_Y, target.transform.position.y + RANDOM_Y),
                    Random.Range(MIN_RANDOM_Z, MAX_RANDOM_Z));
                switch (type)
                {
                    case 0: //Asteroids
                        g.GetComponent<MovementAssistantAsteroids>().moving_speed = Random.Range(120, 180);
                        g.GetComponent<MovementAssistantAsteroids>().rotating_speed = new Vector3(
                            Random.Range(-ROTATING_SPEED, ROTATING_SPEED),
                            Random.Range(-ROTATING_SPEED, ROTATING_SPEED),
                            Random.Range(-ROTATING_SPEED, ROTATING_SPEED));
                        break;
                    case 1: //Comets
                        float ms = Random.Range(250, 500);
                        g.GetComponent<MovementAssistantComet>().movementSpeed = ms;
                        g.GetComponent<MovementAssistantComet>().rotating_speed = ms * 0.02f;
                        if (Random.Range(0, 1) == 0)
                            g.GetComponent<MovementAssistantComet>().rotating_speed *= -1;
                        g.GetComponent<MovementAssistantComet>().rb = g.GetComponent<Rigidbody>();
                        g.GetComponent<MovementAssistantComet>().tr = g.GetComponent<TrailRenderer>();
                        g.transform.rotation = Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), 0);
                        g.GetComponent<Rigidbody>().AddForce(-g.transform.forward * ms, ForceMode.Impulse);
                        break;
                    default:
                        //ERROR
                        break;

                }
                
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

