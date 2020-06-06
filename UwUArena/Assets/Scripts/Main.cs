using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public Battle battle;
	public Test test;

	private void Initialize(){
		MinionData.Initialize();
		EffectsData.Initialize();
	}

	// Use this for initialization
	void Start () {
		Initialize();
		battle = test.TestWallOfFlame();
		StartCoroutine(battle.AnimateBattle(this));
		//Test.TestInkling();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
