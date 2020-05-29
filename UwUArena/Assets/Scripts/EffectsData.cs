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

    private EffectsData(MinionData minionData) {
        onEntryEffects = new List<Effect>();
        onDeathEffects = new List<Effect>();
        onAttackEffects = new List<Effect>();
        onDamageEffects = new List<Effect>();
        onKilledOpponentEffects = new List<Effect>();

        switch(minionData.GetName()) {
            // Chonky Boi
            case "Chonky Swordfish":
                onDamageEffects.Add((Minion minion, Minion opponent) => {
                    minion.SetAttack(minion.GetAttack() + 1);
                });
                break;
            case "Octo Papa":
                onEntryEffects.Add((Minion minion, Minion opponent) => {
                    int totalAquaticKills = 0;
                    foreach (Minion deadMinion in minion.GetOwner().GetDeadBattleRoster()) {
                        totalAquaticKills += deadMinion.GetAquaticKills();
                    }
                    foreach (Minion aliveMinion in minion.GetOwner().GetBattleRoster()) {
                        totalAquaticKills += aliveMinion.GetAquaticKills();
                    }
                    minion.SetAttack(minion.GetAttack() + 3 * totalAquaticKills);
                    minion.SetHealth(minion.GetHealth() + 3 * totalAquaticKills);
                });
                break;
            case "Pheonix":
                onAttackEffects.Add((Minion minion, Minion opponent) => {
                    Debug.Log(minion.GetHealth());
                    if (minion.GetHealth() <= 0) return;
                    Minion pheonix = minion.Clone();
                    pheonix.SetAttack(pheonix.GetAttack() - 1);
                    pheonix.SetHealth(pheonix.GetHealth() - 1);
                    if (!pheonix.IsDead()) pheonix.GetLocation().Add(pheonix);
                });
                break;
        }
        switch(minionData.GetTribe()) {
            case Tribe.Aquatic:
                onKilledOpponentEffects.Add((Minion minion, Minion opponent) => {
                    minion.IncrementAquaticKills();
                });
                break;
        }
        effectsData.Add(minionData.GetName(), this);
    }

    public static void Initialize() {
        if (MinionData.GetMinionData() == null) throw new System.ArgumentException("Invalid Player Roster Index When Buffing Minion", "index");
        effectsData = new Dictionary<string, EffectsData>();
        List<MinionData> minionDataList = MinionData.GetMinionData();
        foreach (MinionData minionData in minionDataList) {
            effectsData[minionData.GetName()] = new EffectsData(minionData);
        }
    }
}