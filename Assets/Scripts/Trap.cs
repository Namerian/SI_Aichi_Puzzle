using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	
	void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.GetComponent<EnemyFollowing>() || c.gameObject.GetComponent<EnemyWaiting>())
        {
            Destroy(c.gameObject);
            Destroy(gameObject);
            //FX
        }
    }
}
