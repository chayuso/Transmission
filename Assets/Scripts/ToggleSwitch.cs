using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour {
    public GameObject Button;
    public Material InactiveMaterial;
    public Material ActiveMaterial;
    public GameObject Wall;
    Vector3 initButtonPos;
    bool isStepped = false;
    public bool manualEnable = false;
	// Use this for initialization
	void Start () {
        initButtonPos = Button.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (manualEnable)
        {
            Wall.SetActive(manualEnable);
            if (manualEnable)
            {
                Button.transform.localPosition = new Vector3(initButtonPos.x, .05f, initButtonPos.z);
                Button.GetComponent<Renderer>().material = ActiveMaterial;

            }
        }
        else
        {
            Wall.SetActive(isStepped);
            if (isStepped)
            {
                Button.transform.localPosition = new Vector3(initButtonPos.x, .05f, initButtonPos.z);
                Button.GetComponent<Renderer>().material = ActiveMaterial;

            }
            else
            {
                Button.transform.localPosition = initButtonPos;
                Button.GetComponent<Renderer>().material = InactiveMaterial;
                Wall.GetComponent<WallScript>().ReEnableEnemies();
            }
        }

	}
    void OnTriggerEnter(Collider col)
    {
        if (col.name == "ThirdPersonController")
        {
            isStepped = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.name == "ThirdPersonController")
        {
            isStepped = false;
        }
    }
}
