using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour {

    public TextMeshProUGUI hp_ui_text;
    public int life = 3;

	// Use this for initialization
	void Start () {
        UpdateShownHp(life);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateShownHp(int hp_left)
    {
        hp_ui_text.text = "";
        for (int i = 0; i < hp_left - 1; i++)
            hp_ui_text.text += "<3 ";
        hp_ui_text.text += "<3";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (--life <= 0)
            Two.ECS.GameManager.GM.GameOver();
        UpdateShownHp(life);
    }
}
