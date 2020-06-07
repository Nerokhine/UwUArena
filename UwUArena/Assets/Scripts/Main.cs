using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public Animations animations;

	private void Initialize(){
		MinionData.Initialize();
		EffectsData.Initialize();
	}

	// Use this for initialization
	void Start () {
		Initialize();
		Battle battle = Test.TestWallOfFlame();
		StartCoroutine(animations.AnimateBattle(battle.GetBattleRecord()));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
