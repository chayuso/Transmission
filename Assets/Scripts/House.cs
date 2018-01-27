using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class House : MonoBehaviour {

	Collider col;

	void Awake(){
		col = GetComponent<Collider>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//------------------------------------------------------------
	// checks whether the house is within a power radius
	//------------------------------------------------------------
	public bool IsInRangeOfSource(float range, Vector3 source){
		//if the source is below or above the bounds of this object, cast at an angle instead

		Vector3 target = transform.position;
		target.y = source.y;

		if (source.y > transform.position.y + col.bounds.extents.y){
			source.y = transform.position.y + col.bounds.extents.y;
			target.y = transform.position.y + col.bounds.extents.y/2;
		} else if (source.y < transform.position.y - col.bounds.extents.y){
			source.y = transform.position.y - col.bounds.extents.y;
			target.y = transform.position.y - col.bounds.extents.y/2;
		}

		RaycastHit[] hits = Physics.RaycastAll(source, target - source, range);
		foreach(RaycastHit hit in hits){
			if (hit.collider.gameObject == gameObject)
				return true;
		}
		return false;
	}

}
