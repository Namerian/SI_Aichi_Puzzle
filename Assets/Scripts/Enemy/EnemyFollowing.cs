using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowing : MonoBehaviour {

    NavMeshAgent agent;
    public GameObject target;
    public bool isBoss;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.transform.position);
	}
}
