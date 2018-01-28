using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameState : MonoBehaviour {
    public int litHousesCount = 0;
    public List<GameObject> LitHouses = new List<GameObject>();
    public List<GameObject> UnLitHouses = new List<GameObject>();
    public int winCount = 11;
    public int transmitterLimit = 10;
    public int transmittersDeployed = 0;
    private AudioController AC;
    public bool finishedLevel = false;
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
        if (litHousesCount >= winCount / 5&&!finishedLevel)
        {
            AC.BGM1.volume = 100;
        }
        else { AC.BGM1.volume = 0; }
        if (litHousesCount >= (winCount / 5)*2 && !finishedLevel)
        {
            AC.BGM2.volume = 100;
        }
        else { AC.BGM2.volume = 0; }
        if (litHousesCount >= (winCount / 5) * 3 && !finishedLevel)
        {
            AC.BGM3.volume = 100;
        }
        else { AC.BGM3.volume = 0; }
        if (litHousesCount >= (winCount / 5) * 4 && !finishedLevel)
        {
            AC.BGM4.volume = 100;
        }
        else { AC.BGM4.volume = 0; }
        if (litHousesCount >= (winCount / 5) * 5 && !finishedLevel)
        {
            AC.BGM5.volume = 100;
        }
        else { AC.BGM5.volume = 0; }
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
        if (!finishedLevel)
        {
            finishedLevel = true;
            yield return new WaitForSeconds(1.5f);
            Application.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
