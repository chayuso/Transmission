using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitterDebug : MonoBehaviour {

	[SerializeField] GameObject transmitterPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 300)){
				Transmitter found = hit.collider.gameObject.GetComponent<Transmitter>();
				if (found != null && found.IsStartTransmitter())
					return;

				if (found != null)
					DestroyTransmitter(found);
				else
					SpawnTransmitter(hit.point);
			}
		}
	}

	void SpawnTransmitter(Vector3 location){
		GameObject.Instantiate(transmitterPrefab, location, Quaternion.AngleAxis(0, Vector3.right));
	}

	void DestroyTransmitter(Transmitter t){
		Destroy(t.gameObject);
	}
}
