using System;
using System.Collections;
using System.Collections.Generic;
public class Effects {
    // The minion who has these effects
    private Minion minion;
    private List<Effect> onEntryEffects;
    private List<Effect> onDeathEffects;
    private List<Effect> onAttackEffects;
    private List<Effect> onDamageEffects;
    private List<Effect> onKilledOpponentEffects;

    public Effects(Minion minion) {
        this.minion = minion;
        EffectsData effectsData = EffectsData.GetEffectsData(minion.GetName());
        onEntryEffects = effectsData.GetOnEntryEffects();
        onDeathEffects = effectsData.GetOnDeathEffects();
        onAttackEffects = effectsData.GetOnAttackEffects();
        onDamageEffects = effectsData.GetOnDamageEffects();
        onKilledOpponentEffects = effectsData.GetOnKilledOpponentEffects();
    }

    public void OnEntry(Minion opponent) {
        foreach (Effect effect in onEntryEffects) {
            effect(minion, opponent);
        }
    }

    public void OnDeath(Minion opponent) {
        foreach (Effect effect in onDeathEffects) {
            effect(minion, opponent);
        }
    }

    public void OnAttack(Minion opponent) {
        foreach (Effect effect in onAttackEffects) {
            effect(minion, opponent);
        }
    }

    public void OnDamage(Minion opponent) {
        foreach (Effect effect in onDamageEffects) {
            effect(minion, opponent);
        }
    }

    public void OnKilledOpponent(Minion opponent) {
        foreach (Effect effect in onKilledOpponentEffects) {
            effect(minion, opponent);
        }
    }
}
