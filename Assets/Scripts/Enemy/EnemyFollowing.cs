//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class EnemyFollowing : Enemy {

//    NavMeshAgent agent;
//    public GameObject target;

//    void Awake()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        agent.updateRotation = false;
//    }

//	// Use this for initialization
//	void Start () {
//        LevelManager.Instance.enemiesFollowing.Add(this);
//    }
	
//	// Update is called once per frame
//	void Update () {
//        agent.SetDestination(target.transform.position);
//	}
//}
