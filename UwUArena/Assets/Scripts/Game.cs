using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game: MonoBehaviour {
    //private Player[] player;


    // Use this for initialization
	void Start () {
		MinionData.Initialize();
        Debug.Log(MinionData.GetMinionData("Sharko").GetAttack());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*public Game() {
        InitializeCards();
    }*/
}