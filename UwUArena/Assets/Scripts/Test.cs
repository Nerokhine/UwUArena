using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Test {
    private static Battle StartBattle(Player player1, Player player2) {
        Battle battle = new Battle();
        battle.Fight(player1, player2);
        return battle;
    }
    public static Battle TestBattle() {
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

        return StartBattle(player1, player2);
    }

    public static Battle TestChonkySwordfish() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Chonky Swordfish"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Booka"));

        return StartBattle(player1, player2);
    }

    public static Battle TestFishPatrol() {
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

        return StartBattle(player1, player2);
    }

    public static Battle TestPheonix() {
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

        return StartBattle(player1, player2);
    }

    public static Battle TestImp() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Imp"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));

        return StartBattle(player1, player2);
    }

    public static Battle TestWallOfFlame() {
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
    
    public static Battle TestBabyColossus() {
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

        return StartBattle(player1, player2);
    }

    public static Battle TestButtSniffer() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Pheonix"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Butt sniffer"));

        return StartBattle(player1, player2);
    }

    public static Battle TestInkling() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Inkling"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Inkling"));

        return StartBattle(player1, player2);
    }
}