using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HuntedTarget : MonoBehaviour {

	[SerializeField] UnityEvent onHit;
	[SerializeField] HuntVerificationEvent verifyValidity;
	[SerializeField] int priority = 0;

	bool isValid = true;

	public void SetValidTarget(bool valid){
		isValid = valid;
	}

	public bool IsValidTarget(){
		verifyValidity.Invoke(this);
		return isValid;
	}

	public void Hit(){
		onHit.Invoke();
	}

	public int GetPriority(){
		return priority;
	}
}

[System.Serializable]
public class HuntVerificationEvent : UnityEvent<HuntedTarget>{}
