﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour {

	//=========================================================================
	//=========================================================================
	// Static variables and methods
	//=========================================================================
	//=========================================================================

	static Transmitter startTransmitter = null;
	static List<Transmitter> transmitters = new List<Transmitter>();

	static void SetStartTransmitter(Transmitter t){startTransmitter = t;}
	static Transmitter GetStartTransmitter(){return startTransmitter;}

	static void AddTransmitter(Transmitter t){transmitters.Add(t);}
	static void RemoveTransmitter(Transmitter t){transmitters.Remove(t);}
    private AudioController AC;
    //------------------------------------------------------------
    // begins the update cycle of the transmitters
    //------------------------------------------------------------
    static void Reassess(){
		House[] litHousesBefore = House.LitHouses();	//log the houses that are lit beforehand

		foreach (Transmitter t in transmitters){
			t.updated = false;
			t.SetPower(false);
		}

		startTransmitter.SetPower(true);
		AssessPower(startTransmitter);

		foreach (Transmitter t in transmitters){t.updated = true;}

		House.AssessHousingChange(litHousesBefore);	//produce any effects related to mass change in house power

	}

	//------------------------------------------------------------
	// checks whether the current transmitter is powered,
	// then reflects that in its neighbors recursively
	//------------------------------------------------------------
	static void AssessPower(Transmitter t){
		t.updated = true;
		if (t.broken)
			t.SetPower(false);
		
		if (!t.powered)
			return;
		
		foreach(Transmitter next in t.FindNeighbors()){
			if (next.updated == true)
				continue;
			next.SetPower(true);
			AssessPower(next);
		}
        if (t.powered)
        {
            t.FindNearbyHouses();
        }
    }



	//=========================================================================
	//=========================================================================
	// Instance variables and methods
	//=========================================================================
	//=========================================================================

	//------------------------------------------------------------
	// editor variables
	//------------------------------------------------------------
	[SerializeField] bool isStartTransmitter = false;
	[SerializeField] bool useMaterialsOnly = false;
	[SerializeField] Material debugOff;
	[SerializeField] Material debugOn;
	[SerializeField] Material debugBroken;
	public List<House> houses = new List<House>();
	public GameObject TransistorOn;
	public GameObject TransistorOff;
	public GameObject TransistorBroken;

	//------------------------------------------------------------
	// local variables
	//------------------------------------------------------------
	MeshRenderer rend;
	float transmissionRadius;

	bool updated = false;
	bool powered = false;
	bool broken = false;

	//------------------------------------------------------------
	// on awake
	//------------------------------------------------------------
	void Awake(){
		SphereCollider col = GetComponent<SphereCollider>();

		float greatestDimension = Mathf.Max(transform.localScale.x, transform.localScale.z);

		transmissionRadius = col.radius * greatestDimension;
		Destroy(col);

		rend = GetComponent<MeshRenderer>();

		AddTransmitter(this);
		if (isStartTransmitter)
			SetStartTransmitter(this);
	}

	//------------------------------------------------------------
	// on start
	//------------------------------------------------------------
	void Start () {
        AC = GameObject.Find("AudioController").GetComponent<AudioController>();
        OnPlaced();
	}
	//------------------------------------------------------------
	// on place, update the power chain
	//------------------------------------------------------------
	void OnPlaced(){
		UpdateChain();


    }

	//------------------------------------------------------------
	// on moved, update the power chain
	//------------------------------------------------------------
	public void OnMoved(){
		UpdateChain();
	}

	//------------------------------------------------------------
	// on destroy
	//------------------------------------------------------------
	void OnDestroy(){
		RemoveTransmitter(this);
		OnDisabled();
        foreach (House h in GameObject.FindObjectsOfType<House>())
        {
            h.OnPowerOff();
        }
        OnPlaced();
    }

	//------------------------------------------------------------
	// breaks the transmitter
	//------------------------------------------------------------
	public void Break(){
		OnDisabled();
        foreach (House h in GameObject.FindObjectsOfType<House>())
        {
            h.OnPowerOff();
        }
        OnPlaced();
    }
    public bool IsBroken()
    {
        return broken;
    }
	//------------------------------------------------------------
	// on remove, also update the power chain
	//------------------------------------------------------------
	void OnDisabled(){
		broken = true;
		UpdateChain();
	}

	//------------------------------------------------------------
	// sets the power state of this transmitter
	//------------------------------------------------------------
	void SetPower(bool on){
		powered = on;
		OnPowerChanged(on);
	}

	//------------------------------------------------------------
	// the events that occur when the power is changed
	//------------------------------------------------------------
	void OnPowerChanged(bool newPower){
		SetDebugAppearance(newPower);
	}

	//------------------------------------------------------------
	// requests a reassessment of the transmitter chain
	//------------------------------------------------------------
	void UpdateChain(){
		Reassess();
	}

	//------------------------------------------------------------
	// returns a list of all the nearby transmitters
	//------------------------------------------------------------
	Transmitter[] FindNeighbors(){
		List<Transmitter> neighbors = new List<Transmitter>();

		foreach (Transmitter c in transmitters){
            if (Vector3.Distance(c.transform.position, transform.position) <= transmissionRadius)
				neighbors.Add(c);
		}
	
		Debug.Log(neighbors.Count);
		return neighbors.ToArray();
	}
		
	//------------------------------------------------------------
	// returns a list of all nearby houses
	//------------------------------------------------------------
	House[] FindNearbyHouses(){
		houses = new List<House>();
		foreach(House h in GameObject.FindObjectsOfType<House>()){
            if (h.IsInRangeOfSource(transmissionRadius, transform.position))
            {
                houses.Add(h);
                h.OnPowerOn();
            }

		}
		return houses.ToArray();
	}

	//------------------------------------------------------------
	// visual debug
	//------------------------------------------------------------
	void SetDebugAppearance(bool on){
		if (!useMaterialsOnly){
			if (broken){
				TransistorBroken.SetActive(true);
				TransistorOn.SetActive(false);
				TransistorOff.SetActive(false);
                foreach (Transform tr in gameObject.transform)
                {
                    if (tr.name.Split(' ')[0].Trim() == "Cylinder"|| tr.name.Split(' ')[0].Trim() == "Outline")
                    {
                        tr.gameObject.SetActive(false);
                    }
                }
			} else if (powered){
				TransistorBroken.SetActive(false);
				TransistorOn.SetActive(true);
				TransistorOff.SetActive(false);
                foreach (Transform tr in gameObject.transform)
                {
                    if (tr.name.Split(' ')[0].Trim() == "Cylinder" || tr.name.Split(' ')[0].Trim() == "Outline")
                    {
                        tr.gameObject.SetActive(true);
                    }
                }
            } else{
				TransistorBroken.SetActive(false);
				TransistorOn.SetActive(false);
				TransistorOff.SetActive(true);
                foreach (Transform tr in gameObject.transform)
                {
                    if (tr.name.Split(' ')[0].Trim() == "Cylinder" || tr.name.Split(' ')[0].Trim() == "Outline")
                    {
                        tr.gameObject.SetActive(false);
                    }
                }
            }
		} else {
        	rend.material = on ? debugOn : debugOff;
			rend.material = broken ? debugBroken : rend.material;
		}
	}

	//------------------------------------------------------------
	// to allow things to check whether this is the special one
	//------------------------------------------------------------
	public bool IsStartTransmitter(){
		return isStartTransmitter;
	}

	//------------------------------------------------------------
	// responds to the hunted script as to whether this has already been broken
	//------------------------------------------------------------
	public void IsHuntable(HuntedTarget ht){
		//print("Broken: " + broken + " / Start: " + isStartTransmitter);
		ht.SetValidTarget(!broken && !isStartTransmitter);
	}
}
