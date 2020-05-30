using System;
using System.Collections;
using System.Collections.Generic;
public class Player {
    private static int MAX_NUMBER_OF_COINS = 10;
    private static int MAX_BUFFS_IN_HAND = 6;
    private static int MAX_MINIONS_IN_HAND = 6;
    private static int MAX_ROSTER_SIZE = 6;
    private static int MINION_COST = 3;
    private static int BUFF_COST = 3;
    private static int MINION_SELL_AMOUNT = 1;
    private static int BUFF_SELL_AMOUNT = 1;
    private List<Minion> minionsInHand;
    private List<Buff> buffsInHand;
    private List<Minion> roster;
    private List<Minion> battleRoster;
    private List<Minion> deadBattleRoster;
    private int health;
    private int maxCoins;
    private int coins;
    private string name;

    public Player Clone() {
        Player clone = new Player(name, health, coins);
        foreach(Minion minion in minionsInHand) {
            clone.AddToHand(minion.Copy());
        }
        foreach(Buff buff in buffsInHand) {
            // TODO buff copy and buff class
            clone.AddToHand(buff);
        }
        foreach(Minion minion in roster) {
            clone.AddToRoster(minion.Copy());
        }
        foreach(Minion minion in battleRoster) {
            clone.AddToBattleRoster(minion.Copy());
        }
        foreach(Minion minion in deadBattleRoster) {
            clone.AddToDeadBattleRoster(minion.Copy());
        }

        return clone;
    }
    public Player(string name, int health, int coins) {
        this.name = name;
        this.health = health;
        this.coins = coins;
        this.maxCoins = coins;
        this.minionsInHand = new List<Minion>();
        this.buffsInHand = new List<Buff>();
        this.roster = new List<Minion>();
        this.battleRoster = new List<Minion>();
        this.deadBattleRoster = new List<Minion>();
    }

    public string GetName() {
        return name;
    }

    private bool EnoughMoney(Buff buff) {
        return coins >= BUFF_COST;
    }

    private bool EnoughMoney(Minion minion) {
        return coins >= MINION_COST;
    }

    private bool EnoughSpaceInHand(Buff buff) {
        return buffsInHand.Count < MAX_BUFFS_IN_HAND;
    }
    private bool EnoughSpaceInHand(Minion minion) {
        return minionsInHand.Count < MAX_MINIONS_IN_HAND;
    }

    private bool EnoughSpaceInRoster(List<Minion> roster) {
        return roster.Count < MAX_ROSTER_SIZE;
    }

    private bool CanBuy(Minion minion) {
        return EnoughMoney(minion) && EnoughSpaceInHand(minion);
    }

    private bool CanBuy(Buff buff) {
        return EnoughMoney(buff) && EnoughSpaceInHand(buff);
    }

    public bool AddToHand(Minion minion) {
        if (EnoughSpaceInHand(minion)) {
            minion.SetOwner(this);
            minion.SetLocation(minionsInHand);
            minionsInHand.Add(minion);
            return true;
        }
        return false;
    }

    public bool AddToHand(Buff buff) {
        if (EnoughSpaceInHand(buff)) {
            buffsInHand.Add(buff);
            return true;
        }
        return false;
    }

    public bool Buy(Buff buff) {
        if (CanBuy(buff)) {
            coins -= BUFF_COST;
            AddToHand(buff);
            return true;
        }
        return false;
    }

    public bool Buy(Minion minion) {
        if (CanBuy(minion)) {
            coins -= MINION_COST;
            AddToHand(minion);
            return true;
        }
        return false;
    }

    public bool BuffMinion(Buff buff, int index) {
        if (index >= 0 && index < roster.Count) {
            Minion minion = roster[index];
            if (minion.Buff(buff)) {
                return true;
            } else {
                return false;
            }
        } else {
            throw new System.ArgumentException("Invalid Player Roster Index When Buffing Minion", "index");
        }
    }

    public void Sell(Minion minion) {
        coins = Math.Min(coins + MINION_SELL_AMOUNT, MAX_NUMBER_OF_COINS);
        // TODO do this better. Add the minion to the store
        // Possible TODO (add the buffs on it back to the store)
        roster.Remove(minion);
        minionsInHand.Remove(minion);
    }

    public void Sell(Buff buff) {
        coins = Math.Min(coins + BUFF_SELL_AMOUNT, MAX_NUMBER_OF_COINS);
        //buffsInHand.Remove(buff);
        // TODO Add the buff to the store
    }

    public bool AddToRoster(Minion minion, int index) {
        if (!EnoughSpaceInRoster(roster)) return false;
        minion.SetOwner(this);
        minion.SetLocation(roster);
        roster.Insert(index, minion);
        return true;
    }

    public bool AddToRoster(Minion minion) {
        if (!EnoughSpaceInRoster(roster)) return false;
        minion.SetOwner(this);
        minion.SetLocation(roster);
        roster.Add(minion);
        return true;
    }

    public bool AddToBattleRoster(Minion minion, int index) {
        if (!EnoughSpaceInRoster(battleRoster)) return false;
        minion.SetOwner(this);
        minion.SetLocation(battleRoster);
        battleRoster.Insert(index, minion);
        return true;
    }

    public bool AddToBattleRoster(Minion minion) {
        if (!EnoughSpaceInRoster(battleRoster)) return false;
        minion.SetOwner(this);
        minion.SetLocation(battleRoster);
        battleRoster.Add(minion);
        return true;
    }

    public bool AddToDeadBattleRoster(Minion minion) {
        minion.SetOwner(this);
        minion.SetLocation(deadBattleRoster);
        deadBattleRoster.Add(minion);
        return true;
    }

    /* 
        Removes and returns the minion from the roster
        index: index of the minion in the roster
    */
    /*public Minion GetRosterMinion(int index) {
        if (index >= 0 && index < roster.Count) {
            Minion minion = roster[index];
            roster.RemoveAt(index);
            return minion;
        } else {
            throw new System.ArgumentException("Invalid Player Roster Index When Retreiving Minion", "index");
        }
    }*/

    /* 
        Removes and returns the minion from the battle roster
        index: index of the minion in the roster
    */
    /*public Minion GetBattleRosterMinion(int index) {
        if (index >= 0 && index < battleRoster.Count) {
            Minion minion = battleRoster[index];
            battleRoster.RemoveAt(index);
            return minion;
        } else {
            throw new System.ArgumentException("Invalid Player Battle Roster Index When Retreiving Minion", "index");
        }
    }*/

    public List<Minion> GetBattleRoster() {
        return battleRoster;
        // TODO move print battle roster here and remove this function
    }

    public List<Minion> GetDeadBattleRoster() {
        return deadBattleRoster;
        // TODO move print battle roster here and remove this function
    }

    public int GetBattleRosterSize() {
        return battleRoster.Count;
    }

    public void TakeDamage(int damage) {
        health -= damage;
    }

    public void StartTurn() {
        if (maxCoins < MAX_NUMBER_OF_COINS) maxCoins ++;
        coins = maxCoins;
    }

    public int GetHealth() {
        return health;
    }

    private void InitializeBattleRoster() {
        battleRoster = new List<Minion>();
        deadBattleRoster = new List<Minion>();
        foreach(Minion minion in roster) {
            AddToBattleRoster(minion.Copy());
        }
    }

    public Minion GetBattlingMinion() {
        return battleRoster[0];
    }

    public void StartBattle() {
        InitializeBattleRoster();
    }



}