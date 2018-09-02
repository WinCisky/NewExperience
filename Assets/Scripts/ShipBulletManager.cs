using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBulletManager : MonoBehaviour {

    public GameObject[] bullets;
    public bool ship_destroyed;
    public Animation anim;
    public Transform cannon;

	// Use this for initialization
	void Start () {
        StartCoroutine(Shoot());
	}

    IEnumerator Shoot()
    {
        while (!ship_destroyed)
        {
            anim.Play("Shoot");
            GameObject go = null;
            for (int i = 0; i < (bullets.Length - 1); i++)
            {
                if (bullets[i].GetComponent<MovementAssistantBullet>().usable)
                {
                    go = bullets[i];
                    break;
                }
            }
            if (go != null)
            {
                //animation             
                yield return new WaitWhile(() => anim.IsPlaying("Shoot"));
                go.GetComponent<MovementAssistantBullet>().reset = true;
                go.GetComponent<MovementAssistantBullet>().usable = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
