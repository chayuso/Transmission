using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

	[SerializeField] float sightRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Transmitter Sweep(){
		foreach(Transmitter t in GameObject.FindObjectsOfType<Transmitter>()){
			if (Vector3.Distance(t.transform.position, transform.position) > sightRange)
				continue;
			RaycastHit hit;
			if (Physics.Raycast(t.transform.position, transform.position - t.transform.position, out hit, sightRange)){
				if (hit.collider.gameObject == gameObject){
					return t;
				}
			}
		}
		return null;
	}
}
