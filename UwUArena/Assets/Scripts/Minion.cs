public class Minion {
	private int level;
	private int health;
	private int attack;
	private string name;
	private Tribe tribe;
	//private delegate void Death();
	//private delegate void Entry();
	//private delegate void Conditional();

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

	}
	public bool IsDead() {
		return health <= 0;
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
		// TODO Give that minion this minion's buffs
		return clone;
	}

	public void Attack (Minion minion) {
		minion.TakeDamage(attack);
		TakeDamage(minion.attack);
	}

	/*public void GiveEffect (delegate DeathEffect, delegate EntryEffect, delegate ConditionalEffect) {
		Death = DeathEffect;
		Entry = EntryEffect;
		// Conditional effects will check their trigger on every "game action" that happens.
		// A game action is an entry, death, attack, damage calculation, minion effect (including conditional effects)
		Conditional = ConditionalEffect;
	}*/
 
    public void GiveStats(int attack, int health) {
        health += health;
		attack += attack;

         if (IsDead()) {
            Death();
		 }
    }

}
