using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO Certain Functions and objects are kept server side
public class Game: MonoBehaviour {
    private List<Player> players;

    private bool DEBUG_MESSAGES_ENABLED = true;

    public List<Player> GetPlayers() {
        return players;
    }


    public void GetBattleState() {
        //return players;
    }

    // Use this for initialization

    private bool IsBattleOver(Player player1, Player player2, Minion p1Minion, Minion p2Minion) {
        return !((player1.GetBattleRosterSize() > 0 || !p1Minion.IsDead())
            && (player2.GetBattleRosterSize() > 0|| !p2Minion.IsDead()));
    }

    private Minion GetNextMinion(Player player, Minion minion = null) {
        if ((minion == null && player.GetBattleRosterSize() > 0)
            || (minion.IsDead() && player.GetBattleRosterSize() > 0)) {
                return player.GetBattleRosterMinion(0);
        }
        return minion;
    }

    private int GetAliveMinionCount(Player player, Minion minion) {
        return player.GetBattleRosterSize() + (minion.IsDead() ? 0 : 1);
    }

    private void DebugMinion(Minion minion) {
        Debug.Log(
            "Name: " + minion.GetName()
            + "\n" + "Attack: " + minion.GetAttack()
            + "\n" + "Health: " + minion.GetHealth()
            + "\n"
        );
    }

    private void FightDebugLogs(Player player1, Player player2, Minion p1Minion, Minion p2Minion) {
        if (!DEBUG_MESSAGES_ENABLED) return;
        Debug.Log("Player 1 Minions:");
        if (!p1Minion.IsDead()) {
            DebugMinion(p1Minion);
        }
        foreach (Minion minion in player1.GetBattleRoster()) {
            DebugMinion(minion);
        }
        Debug.Log("Player 2 Minions:");
        if (!p2Minion.IsDead()) {
            DebugMinion(p2Minion);
        }
        foreach (Minion minion in player2.GetBattleRoster()) {
            DebugMinion(minion);
        }
        Debug.Log("Player 1 Health: " + player1.GetHealth());
        Debug.Log("Player 2 Health: " + player2.GetHealth());
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
        if (GetAliveMinionCount(player1, p1Minion) == GetAliveMinionCount(player2, p2Minion)) return;

        Player victor = GetAliveMinionCount(player1, p1Minion) > GetAliveMinionCount(player2, p2Minion)
            ? player1 : player2;
        Player loser = GetAliveMinionCount(player1, p1Minion) > GetAliveMinionCount(player2, p2Minion)
            ? player2 : player1;

        int victorAliveMinionCount = GetAliveMinionCount(player1, p1Minion) > GetAliveMinionCount(player2, p2Minion)
            ? GetAliveMinionCount(player1, p1Minion) : GetAliveMinionCount(player2, p2Minion);
        loser.TakeDamage(victorAliveMinionCount);

        FightDebugLogs(player1, player2, p1Minion, p2Minion);
    }

    private void TestGame() {
        // Initialize Player 1
        Player player1 = new Player(health:30, coins:3);
        player1.AddToRoster(new Minion("Pooka"));
        player1.AddToRoster(new Minion("Inkling"));
        player1.AddToRoster(new Minion("Octo Papa"));
        player1.AddToRoster(new Minion("Sharko"));
        player1.AddToRoster(new Minion("Bubble Blowfish"));
        player1.AddToRoster(new Minion("Chonky Swordfish"));

        // Initialize Player 2
        Player player2 = new Player(health:30, coins:3);
        player2.AddToRoster(new Minion("Fireball"));
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Inferno Golem"));
        player2.AddToRoster(new Minion("Whelp Master"));
        player2.AddToRoster(new Minion("Whelp Master"));
        player2.AddToRoster(new Minion("Pheonix"));

        Fight(player1, player2);
        Fight(player1, player2);
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