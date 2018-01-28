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
    public AudioSource BGM1;
    public AudioSource BGM2;
    public AudioSource BGM3;
    public AudioSource BGM4;
    public AudioSource BGM5;
    // Use this for initialization
    void Start () {
        Ambient.Play();
        BGM1.Play();
        BGM2.Play();
        BGM3.Play();
        BGM4.Play();
        BGM5.Play();

        BGM1.volume=0;
        BGM2.volume = 0;
        BGM3.volume = 0;
        BGM4.volume = 0;
        BGM5.volume = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
