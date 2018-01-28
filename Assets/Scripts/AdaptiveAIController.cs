using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveAIController : MonoBehaviour {
	[SerializeField] Transform footPoint;
	[SerializeField] Transform[] checkpoints;
	[SerializeField] int startCheckpoint = 0;
	[SerializeField] bool circular = false;
	[SerializeField] float maxSpeed = 1f;
	[SerializeField] float acceleration = 0.2f;

	int nextCheckpointIndex = 0;
	bool reverse = false;
	float currentSpeed = 0f;

	enum Behavior{
		Patrol, Wait, Attack
	}

	void Awake(){
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ProgressBehavior(){

	}

	//------------------------------------------------------------
	// moves the player at its speed in the direction of the next checkpoint
	//------------------------------------------------------------
	void Move(){
		Vector3 offsetToDestination = checkpoints[nextCheckpointIndex].position - footPoint.position;
		transform.position += offsetToDestination.normalized * currentSpeed;
	}

	//------------------------------------------------------------
	// changes the speed of the character based on how close it is to the checkpoint
	//------------------------------------------------------------
	void AdjustSpeed(){
		float distanceToDestination = Vector3.Distance(footPoint.position, checkpoints[nextCheckpointIndex].position);
		if (DistanceToStop() > distanceToDestination)
			currentSpeed -= Time.deltaTime * acceleration;
		else if (currentSpeed < maxSpeed)
			currentSpeed += Time.deltaTime * acceleration;
	}

	//------------------------------------------------------------
	// calculates the distance to stop
	//------------------------------------------------------------
	float DistanceToStop(){
		float timeToStop = currentSpeed / acceleration;
		return (currentSpeed / 2) * timeToStop;
	}
}
