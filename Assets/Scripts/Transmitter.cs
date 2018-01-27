using System.Collections;
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

	//------------------------------------------------------------
	// begins the update cycle of the transmitters
	//------------------------------------------------------------
	static void Reassess(){
		foreach (Transmitter t in transmitters){
			t.updated = false;
			t.SetPower(false);
		}

		startTransmitter.SetPower(true);
		AssessPower(startTransmitter);

		foreach (Transmitter t in transmitters){t.updated = true;}
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
	[SerializeField] Material debugOff;
	[SerializeField] Material debugOn;

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
		transmissionRadius = col.radius;
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
		OnPlaced();
	}

	//------------------------------------------------------------
	// on place, update the power chain
	//------------------------------------------------------------
	void OnPlaced(){
		UpdateChain();
	}

	//------------------------------------------------------------
	// on destroy
	//------------------------------------------------------------
	void OnDestroy(){
		RemoveTransmitter(this);
		OnDisabled();
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
	// visual debug
	//------------------------------------------------------------
	void SetDebugAppearance(bool on){
		rend.material = on ? debugOn : debugOff;
	}

	//------------------------------------------------------------
	// to allow things to check whether this is the special one
	//------------------------------------------------------------
	public bool IsStartTransmitter(){
		return isStartTransmitter;
	}

}
