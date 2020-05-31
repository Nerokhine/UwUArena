using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	private void Initialize(){
		MinionData.Initialize();
		EffectsData.Initialize();
	}

	// Use this for initialization
	void Start () {
		Initialize();
		Test.TestInkling();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
