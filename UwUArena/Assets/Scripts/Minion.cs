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

	public void AddToBattleRecord(string message) {
		GetOwner().GetBattle().AddToBattleRecord(GetOwner(), opponent.GetOwner(), message);
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
		if (location == null) throw new System.ArgumentException(name + " has no location", "location");
		opponent.GetEffects().OnKilledOpponent();
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
		if (health <= 0) {
			health -= damage;
			effects.OnDamage();
			return;
		}

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
		clone.owner = owner;
		clone.location = location;
		// TODO Give that minion this minion's buffs
		return clone;
	}

	public void Fight(Minion opponent) {
		opponent.SetOpponent(this);
		SetOpponent(opponent);
		EnterBattle();
		opponent.EnterBattle();
		Attack(opponent);
	}

	public void Attack (Minion opponent) {
		if (IsDead() || opponent.IsDead()) {
			GetOwner().GetBattlingMinion().Fight(opponent.GetOwner().GetBattlingMinion());
			return;
		}
		effects.OnAttack();
		if (!opponent.IsDead()) {
			int opponentAttack = opponent.GetAttack();
			int thisAttack = GetAttack();
			opponent.TakeDamage(thisAttack);
			TakeDamage(opponentAttack);
		}
	}

	public void EnterBattle () {
		if (!IsDead()) {
			if (!hasEntered) {
				GetOwner().ApplyGifts(this);
				GetOwner().ApplyTraps(this);
				effects.Gifts();
				effects.Traps();
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
