﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion {
	private int health;
	private int attack;
	private string name;
	private Tribe tribe;
	private delegate void Death();
	private delegate void Entry();
	private delegate void Conditional();
	

	public void IsDead() {
		return health <= 0;
	}

	public void TakeDamage (int damage) 
    {
        health -= damage;
 
		if (IsDead()) {
			Death();
		}

    }

	public constructor Minion () {
		Initializer.Initialize(this);
	}

	public void GiveEffect (delegate DeathEffect, delegate EntryEffect, delegate ConditionalEffect) {
		Death = DeathEffect;
		Entry = EntryEffect;
		// Conditional effects will check their trigger on every "game action" that happens.
		// A game action is an entry, death, attack, damage calculation, minion effect (including conditional effects)
		Conditional = ConditionalEffect;
	}
 
    public void GiveStats(int attack, int health) {
        health += health;
		attack += attack;

         if (IsDead()) {
            Death();
		 }
    }

}
