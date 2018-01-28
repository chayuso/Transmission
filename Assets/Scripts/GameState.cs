using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameState : MonoBehaviour {
    public int litHousesCount = 0;
    public List<GameObject> LitHouses = new List<GameObject>();
    public List<GameObject> UnLitHouses = new List<GameObject>();
    public int winCount = 11;
    private AudioController AC;
    // Use this for initialization
    void Start () {
        AC = GameObject.Find("AudioController").GetComponent<AudioController>();
    }
	
	// Update is called once per frame
	void Update () {
        litHousesCount = LitHouses.Count;
        if (litHousesCount >= winCount)
        {
            StartCoroutine(NextSceneWin());
            return;
        }
	}
    public IEnumerator ReevaluateHouse()
    {
        yield return new WaitForSeconds(.1f);
        bool removed = false;
        foreach (GameObject go in UnLitHouses)
        {
            if (LitHouses.Contains(go))
            {
                LitHouses.Remove(go);
                removed = true;
            }
        }
        if (removed)
        {
            AC.BuildingPowerOff.Play();
        }
    }
    public IEnumerator NextSceneWin()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
