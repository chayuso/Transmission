using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReadout : MonoBehaviour {

	[SerializeField] Text transmitterText;
	[SerializeField] Text houseText;

	GameState gameStateObj;

	// Use this for initialization
	void Start () {
		gameStateObj = FindObjectOfType<GameState>();
	}
	
	// Update is called once per frame
	void Update () {
		transmitterText.text = "Transmitters Remaining\n" + (gameStateObj.transmitterLimit - gameStateObj.transmittersDeployed);
		houseText.text = "Nodes Powered\n" + gameStateObj.litHousesCount + "/" + gameStateObj.winCount;
	}
}
