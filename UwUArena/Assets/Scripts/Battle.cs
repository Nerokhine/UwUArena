using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO Certain Functions and objects are kept server side
public class Battle {
    private List<Player> players = new List<Player>();
    private List<KeyValuePair<List<Player>, string>> battleRecord;

    private bool DEBUG_MESSAGES_ENABLED = true;

    public void AddToBattleRecord(Player player1, Player player2, string message) {
        List<Player> record = new List<Player>();
        record.Add(player1.Clone());
        record.Add(player2.Clone());
        battleRecord.Add(new KeyValuePair<List<Player>,string>(record, message));
    }

    public List<Player> GetPlayers() {
        return players;
    }

    private bool IsBattleOver(Player player1, Player player2) {
        return !((player1.GetBattleRosterSize() > 0) && (player2.GetBattleRosterSize() > 0));
    }

    private string DebugMinion(Minion minion) {
        return "Name: " + minion.GetName() + ", Attack: " + minion.GetAttack() + ", Health: " + minion.GetHealth() + "\n";
    }

    private string DebugTraps(Player player) {
        List<List<Effect>> traps = player.GetTraps();
        if (traps.Count <= 0) return "";
        int numberOfDifferentTraps = traps[0].Count;
        List<int> trapsAmounts = new List<int>();
        for (int i = 0; i < numberOfDifferentTraps; i++) {
            trapsAmounts.Add(0);
            for(int j = 0; j < traps.Count; j ++) {
                if (traps[j].Count <= i + 1) {
                    trapsAmounts[i] ++;
                }
            }
        }
        string debug = "Traps: ";
        foreach (int trapCount in trapsAmounts) {
           debug += trapCount + ",";
        }
        debug += "\n";
        return debug;
    }

    private string DebugGifts(Player player) {
        List<List<Effect>> gifts = player.GetGifts();
        if (gifts.Count <= 0) return "";
        int numberOfDifferentGifts = gifts[0].Count;
        List<int> giftsAmounts = new List<int>();
        for (int i = 0; i < numberOfDifferentGifts; i++) {
            giftsAmounts.Add(0);
            for(int j = 0; j < gifts.Count; j ++) {
                if (gifts[j].Count <= i + 1) {
                    giftsAmounts[i] ++;
                }
            }
        }
        string debug = "Gifts: ";
        foreach (int giftCount in giftsAmounts) {
           debug += giftCount + ",";
        }
        debug += "\n";
        return debug;
    }

    private void FightDebugLogs() {
        if (!DEBUG_MESSAGES_ENABLED) return;
        string debug = "";
        foreach(KeyValuePair<List<Player>,string> valuePair in battleRecord) {
            List<Player> playerList = valuePair.Key;
            Player player1 = playerList[0];
            Player player2 = playerList[1];
            debug += valuePair.Value;
            debug += DebugTraps(player1);
            debug += DebugGifts(player1);
            debug += player1.GetName() + "'s Minions:\n";
            foreach (Minion minion in player1.GetBattleRoster()) {
                debug += DebugMinion(minion);
            }
            debug += "\n";
            debug += DebugTraps(player2);
            debug += DebugGifts(player2);
            debug += player2.GetName() + "'s Minions:\n";
            foreach (Minion minion in player2.GetBattleRoster()) {
                debug += DebugMinion(minion);
            }
            debug += "\n==========================================\n";
        }
        Player lastPlayer1 = battleRecord[battleRecord.Count - 1].Key[0];
        Player lastPlayer2 = battleRecord[battleRecord.Count - 1].Key[1];
        debug += lastPlayer1.GetName() + "'s Health: " + lastPlayer1.GetHealth() + "\n";
        debug += lastPlayer2.GetName() + "'s Health: " + lastPlayer2.GetHealth() + "\n";
        Debug.Log(debug);
    }
    private void Fight(Player player1, Player player2) {
        battleRecord = new  List<KeyValuePair<List<Player>, string>>();
        player1.StartBattle(this);
        player2.StartBattle(this);

        while (!IsBattleOver(player1, player2)) {
            player1.GetBattlingMinion().Fight(player2.GetBattlingMinion());
            if(IsBattleOver(player1, player2)) break;
            player2.GetBattlingMinion().Fight(player1.GetBattlingMinion());
        }
        // Tie
        if (player1.GetBattleRosterSize() == player2.GetBattleRosterSize()) return;

        Player victor = player1.GetBattleRosterSize() > player2.GetBattleRosterSize() ? player1 : player2;
        Player loser = player1.GetBattleRosterSize() > player2.GetBattleRosterSize() ? player2 : player1;
        loser.TakeDamage(victor.GetBattleRosterSize());

        AddToBattleRecord(player1, player2, "End of Battle:\n");
        FightDebugLogs();
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

    public void TestImp() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Imp"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));

        players.Add(player1);
        players.Add(player2);
        Fight(player1, player2);
    }

    public void TestWallOfFlame() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        //player1.AddToRoster(new Minion("Fish Patrol"));
        //player1.AddToRoster(new Minion("Pheonix"));
        player1.AddToRoster(new Minion("Pheonix"));
        player1.AddToRoster(new Minion("Pheonix"));
        player1.AddToRoster(new Minion("Pheonix"));
        player1.AddToRoster(new Minion("Pheonix"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Pheonix"));

        players.Add(player1);
        players.Add(player2);
        Fight(player1, player2);
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

        players.Add(player1);
        players.Add(player2);
        Fight(player1, player2);
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

        players.Add(player1);
        players.Add(player2);
        Fight(player1, player2);
    }

    public void TestInkling() {
        // Initialize Player 1
        Player player1 = new Player("Nik", health:30, coins:3);
        player1.AddToRoster(new Minion("Inkling"));

        // Initialize Player 2
        Player player2 = new Player("Computer", health:30, coins:3);
        player2.AddToRoster(new Minion("Wall of flame"));
        player2.AddToRoster(new Minion("Inkling"));

        players.Add(player1);
        players.Add(player2);
        Fight(player1, player2);
    }
}