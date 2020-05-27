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
	
	private void Death() {
		if (location == null) throw new System.ArgumentException("Minion has no location", "location");
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

	public void TakeDamage (int damage) 
    {
        health -= damage;
 
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
	}

	public Minion Clone() {
		Minion clone = new Minion(name);
		clone.location = location;
		clone.owner = owner;
		// TODO Give that minion this minion's buffs
		return clone;
	}

	public void Attack (Minion minion) {
		minion.TakeDamage(GetAttack());
		TakeDamage(minion.GetAttack());
	}
 
    public void GiveStats(int attack, int health) {
        health += health;
		attack += attack;

         if (IsDead()) {
            Death();
		 }
    }

}
