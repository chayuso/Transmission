using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    private GameObject EnemyCharacter;
    public GameObject[] Targets;
    public float journeyLength;
    public Vector3 TPosition;
    public float speed = 1f;
    public int target = 0;
    public int  signValue = 1;
    public bool disableMovement = false;

    // Use this for initialization
    void Start()
    {
        EnemyCharacter = this.gameObject;

        TPosition = Targets[0].transform.position;
        journeyLength = Vector3.Distance(EnemyCharacter.transform.position, TPosition);
        
    }


    // Update is called once per frame
    void Update()
    {
        if (!disableMovement)
        {
            AITransitions();
        }
        if (Vector3.Distance(EnemyCharacter.transform.position, TPosition)<.5f)
        {
            target += 1*signValue;
            try
            {

                TPosition = Targets[target].transform.position;
            }
            catch
            {
                signValue = signValue * -1;
                target += (1 * signValue)*2;

                TPosition = Targets[target].transform.position;
            }
        }
    }
    /*IEnumerator EnemyPathRoute()
    {
        while (true)
        {
            for (int i = 0; i < Targets.Length; i++)
            {
                TPosition = Targets[i].transform.position;
                journeyLength = Vector3.Distance(EnemyCharacter.transform.position, TPosition);
                target = i;
                print(target);

                yield return new WaitForSeconds(2f);


                if (i + 1 == Targets.Length)
                {
                    for (int j = 0; j < Targets.Length; j++)
                    {
                        TPosition = Targets[Targets.Length - j-1].transform.position;
                        target = Targets.Length - j;
                        journeyLength = Vector3.Distance(EnemyCharacter.transform.position, TPosition);
                        print(target);
                        yield return new WaitForSeconds(2f);

                    }
                }

            }
        }
       
    }*/
    void AITransitions()
    {
        float distCoveredCamera = Time.deltaTime * (speed);
        float fracJourneyCamera = distCoveredCamera / journeyLength;
        
        
        EnemyCharacter.transform.position = Vector3.Lerp(EnemyCharacter.transform.position, TPosition, fracJourneyCamera);
    }
}
