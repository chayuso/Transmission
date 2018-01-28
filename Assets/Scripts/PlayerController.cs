using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject TransmitterPrefab;
    public GameObject PlayerCharacter;
    private GameState GS;
    private AudioController AC;
	// Use this for initialization
	void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        AC = GameObject.Find("AudioController").GetComponent<AudioController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V)&&GS.transmittersDeployed<GS.transmitterLimit)
        {
            SpawnObject(PlayerCharacter.transform.position, PlayerCharacter.transform.rotation, TransmitterPrefab);
            AC.DeployTransmission.Play();
            GS.transmittersDeployed++;
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
