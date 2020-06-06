using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test: MonoBehaviour {
    public Battle battle;

    private Battle StartBattle(Player player1, Player player2) {
        battle.Fight(player1, player2);
        return battle;
    }
    public Battle TestBattle() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Booka"));
        player1.AddToRoster(new Minion("Inkling"));
        player1.AddToRoster(new Minion("Bubble Blowfish"));
        player1.AddToRoster(new Minion("Chonky Swordfish"));
        player1.AddToRoster(new Minion("Octo Papa"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Fireball"));
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Whelp Master"));
        player2.AddToRoster(new Minion("Whelp Master"));
        player2.AddToRoster(new Minion("Whelp Master"));
        player2.AddToRoster(new Minion("Whelp Master"));

        Battle battle = StartBattle(player1, player2);
        return battle;
    }

    public void TestChonkySwordfish() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Chonky Swordfish"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Booka"));

        StartBattle(player1, player2);
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

        StartBattle(player1, player2);
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

        StartBattle(player1, player2);
    }

    public void TestImp() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Imp"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));

        StartBattle(player1, player2);
    }

    public Battle TestWallOfFlame() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Pheonix"));
        player1.AddToRoster(new Minion("Pheonix"));
        player1.AddToRoster(new Minion("Pheonix"));
        player1.AddToRoster(new Minion("Pheonix"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Pheonix"));

        return StartBattle(player1, player2);
    }
    
    public void TestBabyColossus() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Pheonix"));
        player1.AddToRoster(new Minion("Pheonix"));
        player1.AddToRoster(new Minion("Pheonix"));
        player1.AddToRoster(new Minion("Pheonix"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Baby Colossus"));
        player2.AddToRoster(new Minion("Baby Colossus"));
        player2.AddToRoster(new Minion("Imp"));
        player2.AddToRoster(new Minion("Imp"));
        player2.AddToRoster(new Minion("Imp"));

        StartBattle(player1, player2);
    }

    public void TestButtSniffer() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Pheonix"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Butt sniffer"));

        StartBattle(player1, player2);
    }

    public void TestInkling() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Inkling"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Inkling"));

        StartBattle(player1, player2);
    }
}