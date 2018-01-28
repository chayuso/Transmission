using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class House : MonoBehaviour {
    public GameObject HouseOn;
    public GameObject HouseOff;
    public bool isOn = false;
    private GameState GS;
    private AudioController AC;
    //------------------------------------------------------------
    // checks whether the house is within a power radius
    //------------------------------------------------------------
    void Start()
    {
       GS = GameObject.Find("GameState").GetComponent<GameState>();
       AC = GameObject.Find("AudioController").GetComponent<AudioController>();
    }
	public static House[] LitHouses(){
		List<House> litHouses = new List<House>();
		foreach (House h in GameObject.FindObjectsOfType<House>()){
			if (h.isPowered)
				litHouses.Add(h);
		}
		return litHouses.ToArray();
	}

	public static void AssessHousingChange(House[] previousLitHouses){
		
	}
		

	Collider col;
	bool isPowered = false;

	void Awake(){
		col = GetComponent<Collider>();
	}

	//------------------------------------------------------------
	// checks whether the house is within a power radius
	//------------------------------------------------------------
	public bool IsInRangeOfSource(float range, Vector3 source){
		//if the source is below or above the bounds of this object, cast at an angle instead

		Vector3 target = transform.position;
		target.y = source.y;

		if (source.y > transform.position.y + col.bounds.extents.y){
			source.y = transform.position.y + col.bounds.extents.y;
			target.y = transform.position.y + col.bounds.extents.y/2;
		} else if (source.y < transform.position.y - col.bounds.extents.y){
			source.y = transform.position.y - col.bounds.extents.y;
			target.y = transform.position.y - col.bounds.extents.y/2;
		}

		RaycastHit[] hits = Physics.RaycastAll(source, target - source, range);
		foreach(RaycastHit hit in hits){
			if (hit.collider.gameObject == gameObject)
				return true;
		}
		return false;
	}

	public void SetPower(bool on){
		if (on ^ isPowered)
		if (on)
			OnPowerOn();
		else
			OnPowerOff();
	}

	public void OnPowerOff(){
        HouseOff.SetActive(true);
        HouseOn.SetActive(false);
        isOn = false;
        if (!GS.UnLitHouses.Contains(gameObject))
        {
            GS.UnLitHouses.Add(gameObject);
        }
        StartCoroutine(GS.ReevaluateHouse());
        //play change to dark effect
    }

	public void OnPowerOn(){
        //play change to light effect
        HouseOff.SetActive(false);
        HouseOn.SetActive(true);
        isOn = true;
        if (!GS.LitHouses.Contains(gameObject))
        {
            GS.LitHouses.Add(gameObject);
        }
        if (GS.UnLitHouses.Contains(gameObject))
        {
            GS.UnLitHouses.Remove(gameObject);
        }
        StartCoroutine(GS.ReevaluateHouse());
    }

}
