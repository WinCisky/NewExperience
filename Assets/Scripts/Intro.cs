using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

    public GameObject menu_ui, player_ship, player_camera;
    public Animator player_camera_animator;

    public void GameIntro()
    {
        menu_ui.gameObject.SetActive(false);
        StartCoroutine(PlaceShip(new Vector3(0, 0, 100), 60));
    }
    
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
