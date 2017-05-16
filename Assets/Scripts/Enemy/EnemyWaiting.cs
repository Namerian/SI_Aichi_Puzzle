using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaiting : MonoBehaviour {

    NavMeshAgent agent;
    public GameObject target;
    public bool isBoss;
    public GameObject[] players = new GameObject[2];
    public float distTreshold;

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
        if (target != null) agent.SetDestination(target.transform.position);
        else checkPlayersDist();
	}

    private void checkPlayersDist()
    {
        if (checkDist(players[0].transform.position)) target = players[0];
        else if (checkDist(players[1].transform.position)) target = players[1];
    }

    bool checkDist(Vector3 distPos)
    {
        if (Vector3.Distance(distPos, transform.position) < distTreshold)
        {
            return true;
        }
        else return false;
    }

}
