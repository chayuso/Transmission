using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlighWithParent : MonoBehaviour {
    // Use this for initialization
    public float offsetY = 0;
    void Start()
    {
        transform.position = transform.parent.position;
        transform.Translate(Vector3.up * offsetY, Space.World);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
