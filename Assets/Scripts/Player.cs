using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //=================================================================
    // Variables - editor
    //=================================================================

    [SerializeField]
    private string _name = "PlayerA";

    [SerializeField]
    private float _speed = 1.5f;

    [SerializeField]
    private float _stopTime = 0.8f;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private GameObject _trapPrefab;

    [SerializeField]
    private List<Card> _cards = new List<Card>();

    [SerializeField]
    private float _kyoiPoints = 0;

    //=================================================================
    // Variables - private
    //=================================================================

    private bool _stopped = false;
    private Vector3 _lastRotation;

    //=================================================================
    // Properties
    //=================================================================

    public float KyoiPoints { get { return _kyoiPoints; } }

    //=================================================================
    // Monobehaviour Methods
    //=================================================================

    // Use this for initialization
    void Start()
    {
        _lastRotation = new Vector3(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        DirectionInputs();
        ActionInputs();
    }

    //=================================================================
    // Private Methods
    //=================================================================

    private void DirectionInputs()
    {
        float axisY = Input.GetAxis(_name + "_AxisY");
        float axisX = Input.GetAxis(_name + "_AxisX");


        if (_stopped)
        {
            _rigidbody.velocity = this.transform.forward * _speed;
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
        else if (collision.collider.CompareTag("Enemy"))
        {
            Enemy e = collision.collider.GetComponent<Enemy>();

            if (e.isBoss) //boss
            {
                if (_cards[0].cardName == CardsName.attackBig)
                {
                    Destroy(e.gameObject);
                    RemoveCurrentCard();
                }
                else
                {
                    print("TU MEURS");
                }
            }
            else //non boss
            {
                if (_cards[0].cardName == CardsName.attack)
                {
                    Destroy(e.gameObject);
                    RemoveCurrentCard();
                }
                else if (_cards[0].cardName == CardsName.defence)
                {
                    RemoveCurrentCard();
                }
                else
                {
                    print("TU MEURS");
                    RemoveCurrentCard();
                }
            }
        }
    }

    private void StartMoving()
    {
        //Debug.Log("rrrrr");
        _stopped = false;
    }

    private void ActionInputs()
    {
        if (Input.GetButton(_name + "_Action") && _cards[0].cardName == CardsName.trap)
        {
            GameObject trap = Instantiate(_trapPrefab);
            trap.transform.position = transform.position;
            if (_cards[0].cardName == CardsName.trap) RemoveCurrentCard();
        }
    }

    private void CheckCard()
    {

    }

    private void RemoveCurrentCard()
    {
        _cards.RemoveAt(0);
    }
}