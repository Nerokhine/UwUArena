using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO Certain Functions and objects are kept server side
public class Game: MonoBehaviour {
    private List<Player> players;

    // Use this for initialization

    private bool IsBattleOver(Player player1, Player player2, Minion p1Minion, Minion p2Minion) {
        return !((player1.GetBattleRosterSize() > 0 && !p1Minion.IsDead()) && (player2.GetBattleRosterSize() > 0 && !p2Minion.IsDead()));
    }

    private Minion GetNextMinion(Player player, Minion minion = null) {
        if ((minion == null && player.GetBattleRosterSize() > 0) || (minion.IsDead() && player.GetBattleRosterSize() > 0)) {
                return player.GetBattleRosterMinion(0);
        }
        return minion;
    }

    private void Fight(Player player1, Player player2) {
        player1.StartBattle();
        player2.StartBattle();
        Minion p1Minion = GetNextMinion(player1);
        Minion p2Minion = GetNextMinion(player2);

        while (!IsBattleOver(player1, player2, p1Minion, p2Minion)) {
            p1Minion.Attack(p2Minion);
            p1Minion = GetNextMinion(player1, p1Minion);
            p2Minion = GetNextMinion(player2, p2Minion);
            if(IsBattleOver(player1, player2, p1Minion, p2Minion)) break;
            p2Minion.Attack(p1Minion);
            p1Minion = GetNextMinion(player1, p1Minion);
            p2Minion = GetNextMinion(player2, p2Minion);
        }
        // Tie
        if (player1.GetBattleRosterSize() == player2.GetBattleRosterSize()) return;

        Debug.Log("hi");

        Player victor = player1.GetBattleRosterSize() > player2.GetBattleRosterSize() ? player1 : player2;
        Player loser = player1.GetBattleRosterSize() > player2.GetBattleRosterSize() ? player2 : player1;

        loser.TakeDamage(victor.GetBattleRosterSize());
    }

    private void TestGame() {
        Player player1 = new Player(health:30, coins:3);
        Minion minion1 = new Minion("Octo Papa");
        player1.AddToRoster(minion1);
        Player player2 = new Player(health:30, coins:3);
        Minion minion2 = new Minion("Sharko");
        player2.AddToRoster(minion2);
        Fight(player1, player2);
        Debug.Log(player2.GetHealth());
    }

	void Start () {
        players = new List<Player>();
		MinionData.Initialize();
        TestGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}