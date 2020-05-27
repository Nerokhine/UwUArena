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
	private List<Minion> location;
	private Effects effects;

	private bool hasEntered;

	public int GetLevel() {
		return level;
	}

	public int GetHealth() {
		return health;
	}

	public int GetAttack() {
		return attack;
	}

	public string GetName() {
		return name;
	}

	public Tribe GetTribe() {
		return tribe;
	}
	
	private void Death(Minion opponent) {
		if (location == null) throw new System.ArgumentException("Minion has no location", "location");
		effects.OnDeath(opponent);
		location.Remove(this);
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

	public List<Minion> GetLocation() {
		return location;
	}

	public void SetLocation(List<Minion> location) {
		this.location = location;
	}

	public void TakeDamage (int damage, Minion opponent)
    {
		effects.OnDamage(opponent);
        health -= damage;
 
		if (IsDead()) {
			Death(opponent);
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
		this.hasEntered = false;
		this.effects = new Effects(this);
	}

	public Minion Clone() {
		Minion clone = new Minion(name);
		clone.location = location;
		clone.owner = owner;
		// TODO Give that minion this minion's buffs
		return clone;
	}

	public void Attack (Minion opponent) {
		opponent.EnterBattle(this);
		EnterBattle(opponent);

		effects.OnAttack(opponent);
		opponent.TakeDamage(GetAttack(), this);
		TakeDamage(opponent.GetAttack(), opponent);

		if (opponent.IsDead()) effects.OnKilledOpponent(opponent);
	}

	public void EnterBattle (Minion opponent) {
		if (!hasEntered) {
			effects.OnEntry(opponent);
			hasEntered = true;
		}
	}
 
    public void GiveStats(int attack, int health) {
		if (attack > 0 && health > 0) {
			health += health;
			attack += attack;
		}
    }

}
