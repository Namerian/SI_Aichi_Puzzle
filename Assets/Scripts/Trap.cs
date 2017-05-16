using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	
	void OnCollisionEnter(Collision c)
    {
        if(c.collider.gameObject.GetComponent<EnemyFollowing>() || c.collider.gameObject.GetComponent<EnemyWaiting>())
        {
            Destroy(c.gameObject);
            //FX
        }
    }
}
