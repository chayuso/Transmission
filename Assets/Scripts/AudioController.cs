using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public AudioSource Ambient;
    public AudioSource BuildingPowerOff;
    public AudioSource DeployTransmission;
    public AudioSource HousePowerMultiple;
    public AudioSource HousePowersingle;
    public AudioSource RetrieveTransmission;
    public AudioSource Switch;
    // Use this for initialization
    void Start () {
        Ambient.Play();
        HousePowersingle.Play();
        HousePowersingle.volume=0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
