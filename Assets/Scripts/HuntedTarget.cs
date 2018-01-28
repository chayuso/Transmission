using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HuntedTarget : MonoBehaviour {

	[SerializeField] UnityEvent onHit;
	[SerializeField] int priority = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetHit(){
		onHit.Invoke();
	}
}
