using System;
using System.Collections;
using System.Collections.Generic;
public class Effects {
    // The minion who has these effects
    private Minion minion;
    private Minion opponent;
    private List<Effect> onEntryEffects;
    private List<Effect> onDeathEffects;
    private List<Effect> onAttackEffects;
    private List<Effect> onDamageEffects;
    private List<Effect> onKilledOpponentEffects;

    public void SetOpponent(Minion opponent) {
        this.opponent = opponent;
    }

    public void AddOnEntry(Effect effect) {
        onEntryEffects.Add(effect);
    }

    public void AddOnDeath(Effect effect) {
        onDeathEffects.Add(effect);
    }
    public void AddOnAttack(Effect effect) {
        onAttackEffects.Add(effect);
    }

    public void AddOnDamage(Effect effect) {
        onDamageEffects.Add(effect);
    }

    public void AddOnKilledOpponent(Effect effect) {
        onKilledOpponentEffects.Add(effect);
    }

    public Effects(Minion minion) {
        this.minion = minion;
        EffectsData effectsData = EffectsData.GetEffectsData(minion.GetName());
        onEntryEffects = effectsData.GetOnEntryEffects();
        onDeathEffects = effectsData.GetOnDeathEffects();
        onAttackEffects = effectsData.GetOnAttackEffects();
        onDamageEffects = effectsData.GetOnDamageEffects();
        onKilledOpponentEffects = effectsData.GetOnKilledOpponentEffects();
    }

    public void OnEntry() {
        foreach (Effect effect in onEntryEffects) {
            effect(minion, opponent);
        }
    }

    public void OnDeath() {
        foreach (Effect effect in onDeathEffects) {
            effect(minion, opponent);
        }
    }

    public void OnAttack() {
        foreach (Effect effect in onAttackEffects) {
            effect(minion, opponent);
        }
    }

    public void OnDamage() {
        foreach (Effect effect in onDamageEffects) {
            effect(minion, opponent);
        }
    }

    public void OnKilledOpponent() {
        foreach (Effect effect in onKilledOpponentEffects) {
            effect(minion, opponent);
        }
    }
}
