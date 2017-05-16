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

    public GameObject trapPrefab;
    public List<Card> cards = new List<Card>();
    public float kyoiPoints = 0;



    // Use this for initialization
    void Start()
    {
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

        _rigidbody.velocity = this.transform.forward * _speed;

        if (axisY > 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (axisX > 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (axisY < 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (axisX < 0)
        {
            this.transform.localEulerAngles = new Vector3(0, -90, 0);
        }
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
