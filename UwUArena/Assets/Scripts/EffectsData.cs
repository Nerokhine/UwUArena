using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EffectsData {
    private static Dictionary<string, EffectsData> effectsData = new Dictionary<string, EffectsData>();
    private List<Effect> onEntryEffects;
    private List<Effect> onDeathEffects;
    private List<Effect> onAttackEffects;
    private List<Effect> onDamageEffects;
    private List<Effect> onKilledOpponentEffects;

    public List<Effect> GetOnEntryEffects() {
        List<Effect> entryEffects = new List<Effect>();
        foreach(Effect effect in onEntryEffects) {
            entryEffects.Add(effect);
        }
        return entryEffects;
    }

    public List<Effect> GetOnDeathEffects() {
        List<Effect> deathEffects = new List<Effect>();
        foreach(Effect effect in onDeathEffects) {
            deathEffects.Add(effect);
        }
        return deathEffects;
    }

    public List<Effect> GetOnAttackEffects() {
        List<Effect> attackEffects = new List<Effect>();
        foreach(Effect effect in onAttackEffects) {
            attackEffects.Add(effect);
        }
        return attackEffects;
    }

    public List<Effect> GetOnDamageEffects() {
        List<Effect> damageEffects = new List<Effect>();
        foreach(Effect effect in onDamageEffects) {
            damageEffects.Add(effect);
        }
        return damageEffects;
    }

    public List<Effect> GetOnKilledOpponentEffects() {
        List<Effect> killedOpponentEffects = new List<Effect>();
        foreach(Effect effect in onKilledOpponentEffects) {
            killedOpponentEffects.Add(effect);
        }
        return killedOpponentEffects;
    }

    public static EffectsData GetEffectsData(string name) {
        return effectsData[name];
    }

    private EffectsData(string name) {
        onEntryEffects = new List<Effect>();
        onDeathEffects = new List<Effect>();
        onAttackEffects = new List<Effect>();
        onDamageEffects = new List<Effect>();
        onKilledOpponentEffects = new List<Effect>();

        switch(name) {
            case "Fireball":
                /*onAttackEffects.Add((Minion minion, Minion opponent) => {
                    opponent.TakeDamage(minion.GetAttack(), minion);
                });*/
                break;
            case "Chonky Swordfish":
                onDamageEffects.Add((Minion minion, Minion opponent) => {
                    minion.SetAttack(minion.GetAttack() + 1);
                });
            break;
        }
        effectsData.Add(name, this);
    }

    public static void Initialize() {
        if (MinionData.GetMinionData() == null) throw new System.ArgumentException("Invalid Player Roster Index When Buffing Minion", "index");
        effectsData = new Dictionary<string, EffectsData>();
        List<MinionData> minionDataList = MinionData.GetMinionData();
        foreach (MinionData minionData in minionDataList) {
            effectsData[minionData.GetName()] = new EffectsData(minionData.GetName());
        }
    }
}