using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    private GameState GS;
    public GameObject FloorOff;
    public GameObject FloorOn;
	// Use this for initialization
	void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();

    }
	
	// Update is called once per frame
	void Update () {
        if (GS.finishedLevel)
        {
            FloorOff.SetActive(false);
            FloorOn.SetActive(true);
        }
	}

}
