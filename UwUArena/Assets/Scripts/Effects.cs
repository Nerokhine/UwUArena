using System;
using System.Collections;
using System.Collections.Generic;
public class Effects {
    // The minion who has these effects
    private Minion minion;
    private Minion opponent;
    private List<Effect> trapEffects;
    private List<Effect> giftEffects;
    private List<Effect> onEntryEffects;
    private List<Effect> onDeathEffects;
    private List<Effect> onAttackEffects;
    private List<Effect> onDamageEffects;
    private List<Effect> onKilledOpponentEffects;

    public void SetOpponent(Minion opponent) {
        this.opponent = opponent;
    }

    public void AddGift(Effect effect) {
        giftEffects.Add(effect);
    }

    public void AddTrap(Effect effect) {
        trapEffects.Add(effect);
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

    public List<Effect> GetOnEntry() {
        return onEntryEffects;
    }
    public List<Effect> GetOnDeath() {
        return onDeathEffects;
    }
    public List<Effect> GetOnAttack() {
        return onAttackEffects;
    }
    public List<Effect> GetOnDamage() {
        return onDamageEffects;
    }
    public List<Effect> GetOnKilledOpponent() {
        return onKilledOpponentEffects;
    }

    public Effects Clone(Minion minion) {
        Effects effects = new Effects(minion);
        effects.giftEffects = new List<Effect>();
        effects.trapEffects = new List<Effect>();
        effects.onEntryEffects = new List<Effect>();
        effects.onDeathEffects = new List<Effect>();
        effects.onAttackEffects = new List<Effect>();
        effects.onDamageEffects = new List<Effect>();
        effects.onKilledOpponentEffects = new List<Effect>();
        foreach(Effect effect in onEntryEffects) {
            effects.AddOnEntry(effect);
        }
        foreach(Effect effect in onDeathEffects) {
            effects.AddOnDeath(effect);
        }
        foreach(Effect effect in onAttackEffects) {
            effects.AddOnAttack(effect);
        }
        foreach(Effect effect in onDamageEffects) {
            effects.AddOnDamage(effect);
        }
        foreach(Effect effect in onKilledOpponentEffects) {
            effects.AddOnKilledOpponent(effect);
        }

        return effects;
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
        minion.AddToBattleRecord("Start OnEntry for " + minion.GetName() + "\n");
        foreach (Effect effect in onEntryEffects) {
            effect(minion, opponent);
        }
        minion.AddToBattleRecord("Finish OnEntry for " + minion.GetName() + "\n");
    }

    public void OnDeath() {
        minion.AddToBattleRecord("Start OnDeath for " + minion.GetName() + "\n");
        foreach (Effect effect in onDeathEffects) {
            effect(minion, opponent);
        }
        minion.AddToBattleRecord("Finish OnDeath for " + minion.GetName() + "\n");
    }

    public void OnAttack() {
        minion.AddToBattleRecord("Start OnAttack for " + minion.GetName() + "\n");
        foreach (Effect effect in onAttackEffects) {
            effect(minion, opponent);
        }
        minion.SetFinishedOnAttack(true);
        minion.AddToBattleRecord("Finish OnAttack for " + minion.GetName() + "\n");
        minion.SetFinishedOnAttack(false);
    }

    public void OnDamage() {
        minion.AddToBattleRecord("Start OnDamage for " + minion.GetName() + "\n");
        foreach (Effect effect in onDamageEffects) {
            effect(minion, opponent);
        }
        minion.AddToBattleRecord("Finish OnDamage for " + minion.GetName() + "\n");
    }

    public void OnKilledOpponent() {
        minion.AddToBattleRecord("Start OnKilledOpponent for " + minion.GetName() + "\n");
        foreach (Effect effect in onKilledOpponentEffects) {
            effect(minion, opponent);
        }
        minion.AddToBattleRecord("Finish OnKilledOpponent for " + minion.GetName() + "\n");
    }

    public void Traps() {
        minion.AddToBattleRecord("Start Traps for " + minion.GetName() + "\n");
        foreach (Effect effect in trapEffects) {
            effect(minion, opponent);
        }
        minion.AddToBattleRecord("Finish Traps for " + minion.GetName() + "\n");
    }

    public void Gifts() {
        minion.AddToBattleRecord("Start Gifts for " + minion.GetName() + "\n");
        foreach (Effect effect in giftEffects) {
            effect(minion, opponent);
        }
        minion.AddToBattleRecord("Finish Gifts for " + minion.GetName() + "\n");
    }
}
