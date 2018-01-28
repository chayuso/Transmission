using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransmitterInventory : MonoBehaviour {

	[SerializeField] KeyCode activateButton = KeyCode.X;
	[SerializeField] GameObject transmitterPrefab;
	[SerializeField] Transform transmitterDropPoint;
	[SerializeField] Transform pickupCenter;
	[SerializeField] int transmitterCount = 4;
	[SerializeField] float pickupRadius = 1.5f;

	float distance;
	Transmitter nearest = null;
    private GameState GS;
    private AudioController AC;
    // Update is called once per frame
    void Start()
    {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        AC = GameObject.Find("AudioController").GetComponent<AudioController>();
    }
	void Update () {
		TransmitterHighlight();
		UserControl();
	}

	//------------------------------------------------------------
	// updates the status of the nearest transmitter
	//------------------------------------------------------------
	void TransmitterHighlight(){
		float newDistance;
		Transmitter newNearest = FindNearestTransmitter(out newDistance);

		if (newNearest != null && newDistance <= pickupRadius){
			//if(newNearest != nearest)
			//use this to determine whether to create, destroy or move any selection effect
		}

		nearest = newNearest;
		distance = newDistance;
	}
		
	//------------------------------------------------------------
	// user interaction for this mechanic
	//------------------------------------------------------------
	void UserControl(){
		if (Input.GetKeyDown(activateButton)){
			TransmitterInteract();
		}
	}

	//------------------------------------------------------------
	// determines whether the player is able to pick up a transmitter
	// if not, the player will try to place one instead
	//------------------------------------------------------------
	void TransmitterInteract(){
		if (nearest != null && distance <= pickupRadius)
			PickupTransmitter(nearest);
		//else if (transmitterCount > 0)
		//	PlaceTransmitter();
	}

	//------------------------------------------------------------
	// finds the nearest transmitter THAT IS NOT THE START ONE
	//------------------------------------------------------------
	Transmitter FindNearestTransmitter(out float distance){
		Transmitter[] emitters = GameObject.FindObjectsOfType<Transmitter>();

		Transmitter nearest = null;
		distance = Mathf.Infinity;

		foreach (Transmitter t in emitters){
			if (t.IsStartTransmitter())
				continue;

			float currentDistance = Vector3.Distance(t.transform.position, pickupCenter.position);

			if (nearest == null){
				nearest = t;
				distance = currentDistance;
			} else if(currentDistance < distance){
				nearest = t;
				distance = currentDistance;
			}
		}

		return nearest;
	}

	//------------------------------------------------------------
	// destroys a selected transmitter and increments the player inventory count
	//------------------------------------------------------------
	void PickupTransmitter(Transmitter t){
		Destroy(t.gameObject);
		++transmitterCount;
        GS.transmittersDeployed--;
        AC.RetrieveTransmission.Play();
	}

	//------------------------------------------------------------
	// creates a transmitter and decrements the player inventory
	//------------------------------------------------------------
	void PlaceTransmitter(){
		--transmitterCount;
		GameObject.Instantiate(transmitterPrefab, transmitterDropPoint.position, transform.rotation);
	}


}
