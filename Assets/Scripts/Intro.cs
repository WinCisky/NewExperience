using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

    public GameObject player_ship, player_camera;
    public GameObject[] menu_ui;

    public void GameIntro()
    {
        foreach (var item in menu_ui)
            item.SetActive(false);
        StartCoroutine(PlaceShip(new Vector3(0, 0, 100), 60));
    }

    public void Level3Intro()
    {
        foreach (var item in menu_ui)
            item.SetActive(false);
        StartCoroutine(PlaceShip(new Vector3(0, 0, 300), 60));
    }

    public
    
    IEnumerator PlaceShip(Vector3 start_point, float speed)
    {
        //player_camera_animator.SetBool("intro_start", true);
        while(player_ship.transform.position != start_point)
        {
            player_ship.transform.position = Vector3.MoveTowards(player_ship.transform.position, start_point, Time.fixedDeltaTime * speed);
            yield return new WaitForEndOfFrame();
            
        }
        //player_camera_animator.enabled = false;
        player_ship.GetComponent<FollowRotation>().can_move = true;
        //il giocatore è in posizione
        Two.ECS.GameManager.GM.AddStuff(0);
    }


}
