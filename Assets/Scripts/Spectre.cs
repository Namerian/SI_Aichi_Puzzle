using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectre : MonoBehaviour {

    public GameObject[] ldToDestroy;

	void OnCollisionEnter(Collision c)
    {
        if(c.collider.gameObject == GetComponent<Player>())
        {
            foreach (GameObject g in ldToDestroy)
            {
                Destroy(g);
                //FX ?
            }
        }
    }
}
