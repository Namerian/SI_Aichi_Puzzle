using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    public float offSet;
    public float upOffSet;
    private Vector3 direction;
    private Vector3 transformPosition;
    private Vector3 camPosition;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transformPosition = transform.parent.position;
        transformPosition.y = transformPosition.y + upOffSet;
        camPosition = Camera.main.transform.position;
        camPosition.y = transformPosition.y;
        transform.position = transformPosition + ((transformPosition - camPosition) * offSet);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);

    }
}
