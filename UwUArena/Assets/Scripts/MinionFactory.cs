using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MinionFactory {
    // Will probably be replaced by a sql database + query
    private static Dictionary<int,List<MinionData>> minionsByLevel;

    public static void Initialize() {
        minionsByLevel = new Dictionary<int,List<MinionData>>();
        foreach (MinionData minionData in MinionData.GetMinionData()) {
            minionsByLevel[minionData.GetLevel()].Add(minionData);
        }
    }
    public static Minion GenerateMinion(int level) {
        int index = new System.Random().Next(0, minionsByLevel[level].Count - 1);
        return new Minion(minionsByLevel[level][index].GetName());
    }
}