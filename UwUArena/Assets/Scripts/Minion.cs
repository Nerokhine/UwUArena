﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Minion {
	private static int nextID = 0;
	int id;
	private GameObject minionObject;
	private int level;
	private int health;
	private int attack;
	private string name;
	private string effectText;
	private Tribe tribe;
	private Player owner;
	private Minion opponent;
	private List<Minion> location;
	private Effects effects;
	bool finishedDeath;
	bool finishedOnAttack;

	int aquaticKills;

	private bool hasEntered;

	public int GenerateID() {
		nextID ++;
		return nextID;
	}

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

	public int GetID() {
		return id;
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
		finishedDeath = true;
		AddToBattleRecord("Finish Death for " + GetName() + "\n");
		location.Remove(this);
		owner.AddToDeadBattleRoster(this);
	}
	public bool IsDead() {
		return health <= 0;
	}

	public bool GetFinishedDeath() {
		return finishedDeath;
	}

	public bool GetFinishedOnAttack() {
		return finishedOnAttack;
	}

	public void SetFinishedOnAttack(bool finishedOnAttack) {
		this.finishedOnAttack = finishedOnAttack;
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

	public GameObject CreateMinionObject(float xPosition, float yPosition) {
		if (minionObject == null) {
			minionObject = GameObject.Instantiate(Resources.Load("Minion", typeof(GameObject))) as GameObject;
			minionObject.transform.SetParent(GameObject.Find("Canvas").transform);
		}
		return UpdateMinionObject(minionObject, xPosition, yPosition);
	}

	private GameObject UpdateMinionObject(GameObject minionObject, float xPosition, float yPosition) {
		RectTransform rectTransform = minionObject.GetComponent<RectTransform>();
		rectTransform.localPosition = new Vector3(xPosition, yPosition, 0);
		rectTransform.localScale = new Vector3 (0, 0, 0);
		minionObject.transform.Find("Name").GetComponent<Text>().text = name;
		minionObject.transform.Find("Attack").GetComponent<Text>().text = attack.ToString();
		minionObject.transform.Find("Health").GetComponent<Text>().text = health.ToString();
		minionObject.transform.Find("Effect").GetComponent<Text>().text = effectText;
		switch (tribe) {
			case Tribe.Aquatic:
				minionObject.transform.Find("TorusTopHalf").gameObject.GetComponent<Renderer>().material
					= Resources.Load("Material/Blue", typeof(Material)) as Material;
				minionObject.transform.Find("TorusBottomHalf").gameObject.GetComponent<Renderer>().material
					= Resources.Load("Material/Blue", typeof(Material)) as Material;
				break;
			case Tribe.Dragon:
				minionObject.transform.Find("TorusTopHalf").gameObject.GetComponent<Renderer>().material
					= Resources.Load("Material/Gold", typeof(Material)) as Material;
				minionObject.transform.Find("TorusBottomHalf").gameObject.GetComponent<Renderer>().material
					= Resources.Load("Material/Gold", typeof(Material)) as Material;
				break;
			case Tribe.Forest:
				minionObject.transform.Find("TorusTopHalf").gameObject.GetComponent<Renderer>().material
					= Resources.Load("Material/Forest", typeof(Material)) as Material;
				minionObject.transform.Find("TorusBottomHalf").gameObject.GetComponent<Renderer>().material
					= Resources.Load("Material/Forest", typeof(Material)) as Material;
				break;
			case Tribe.Pyro:
				minionObject.transform.Find("TorusTopHalf").gameObject.GetComponent<Renderer>().material
					= Resources.Load("Material/Red", typeof(Material)) as Material;
				minionObject.transform.Find("TorusBottomHalf").gameObject.GetComponent<Renderer>().material
					= Resources.Load("Material/Red", typeof(Material)) as Material;
				break;
			case Tribe.Thunder:
				minionObject.transform.Find("TorusTopHalf").gameObject.GetComponent<Renderer>().material
					= Resources.Load("Material/Yellow", typeof(Material)) as Material;
				minionObject.transform.Find("TorusBottomHalf").gameObject.GetComponent<Renderer>().material
					= Resources.Load("Material/Yellow", typeof(Material)) as Material;
				break;
		}
		return minionObject;
	}

	public void UpdateMinionObject(GameObject minionObject) {
		minionObject.transform.Find("Name").GetComponent<Text>().text = name;
		minionObject.transform.Find("Attack").GetComponent<Text>().text = attack.ToString();
		minionObject.transform.Find("Health").GetComponent<Text>().text = health.ToString();
		minionObject.transform.Find("Effect").GetComponent<Text>().text = effectText;
		this.minionObject = minionObject;
	}

	public GameObject GetMinionObject() {
		return minionObject;
	}

	public Minion (string name) {
		MinionData minionData = MinionData.GetMinionData(name);
		this.name = name;
		this.health = minionData.GetHealth();
		this.attack = minionData.GetAttack();
		this.tribe = minionData.GetTribe();
		this.level = minionData.GetLevel();
		this.effectText = minionData.GetEffectText();
		aquaticKills = 0;
		finishedDeath = false;
		finishedOnAttack = false;
		this.hasEntered = false;
		this.effects = new Effects(this);
		this.id = GenerateID();
	}

	public Minion Copy(bool keepID = false) {
		Minion clone = new Minion(name);
		clone.health = health;
		clone.attack = attack;
		clone.effects = effects.Clone(clone);
		clone.owner = owner;
		clone.location = location;
		if (keepID) {
			clone.finishedDeath = finishedDeath;
			clone.finishedOnAttack = finishedOnAttack;
			clone.id = id;
		}
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
			Minion minion = GetOwner().GetBattlingMinion();
			Minion newOpponent = opponent.GetOwner().GetBattlingMinion();
			if (minion != null && newOpponent != null) minion.Fight(newOpponent);
			return;
		}
		effects.OnAttack();
		if (!IsDead() && !opponent.IsDead()) {
			int opponentAttack = opponent.GetAttack();
			int thisAttack = GetAttack();
			opponent.TakeDamage(thisAttack);
			TakeDamage(opponentAttack);
		}
	}

	public void EnterBattle () {
		if (!IsDead()) {
			if (!hasEntered) {
				hasEntered = true;
				GetOwner().ApplyGifts(this);
				GetOwner().ApplyTraps(this);
				effects.Gifts();
				effects.Traps();
				effects.OnEntry();
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
