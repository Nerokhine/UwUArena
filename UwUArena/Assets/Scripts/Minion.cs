using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Minion {
	private int level;
	private int health;
	private int attack;
	private string name;
	private Tribe tribe;
	private Player owner;
	private Minion opponent;
	private List<Minion> location;
	private Effects effects;

	int aquaticKills;

	private bool hasEntered;

	public int GetLevel() {
		return level;
	}

	public int GetHealth() {
		return health;
	}

	public void SetHealth(int health) {
		this.health = health;
		if (IsDead()) {
			Death();
		}
	}

	public int GetAttack() {
		return attack;
	}

	public void SetAttack(int attack) {
		if (attack <= 0) {
			this.attack = 0;
		} else {
			this.attack = attack;
		}
	}

	public string GetName() {
		return name;
	}

	public Tribe GetTribe() {
		return tribe;
	}

	public int GetAquaticKills() {
		return aquaticKills;
	}
	public void IncrementAquaticKills() {
		aquaticKills ++;
	}

	public Effects GetEffects() {
		return effects;
	}
	
	private void Death() {
		Debug.Log(name + " owned by " + GetOwner().GetName() + " has died");
		if (location == null) throw new System.ArgumentException(name + " has no location", "location");
		effects.OnDeath();
		location.Remove(this);
		owner.AddToDeadBattleRoster(this);
	}
	public bool IsDead() {
		return health <= 0;
	}

	public Player GetOwner() {
		return owner;
	}

	public void SetOwner(Player owner) {
		this.owner = owner;
	}

	public void SetOpponent(Minion opponent) {
		this.opponent = opponent;
		effects.SetOpponent(opponent);
	}

	public Minion GetOpponent() {
		return opponent;
	}

	public List<Minion> GetLocation() {
		return location;
	}

	public void SetLocation(List<Minion> location) {
		this.location = location;
	}

	public void TakeDamage (int damage)
    {
        health -= damage;
		effects.OnDamage();
 
		if (IsDead()) {
			Death();
		}
    }

	public bool Buff(Buff buff) {
		return false;
	}

	public Minion (string name) {
		MinionData minionData = MinionData.GetMinionData(name);
		this.name = name;
		this.health = minionData.GetHealth();
		this.attack = minionData.GetAttack();
		this.tribe = minionData.GetTribe();
		this.level = minionData.GetLevel();
		aquaticKills = 0;
		this.hasEntered = false;
		this.effects = new Effects(this);
	}

	public Minion Copy() {
		Minion clone = new Minion(name);
		clone.health = health;
		clone.attack = attack;
		clone.effects = effects.Clone(clone);
		// TODO Give that minion this minion's buffs
		return clone;
	}

	public void Attack (Minion opponent) {
		Debug.Log(name + " Stats" + attack + "/" + health);
		Debug.Log(opponent.name + " Stats" + opponent.attack + "/" + opponent.health);
		opponent.SetOpponent(this);
		SetOpponent(opponent);
		EnterBattle();
		opponent.EnterBattle();

		effects.OnAttack();
		if (!opponent.IsDead()) {
			int opponentAttack = opponent.GetAttack();
			int thisAttack = GetAttack();
			opponent.TakeDamage(thisAttack);
			TakeDamage(opponentAttack);
		}

		if (opponent.IsDead()) effects.OnKilledOpponent();
		if (IsDead()) opponent.GetEffects().OnKilledOpponent();
	}

	public void EnterBattle () {
		if (!IsDead()) {
			if (!hasEntered) {
				effects.OnEntry();
				hasEntered = true;
			}
		}
	}
 
    public void GiveStats(int attack, int health) {
		if (attack > 0 && health > 0) {
			health += health;
			attack += attack;
		}
    }

}
