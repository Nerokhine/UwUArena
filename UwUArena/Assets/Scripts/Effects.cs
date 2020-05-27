using System;
using System.Collections;
using System.Collections.Generic;
public class Effects {
    // The minion who has these effects
    private Minion minion;
    private delegate void Effect(Minion opponent);
    private List<Effect> entryEffects;
    private List<Effect> deathEffects;
    private List<Effect> attackEffects;
    private List<Effect> onDamageEffects;
    private List<Effect> onKilledOpponentEffects;

    public Effects(Minion minion) {
        this.minion = minion;
        entryEffects = new List<Effect>();
        deathEffects = new List<Effect>();
        attackEffects = new List<Effect>();
        onDamageEffects = new List<Effect>();
        onKilledOpponentEffects = new List<Effect>();
        if (minion.GetName() == "Fireball") {
            attackEffects.Add((Minion opponent) => {opponent.TakeDamage(minion.GetAttack(), minion);});
        }
    }

    public void OnEntry(Minion opponent) {
        foreach (Effect effect in entryEffects) {
            effect(opponent);
        }
    }

    public void OnDeath(Minion opponent) {
        foreach (Effect effect in deathEffects) {
            effect(opponent);
        }
    }

    public void OnAttack(Minion opponent) {
        foreach (Effect effect in attackEffects) {
            effect(opponent);
        }
    }

    public void OnDamage(Minion opponent) {
        foreach (Effect effect in onDamageEffects) {
            effect(opponent);
        }
    }

    public void OnKilledOpponent(Minion opponent) {
        foreach (Effect effect in onKilledOpponentEffects) {
            effect(opponent);
        }
    }
}
