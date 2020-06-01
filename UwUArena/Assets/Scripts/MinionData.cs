using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionData {
    private static Dictionary<string, MinionData> minionData = new Dictionary<string, MinionData>();
    private static List<MinionData> minionDataList = new List<MinionData>();
    private static string MINIONS_CSV_FILE = "./UwUArenaMinions.csv";
    private int health;
    private int attack;
    private int level;
    private string name;
    private string effectText;
    private Tribe tribe;
    private Effects effects;

    public int GetHealth() {
        return health;
    }

    public int GetAttack() {
        return attack;
    }

    public int GetLevel() {
        return level;
    }

    public string GetName() {
        return name;
    }

    public string GetEffectText() {
        return effectText;
    }

    public Tribe GetTribe() {
        return tribe;
    }

    private MinionData(int level, string name, Tribe tribe, int attack, int health, string effectText) {
        this.level = level;
        this.name = name;
        this.tribe = tribe;
        this.attack = attack;
        this.health = health;
        this.effectText = effectText;
        Debug.Log(effectText);
        minionData.Add(name, this);
        minionDataList.Add(this);
    }

    private static Tribe GetTribe(string tribe) {
        switch (tribe) {
            case "Aquatic":
                return Tribe.Aquatic;
            case "Pyro":
                return Tribe.Pyro;
            case "Forest":
                return Tribe.Forest;
            case "Thunder":
                return Tribe.Thunder;
            case "Dragon":
                return Tribe.Dragon;
        }
        throw new System.ArgumentException("Invalid Tribe Input", "tribe");
    }

    private static void ParseCsv (string file) {
        String fileData = System.IO.File.ReadAllText(file);
        String[] lines = fileData.Split("\n"[0]);
        bool initializedRowNames = false;
        foreach (string line in lines) {
            string[] lineData = (line.Trim()).Split(","[0]);
            if (!initializedRowNames) {
                initializedRowNames = true;
            } else {
                string effectText = "";
                for (int i = 5; i < lineData.Length; i ++) {
                    effectText += lineData[i];
                }
                new MinionData(
                    level:Int32.Parse(lineData[0]),
                    name:lineData[1],
                    tribe:GetTribe(lineData[2]),
                    attack:Int32.Parse(lineData[3]),
                    health:Int32.Parse(lineData[4]),
                    effectText:effectText
                );
            }
        }
    }

    public static MinionData GetMinionData(string name) {
        return minionData[name];
    }

    public static List<MinionData> GetMinionData() {
        return minionDataList;
    }

    public static void Initialize() {
        ParseCsv(MINIONS_CSV_FILE);
    }
}