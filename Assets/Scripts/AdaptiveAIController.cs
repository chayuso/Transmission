﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySight))]
public partial class AdaptiveAIController : MonoBehaviour {
	[SerializeField] Transform footPoint;
	[SerializeField] Transform[] checkpoints;
	[SerializeField] int startCheckpoint = 0;
	[SerializeField] bool circular = false;
	[SerializeField] float maxSpeed = 1f;
	[SerializeField] float acceleration = 0.2f;
	[SerializeField] float checkpointTolerance = 0.1f;
	[SerializeField] float checkpointWaitTime = 0.5f;
	[SerializeField] float waitAfterKillTime = 0.5f;
	[SerializeField] float turnRate = 120f;
	[SerializeField] float fireArc = 10f;

	EnemySight eyes;
	Behavior currentBehavior;
	int nextCheckpointIndex = 0;
	bool reverse = false;
	float currentSpeed = 0f;
	float waitTimer = 0f;
	float killConfirmTimer = 0f;
	HuntedTarget target = null;
    public bool disableMovement = false;
	enum Behavior{
		Patrol, Wait, Attack
	}

	void Awake(){
		eyes = GetComponent<EnemySight>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!disableMovement)
        {
            DetermineBehavior();
            ExecuteBehavior();
        }

	}

	//------------------------------------------------------------
	// checks a bunch of things to determine the character's behavior
	//------------------------------------------------------------
	void DetermineBehavior(){
		if (AssessHostile()){
			currentBehavior = Behavior.Attack;
			return;
		}
		if (CheckpointReached()){
			currentBehavior = Behavior.Wait;
			return;
		}
		currentBehavior = Behavior.Patrol;
	}

	//------------------------------------------------------------
	// determines if there is a transmitter to attack
	//------------------------------------------------------------
	bool AssessHostile(){
		if (killConfirmTimer > 0f)
			return true;
		target = eyes.Scan();
		return (target != null && target.IsValidTarget());
	}

	//------------------------------------------------------------
	// determines if the next checkpoint has been reached
	//------------------------------------------------------------
	bool CheckpointReached(){
		if (DistanceToCheckpoint() <= checkpointTolerance){
			waitTimer = checkpointWaitTime;
			nextCheckpointIndex += reverse ? -1 : 1;
			if (nextCheckpointIndex >= checkpoints.Length){
				if (circular)
					nextCheckpointIndex = 0;
				else{
					nextCheckpointIndex -= 2;
					reverse = !reverse;
				}
			} else if (nextCheckpointIndex < 0){
				nextCheckpointIndex += 2;
				reverse = !reverse;
			}
		}
		return waitTimer > 0f;
	}

	//------------------------------------------------------------
	// causes the character to execute the actions associated with its behavior
	//------------------------------------------------------------
	void ExecuteBehavior(){
		//print(currentBehavior);
		if (currentBehavior == Behavior.Patrol)
			Move();
		else if (currentBehavior == Behavior.Wait)
			Wait();
		else if (currentBehavior == Behavior.Attack)
			Attack();
	}

	//------------------------------------------------------------
	// returns the distance to the next checkpoint
	//------------------------------------------------------------
	float DistanceToCheckpoint(){
		return Vector3.Distance(footPoint.transform.position, checkpoints[nextCheckpointIndex].position);
	}

	//------------------------------------------------------------
	// moves the player at its speed in the direction of the next checkpoint
	// and continues to rotate it to face that point
	//------------------------------------------------------------
	void Move(){
		AdjustSpeed();
		LookTowardsTarget(checkpoints[nextCheckpointIndex].position);
		Vector3 offsetToDestination = checkpoints[nextCheckpointIndex].position - footPoint.position;
		transform.position += offsetToDestination.normalized * currentSpeed * Time.deltaTime;
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

	//------------------------------------------------------------
	// while waiting, the character rotates to face the next checkpoint
	//------------------------------------------------------------
	void Wait(){
		waitTimer -= Time.deltaTime;
		LookTowardsTarget(checkpoints[nextCheckpointIndex].position);
	}

	//------------------------------------------------------------
	// the rotation that the character should have on the way to the next checkpoint
	//------------------------------------------------------------
	void LookTowardsTarget(Vector3 targetPoint){
		Quaternion targetLook = Quaternion.LookRotation(Vector3.ProjectOnPlane(targetPoint - transform.position, Vector3.up), Vector3.up);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetLook, turnRate * Time.deltaTime);
	}

	//------------------------------------------------------------
	// aims at and attacks the target if possible
	//------------------------------------------------------------
	void Attack(){
		if (killConfirmTimer > 0){
			killConfirmTimer -= Time.deltaTime;
			return;
		}
		LookTowardsTarget(target.transform.position);
		Vector3 flatOffsetVector = Vector3.ProjectOnPlane(target.transform.position - transform.position, Vector3.up);
		if (Vector3.Angle(transform.forward, flatOffsetVector) <= fireArc){
			Fire();
		}
	}

	//------------------------------------------------------------
	// attacks the target
	//------------------------------------------------------------
	void Fire(){
		//do some fire animation
		target.Hit();
		killConfirmTimer = waitAfterKillTime;
	}

}
