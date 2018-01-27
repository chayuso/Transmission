using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
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
	SphereCollider col;
	MeshRenderer rend;

	bool updated = false;
	bool powered = false;
	bool broken = false;

	//------------------------------------------------------------
	// on start
	//------------------------------------------------------------
	void Start () {
		col = GetComponent<SphereCollider>();
		rend = GetComponent<MeshRenderer>();

		if (isStartTransmitter)
			SetStartTransmitter(this);

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
		Collider[] nearby = Physics.OverlapSphere(transform.position, col.radius);
		foreach (Collider c in nearby){
			Transmitter t = c.gameObject.GetComponent<Transmitter>();
			if (t == null)
				continue;
			neighbors.Add(t);
		}
		return neighbors.ToArray();
	}

	//------------------------------------------------------------
	// visual debug
	//------------------------------------------------------------
	void SetDebugAppearance(bool on){
		rend.material = on ? debugOn : debugOff;
	}

}
