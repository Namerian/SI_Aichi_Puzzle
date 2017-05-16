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
    private float _stopTime = 0.8f;

    [SerializeField]
    private Rigidbody _rigidbody;

    private bool _stopped = false;
    private Vector3 _lastRotation;

    // Use this for initialization
    void Start()
    {
        _lastRotation = new Vector3(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float axisY = Input.GetAxis(_name + "_AxisY");
        float axisX = Input.GetAxis(_name + "_AxisX");

        if (_stopped)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        else
        {
            _rigidbody.velocity = this.transform.forward * _speed;

            Vector3 rot = Vector3.down;

            if (axisY > 0)
            {
                rot = new Vector3(0, 0, 0);
            }
            else if (axisX > 0)
            {
                rot = new Vector3(0, 90, 0);
            }
            else if (axisY < 0)
            {
                rot = new Vector3(0, 180, 0);
            }
            else if (axisX < 0)
            {
                rot = new Vector3(0, 270, 0);
            }

            if (rot != Vector3.down && this.transform.localEulerAngles != rot)
            {
                _lastRotation = this.transform.localEulerAngles;
                //Debug.Log(_lastRotation);
                this.transform.localEulerAngles = rot;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            //Debug.Log("ttttttttttttttttttt");

            _stopped = true;

            this.transform.localEulerAngles = _lastRotation;
            _lastRotation = new Vector3(0, 90, 0);

            Invoke("StartMoving", _stopTime);
        }
    }

    private void StartMoving()
    {
        //Debug.Log("rrrrr");
        _stopped = false;
    }
}
