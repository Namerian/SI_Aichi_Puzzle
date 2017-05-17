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

<<<<<<< HEAD
    private bool _stopped = false;
    private Vector3 _lastRotation;
=======
    public GameObject trapPrefab;
    public List<Card> cards = new List<Card>();
    public float kyoiPoints = 0;


>>>>>>> rooki

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

    void DirectionInputs()
    {
        float axisY = Input.GetAxis(_name + "_AxisY");
        float axisX = Input.GetAxis(_name + "_AxisX");

<<<<<<< HEAD
        if (_stopped)
=======
        _rigidbody.velocity = this.transform.forward * _speed;

        if (axisY > 0)
>>>>>>> rooki
        {
            _rigidbody.velocity = Vector3.zero;
        }
<<<<<<< HEAD
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
=======
        else if (axisX > 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (axisY < 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (axisX < 0)
>>>>>>> rooki
        {
            //Debug.Log("ttttttttttttttttttt");

            _stopped = true;

            this.transform.localEulerAngles = _lastRotation;
            _lastRotation = new Vector3(0, 90, 0);

            Invoke("StartMoving", _stopTime);
        }
<<<<<<< HEAD
    }

    private void StartMoving()
    {
        //Debug.Log("rrrrr");
        _stopped = false;
    }
}
=======
    }

    void ActionInputs()
    {
        if (Input.GetButton(_name + "_Action") && cards[0].cardName == CardsName.trap)
        {
            GameObject trap = Instantiate(trapPrefab);
            trap.transform.position = transform.position;
            if(cards[0].cardName == CardsName.trap) RemoveCurrentCard();
        }
    }

    void OnCollisionEnter(Collision c)
    {
        Enemy e = c.collider.GetComponent<Enemy>();
        if(e != null)
        {
            if (e.isBoss) //boss
            {
                if (cards[0].cardName == CardsName.attackBig)
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
                if (cards[0].cardName == CardsName.attack)
                {
                    Destroy(e.gameObject);
                    RemoveCurrentCard();
                }
                else if (cards[0].cardName == CardsName.defence)
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

    void CheckCard()
    {

    }

    void RemoveCurrentCard()
    {
        cards.RemoveAt(0);
    }

}
>>>>>>> rooki
