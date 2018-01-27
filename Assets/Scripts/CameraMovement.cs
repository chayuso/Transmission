using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public GameObject playerCamera;
    public GameObject playerCharacter;
    public float journeyLength;
    public Vector3 TPosition = new Vector3(0, 0, 0);
    public float speed = 25f;
    // Use this for initialization
    void Start () {
        journeyLength = Vector3.Distance(TPosition,playerCharacter.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        CameraTransitions();

    }
    void CameraTransitions()
    {
        float distCoveredCamera = Time.deltaTime * (speed * 0.1f);
        float fracJourneyCamera = distCoveredCamera / journeyLength;
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, playerCharacter.transform.position, fracJourneyCamera);
    }
}
