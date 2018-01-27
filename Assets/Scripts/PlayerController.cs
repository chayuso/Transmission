using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject TransmitterPrefab;
    public GameObject PlayerCharacter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnObject(PlayerCharacter.transform.position, PlayerCharacter.transform.rotation, TransmitterPrefab);
        }
    }
    void SpawnObject(Vector3 Position, Quaternion Rotation, GameObject Prefab)
    {
        var transmitter = (GameObject)Instantiate(
            Prefab,
            Position,
            Rotation);
    }

}
