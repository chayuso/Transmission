using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignTile : MonoBehaviour {
    public bool NonTile = false;
    public int tileX;
    public int tileY;
    public int tileZ;
    // Use this for initialization
    void Awake()
    {
        /*try
        {
            
            tileX = System.Int32.Parse(transform.name.Split('x')[0]);
            tileY = System.Int32.Parse(transform.name.Split('x')[1]);
            tileZ = System.Int32.Parse(transform.name.Split('x')[2]);
            transform.position = new Vector3(tileX + (0.58f * (tileX - 1)), tileZ + (0.5f * (tileZ - 1)), tileY + (0.58f * (tileY - 1)));
            if (NonTile)
            {
                GetComponent<MeshRenderer>().enabled = false;
                transform.name = "N" + tileX.ToString() + "x" + tileY.ToString() + "x" + tileZ.ToString();
            }
        }
        catch
        {*/
            if (!transform.name.StartsWith("I"))
            {
                tileX = findPosition(transform.position.x, 0, "x");
                tileY = findPosition(transform.position.z, 0, "y");
                tileZ = findPosition(transform.position.y, 0, "z");
                transform.position = new Vector3(tileX + (.1f * (tileX - 1)), tileZ + (.1f * (tileZ - 1)), tileY + (.1f * (tileY - 1)));
                if (NonTile)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                    transform.name = "N" + tileX.ToString() + "x" + tileY.ToString() + "x" + tileZ.ToString();
                }
                else
                {
                    transform.name = tileX.ToString() + "x" + tileY.ToString() + "x" + tileZ.ToString();
                }
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
       // }
    }

    // Update is called once per frame
    void Update()
    {

    }
    int findPosition(float position,float offset,string string_v)
    {
        int i = 0;
        float last_value = 0;
        float found_value;
        int return_tile_position;
        int sign_value;
        if (position <= 0)
        {
            sign_value = -1;
        }
        else
        {
            sign_value = 1;
        }
        //Find x
        while (true)
        {
            if (Mathf.Abs(position) <= Mathf.Abs(i +(offset * (i - 1))))
            {
                found_value = i + (offset * (i - 1));
                break;
            }
            last_value = i + (offset * (i - 1));
            i += sign_value;
        }
        if (last_value == 0)
        {
            return_tile_position = i;
        }
        else
        {
            float first_compare = Mathf.Abs(position) - Mathf.Abs(last_value);
            float second_compare = Mathf.Abs(found_value) - Mathf.Abs(position);
            if (first_compare < second_compare)
            {
                return_tile_position = i + (-(sign_value));
            }
            else
            {
                return_tile_position = i;
            }
        }
        return return_tile_position;
    }
}
