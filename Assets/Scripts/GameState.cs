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
        if (litHousesCount >= winCount / 2&&!finishedLevel)
        {
            AC.HousePowersingle.volume = 100;
        }
        else { AC.HousePowersingle.volume = 0; }
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
            AC.HousePowerMultiple.Play();
            AC.HousePowersingle.Stop();
            yield return new WaitForSeconds(1.5f);
            Application.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
