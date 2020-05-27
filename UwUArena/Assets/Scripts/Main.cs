using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MinionData.Initialize();
		Battle battle = new Battle();
		battle.TestBattle();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
