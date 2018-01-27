using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {
    public List<GameObject> DisabledEnemies;
	// Use this for initialization
	void Start () {
        print("testc");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.name == "AIThirdPersonController")
        {
            col.gameObject.GetComponent<AIController>().disableMovement = true;
            DisabledEnemies.Add(col.gameObject);
        }
    }
    public void ReEnableEnemies()
    {
        for (int i = 0; i<DisabledEnemies.Count;i++)
        {
            DisabledEnemies[i].GetComponent<AIController>().disableMovement = false;
        }
        DisabledEnemies.Clear();
    }

}
