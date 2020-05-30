using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO Certain Functions and objects are kept server side
public class Battle {
    private List<Player> players = new List<Player>();

    private bool DEBUG_MESSAGES_ENABLED = true;

    public List<Player> GetPlayers() {
        return players;
    }

    private bool IsBattleOver(Player player1, Player player2) {
        return !((player1.GetBattleRosterSize() > 0) && (player2.GetBattleRosterSize() > 0));
    }

    private void DebugMinion(Minion minion) {
        Debug.Log(
            "Name: " + minion.GetName()
            + "\n" + "Attack: " + minion.GetAttack()
            + "\n" + "Health: " + minion.GetHealth()
            + "\n"
        );
    }

    private void FightDebugLogs(Player player1, Player player2) {
        if (!DEBUG_MESSAGES_ENABLED) return;
        Debug.Log(player1.GetName() + "'s Minions:");
        foreach (Minion minion in player1.GetBattleRoster()) {
            DebugMinion(minion);
        }
        Debug.Log(player2.GetName() + "'s Minions:");
        foreach (Minion minion in player2.GetBattleRoster()) {
            DebugMinion(minion);
        }
        Debug.Log(player1.GetName() + "'s Health: " + player1.GetHealth());
        Debug.Log(player2.GetName() + "'s Health: " + player2.GetHealth());
    }
    private void Fight(Player player1, Player player2) {
        player1.StartBattle();
        player2.StartBattle();

        while (!IsBattleOver(player1, player2)) {
            player1.GetBattlingMinion().Attack(player2.GetBattlingMinion());
            if(IsBattleOver(player1, player2)) break;
            player2.GetBattlingMinion().Attack(player1.GetBattlingMinion());
        }
        // Tie
        if (player1.GetBattleRosterSize() == player2.GetBattleRosterSize()) return;

        Player victor = player1.GetBattleRosterSize() > player2.GetBattleRosterSize() ? player1 : player2;
        Player loser = player1.GetBattleRosterSize() > player2.GetBattleRosterSize() ? player2 : player1;
        loser.TakeDamage(victor.GetBattleRosterSize());

        FightDebugLogs(player1, player2);
    }

    public void TestBattle() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Booka"));
        player1.AddToRoster(new Minion("Inkling"));
        //player1.AddToRoster(new Minion("Sharko"));
        player1.AddToRoster(new Minion("Bubble Blowfish"));
        player1.AddToRoster(new Minion("Chonky Swordfish"));
        player1.AddToRoster(new Minion("Octo Papa"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Fireball"));
        player2.AddToRoster(new Minion("Wall of flame"));
        //player2.AddToRoster(new Minion("Inferno Golem"));
        player2.AddToRoster(new Minion("Whelp Master"));
        player2.AddToRoster(new Minion("Whelp Master"));
        player2.AddToRoster(new Minion("Whelp Master"));
        player2.AddToRoster(new Minion("Whelp Master"));
        //player2.AddToRoster(new Minion("Pheonix"));

        players.Add(player1);
        players.Add(player2);
        Fight(player1, player2);
        Fight(player1, player2);
    }

    public void TestChonkySwordfish() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Chonky Swordfish"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Booka"));

        players.Add(player1);
        players.Add(player2);
        Fight(player1, player2);
    }

    public void TestFishPatrol() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Pheonix"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        //player2.AddToRoster(new Minion("Imp"));
        //player2.AddToRoster(new Minion("Treant"));
        player2.AddToRoster(new Minion("Fish Patrol"));
        player2.AddToRoster(new Minion("Fish Patrol"));
        player2.AddToRoster(new Minion("Fish Patrol"));

        players.Add(player1);
        players.Add(player2);
        Fight(player1, player2);
    }

    public void TestPheonix() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Pheonix"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        //player2.AddToRoster(new Minion("Imp"));
        //player2.AddToRoster(new Minion("Treant"));
        player2.AddToRoster(new Minion("Fireball"));
        player2.AddToRoster(new Minion("Whelp Master"));
        player2.AddToRoster(new Minion("Whelp Master"));
        player2.AddToRoster(new Minion("Whelp Master"));

        players.Add(player1);
        players.Add(player2);
        Fight(player1, player2);
    }
}