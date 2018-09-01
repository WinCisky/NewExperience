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
        public GameObject comet_prefabs;
        public GameObject comets_father;
        public Mesh[] comets_meshes;
        public MeshRenderer[] comets_materials;
        EntityManager manager;

        [Header("Default stuff")]
        public int level = 1;
        public GameObject enemy_prefab;

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
            //InstantiateStuff(0, asteroids_meshes, null, asteroid_prefab, asteroids_father, 500, true);
            InstantiateStuff(1, comets_meshes, comets_materials, comet_prefabs, comets_father, 200, true);
        }

        private void InstantiateStuff(int type, Mesh[] meshes, MeshRenderer[] materials, GameObject go, GameObject father, int amount, bool random_meshes)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject g = Instantiate(go);
                g.transform.SetParent(father.transform);
                
                g.transform.position = new Vector3(
                    g.transform.position.x + Random.Range(target.transform.position.x - RANDOM_X, target.transform.position.x + RANDOM_X),
                    g.transform.position.y + Random.Range(target.transform.position.y - RANDOM_Y, target.transform.position.y + RANDOM_Y),
                    Random.Range(MIN_RANDOM_Z, MAX_RANDOM_Z));
                switch (type)
                {
                    case 0: //Asteroids
                        if (random_meshes)
                        {
                            int random = Random.Range(0, meshes.Length);
                            g.GetComponent<MeshFilter>().mesh = meshes[random];
                            g.GetComponent<MeshCollider>().sharedMesh = meshes[random];
                            if (materials != null)
                                g.GetComponent<MeshRenderer>().materials = materials[random].sharedMaterials;
                        }
                        g.GetComponent<MovementAssistantAsteroids>().moving_speed = Random.Range(120, 180);
                        g.GetComponent<MovementAssistantAsteroids>().rotating_speed = new Vector3(
                            Random.Range(-ROTATING_SPEED, ROTATING_SPEED),
                            Random.Range(-ROTATING_SPEED, ROTATING_SPEED),
                            Random.Range(-ROTATING_SPEED, ROTATING_SPEED));
                        break;
                    case 1: //Comets
                        float ms;
                        if (i % 100 == 0) //ISS (mini boss)
                        {
                            if (materials != null)
                                g.GetComponentInChildren<MeshRenderer>().materials = materials[materials.Length - 1].sharedMaterials;
                            g.GetComponentInChildren<MeshFilter>().mesh = meshes[meshes.Length - 1];
                            g.GetComponent<MeshCollider>().sharedMesh = meshes[meshes.Length - 1];
                            ms = 200;
                            g.GetComponent<MovementAssistantComet>().rotating_speed.z = ms * 0.001f;
                            g.GetComponent<MovementAssistantComet>().rotating_speed.x = Random.Range(ms * 0.0001f, ms * 0.001f);
                            g.GetComponent<MovementAssistantComet>().rotating_speed.y = Random.Range(ms * 0.0001f, ms * 0.001f);
                            g.GetComponent<TrailRenderer>().enabled = false;
                            g.GetComponent<MovementAssistantComet>().has_trail = false;
                        }
                        else
                        {
                            int random = i % (meshes.Length - 1);
                            g.GetComponent<MeshFilter>().mesh = meshes[random];
                            g.GetComponent<MeshCollider>().sharedMesh = meshes[random];
                            if (materials != null)
                                g.GetComponent<MeshRenderer>().materials = materials[random].sharedMaterials;
                            ms = Random.Range(250, 500);
                            g.GetComponent<MovementAssistantComet>().rotating_speed.z = ms * 0.01f;
                            g.GetComponent<MovementAssistantComet>().rotating_speed.x = Random.Range(ms * 0.001f, ms * 0.005f);
                            g.GetComponent<MovementAssistantComet>().rotating_speed.y = Random.Range(ms * 0.001f, ms * 0.005f);
                            g.GetComponent<MovementAssistantComet>().has_trail = true;
                        }
                        g.GetComponent<MovementAssistantComet>().movementSpeed = ms;
                        if (Random.Range(0, 1) == 0)
                            g.GetComponent<MovementAssistantComet>().rotating_speed.x *= -1;
                        if (Random.Range(0, 1) == 0)
                            g.GetComponent<MovementAssistantComet>().rotating_speed.y *= -1;
                        if (Random.Range(0, 1) == 0)
                            g.GetComponent<MovementAssistantComet>().rotating_speed.z *= -1;
                        g.GetComponent<MovementAssistantComet>().rb = g.GetComponent<Rigidbody>();
                        g.GetComponent<MovementAssistantComet>().tr = g.GetComponent<TrailRenderer>();
                        g.transform.rotation = Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), 0);
                        g.GetComponent<Rigidbody>().AddForce(-g.transform.forward * ms, ForceMode.Impulse);
                        break;
                    case 2: //Ships

                        break;
                    case 3: //Bullets

                        break;
                    default:
                        //ERROR
                        break;

                }
                
            }
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

