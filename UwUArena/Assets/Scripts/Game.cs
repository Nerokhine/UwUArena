using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game: MonoBehaviour {
    private List<Player> players;
    
    // Use this for initialization
	void Start () {
        players = new List<Player>();
		MinionData.Initialize();
        Player player = new Player(health:30, coins:3);
        Minion minion = new Minion("Sharko");
        player.AddToRoster(minion);
        players.Add(player);
        //Debug.Log(players[0].GetRosterMinion(0).GetName());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}