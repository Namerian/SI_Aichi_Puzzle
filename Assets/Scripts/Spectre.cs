using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectre : MonoBehaviour {

    public GameObject[] ldToDestroy;

	void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.GetComponent<Player>())
        {
            Debug.Log("Touchey");
            foreach (GameObject g in ldToDestroy)
            {
                Destroy(g);
                //FX ?
            }
        }
    }
}
