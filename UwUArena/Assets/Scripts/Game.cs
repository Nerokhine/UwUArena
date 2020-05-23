using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game: MonoBehaviour {
    private static String MINIONS_CSV_FILE = "./UwUArenaMinions.csv";
    //private Player[] player;


    // Use this for initialization
	void Start () {
		InitializeCards();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*public Game() {
        InitializeCards();
    }*/

    public void ParseCsv (String file) {
        String fileData = System.IO.File.ReadAllText(file);
        String[] lines = fileData.Split("\n"[0]);
        bool initializedRowNames = false;
        foreach (String line in lines) {
            String[] lineData = (line.Trim()).Split(","[0]);
            if (!initializedRowNames) {
                initializedRowNames = true;
            }
            foreach (String record in lineData) {
                Debug.Log(record);
            }
        }
    }

    public void InitializeCards() {
        ParseCsv(MINIONS_CSV_FILE);
    }
}