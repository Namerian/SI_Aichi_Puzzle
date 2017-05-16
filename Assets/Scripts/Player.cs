using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private string _name = "PlayerA";

    [SerializeField]
    private float _speed = 1.5f;

    [SerializeField]
    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float axisY = Input.GetAxis(_name + "_AxisY");
        float axisX = Input.GetAxis(_name + "_AxisX");

        _rigidbody.velocity = this.transform.forward * _speed;

        if(axisY > 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if(axisX > 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if(axisY < 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if(axisX < 0)
        {
            this.transform.localEulerAngles = new Vector3(0, -90, 0);
        }
    }
}
