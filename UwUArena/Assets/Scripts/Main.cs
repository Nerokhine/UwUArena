using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public Battle battle;

	public GameObject card;

	private void Initialize(){
		MinionData.Initialize();
		EffectsData.Initialize();
	}

	// Use this for initialization
	void Start () {
		Initialize();
		battle = Test.TestWallOfFlame();
		StartCoroutine(battle.AnimateBattle());
		//Test.TestInkling();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
