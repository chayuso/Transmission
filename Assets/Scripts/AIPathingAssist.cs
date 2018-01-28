using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AdaptiveAIController : MonoBehaviour {
	[SerializeField] float pathSpherecastSize = 0.8f;
	[SerializeField] LayerMask detectedObstructions;

	bool IsPathBlocked(){
		RaycastHit hit;
		if (Physics.SphereCast(transform.position,
			   pathSpherecastSize,
			   checkpoints[nextCheckpointIndex].position - transform.position,
			   out hit,
			   DistanceToCheckpoint(),
			   detectedObstructions.value)){
			return true;
		}
		return false;
	}

}
