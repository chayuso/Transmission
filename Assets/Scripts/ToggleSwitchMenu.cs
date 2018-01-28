using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class ToggleSwitchMenu : MonoBehaviour {
    public GameObject Button;
    public Material InactiveMaterial;
    public Material ActiveMaterial;
    Vector3 initButtonPos;
    bool isStepped = false;
    public bool manualEnable = false;
    public List<GameObject> ObjectsOnTop;
    // Use this for initialization
    void Start () {
        initButtonPos = Button.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject go in ObjectsOnTop)
        {
            if (go == null)
            {
                ObjectsOnTop.Remove(go);
            }
        }
        if (ObjectsOnTop.Count == 0)
        {
            isStepped = false;
        }
		if (manualEnable)
		{

			if (manualEnable)
			{
				Button.transform.localPosition = new Vector3(initButtonPos.x, .05f, initButtonPos.z);
				Button.GetComponent<Renderer>().material = ActiveMaterial;
				SceneManager.LoadScene("ConTest");

			} 
		}
		else
		{

			if (isStepped)
			{
				Button.transform.localPosition = new Vector3(initButtonPos.x, .05f, initButtonPos.z);
				Button.GetComponent<Renderer>().material = ActiveMaterial;
				if (ObjectsOnTop.Count >= 2){
					SceneManager.LoadScene ("BaseScene");
				}
			}
			else
			{
				Button.transform.localPosition = initButtonPos;
				Button.GetComponent<Renderer>().material = InactiveMaterial;
			}

		}

	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag=="Player" || col.name.Split(' ')[0].Trim() == "Transmitter" || col.name.Split(' ')[0].Trim() == "AIThirdPersonController")
        {
            isStepped = true;
            ObjectsOnTop.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" || col.name.Split(' ')[0].Trim()=="Transmitter"|| col.name.Split(' ')[0].Trim() == "AIThirdPersonController")
        {
            ObjectsOnTop.Remove(col.gameObject);
        }

    }
}
