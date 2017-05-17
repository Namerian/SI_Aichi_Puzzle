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
    private float _speed = 1f;

    [SerializeField]
    private float _stopTime = 0.8f;

    [SerializeField]
    private Rigidbody _rigidbody;

    //[SerializeField]
    //private GameObject _trapPrefab;

    [SerializeField]
    private List<Card> _cards = new List<Card>();

    [SerializeField]
    private int _numLives = 1;
	
	[SerializeField]
    private float _kyoiPoints = 0;
	
	[SerializeField]
    private GameObject ennemiesFollowingPrefab;
	
	[SerializeField]
    private List<GameObject> ennemiesFollowing = new List<GameObject>();
	
	[SerializeField]
    private Vector3[] trailPositions;

    //=================================================================
    // Variables - private
    //=================================================================

    private bool _stopped = false;
    private Vector3 _lastRotation;
    private float _kyoiPoints = 0;

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
        LevelManager.Instance.RegisterPlayer(this);

        _lastRotation = new Vector3(0, 90, 0);
        ShowCardsInUi();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead || LevelManager.Instance.IsGamePaused)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        DirectionInputs();
        //ActionInputs();
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
            _stopped = true;

            this.transform.localEulerAngles = _lastRotation;
            _lastRotation = new Vector3(0, 90, 0);

            Invoke("StartMoving", _stopTime);
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            if (_cards.Count == 0)
            {
                Debug.Log("TU MEURS!");
                PlayerDown();
            }
            else
            {
                Enemy enemy = collision.collider.GetComponent<Enemy>();
                CardResult result = _cards[0].ResolveCard(this, enemy);

                switch (result)
                {
                    case CardResult.PlayerVictory:
                        enemy.gameObject.SetActive(false);
                        break;
                    case CardResult.EnemyVictory:
                        Debug.Log("TU MEURS!");
                        PlayerDown();
                        break;
                }

                RemoveCurrentCard();
            }
        }
    }

    private void StartMoving()
    {
        _stopped = false;
    }

    //private void ActionInputs()
    //{
    //    if (Input.GetButton(_name + "_Action") && _cards[0].cardName == CardsName.trap)
    //    {
    //        GameObject trap = Instantiate(_trapPrefab);
    //        trap.transform.position = transform.position;
    //        if (_cards[0].cardName == CardsName.trap) RemoveCurrentCard();
    //    }
    //}

    private void CheckCard()
    {

    }

    private void RemoveCurrentCard()
    {
        _cards.RemoveAt(0);

        ShowCardsInUi();
    }

    private void ShowCardsInUi()
    {
        Sprite currentCardSprite = null;
        Sprite nextCardSprite = null;

        if (_cards.Count > 0)
        {
            currentCardSprite = _cards[0].Sprite;
        }

        if (_cards.Count > 1)
        {
            nextCardSprite = _cards[1].Sprite;
        }

        GameUiManager.Instance.IngameUi.SetPlayerCards(_name, currentCardSprite, nextCardSprite);
    }
}
	private float _trailMaxLength;

    [SerializeField]						  				
    private int _numDeaths = 0;
	private TrailRenderer trailRenderer;								
	private Vector3 _ennemiesBounds;															   
    public bool IsDead { get; private set; }

		_ennemiesBounds = ennemiesFollowingPrefab.GetComponentInChildren<Renderer>().bounds.size;
        trailRenderer = GetComponent<TrailRenderer>();

		SetTrailTime();
        CreateFollowingEnnemies();
        trailPositions = new Vector3[trailRenderer.positionCount];
        trailRenderer.GetPositions(trailPositions);
        if (ennemiesFollowing.Count > 0) UpdateTrail();
	 private void SetTrailTime()
    {
        if (_name == "PlayerA") trailRenderer.time = _trailMaxLength * ((100 - LevelManager.Instance._kyoiSliderValue * 100) / 100);
        else trailRenderer.time = _trailMaxLength * LevelManager.Instance._kyoiSliderValue;
    }
        if (collision.collider.CompareTag("Wall") && !IsDead)
        }
        else if (collision.collider.CompareTag("Enemy") && !IsDead)
        }
        else if (collision.collider.CompareTag("Player") && IsDead && _numDeaths < _numLives)
        {
            Player otherPlayer = collision.collider.GetComponent<Player>();

            if (!otherPlayer.IsDead)
            {
                IsDead = false;

                Vector3 rotation = this.transform.localEulerAngles;
                rotation.z = 0;
                this.transform.localEulerAngles = rotation;
            }
        }
    }

    }

    private void PlayerDown()
    {
        IsDead = true;
        _rigidbody.velocity = Vector3.zero;

        Vector3 rotation = this.transform.localEulerAngles;
        rotation.z = 90;
        this.transform.localEulerAngles = rotation;												 

        _numDeaths++;
    }
	void CreateFollowingEnnemies()
    {
        if(trailRenderer.positionCount * trailRenderer.minVertexDistance > ennemiesFollowing.Count * _ennemiesBounds.z)
        {
            ennemiesFollowing.Add(Instantiate(ennemiesFollowingPrefab));
            print("EF" + ennemiesFollowing.Count);
        }
        /*
        if(ennemiesFollowing.Count * _ennemiesBounds.z > trailRenderer.positionCount * trailRenderer.minVertexDistance)
        {
            if (ennemiesFollowing[ennemiesFollowing.Count - 1] != null)
            {
                ennemiesFollowing.RemoveAt(ennemiesFollowing.Count - 1);
                Destroy(ennemiesFollowing[ennemiesFollowing.Count - 1]);
            }
        }
        */
    }

	void UpdateTrail()
    {
        for (int i = 0; i < ennemiesFollowing.Count; i++)
        {
            //ennemiesFollowing[i].transform.position = trailRenderer.GetPosition(trailRenderer.positionCount - (i + 1 * trailRenderer.positionCount)); //trailRenderer.GetPosition(i); 
            ennemiesFollowing[i].transform.position = trailPositions[(trailPositions.Length - 1) - (i * (trailPositions.Length / ennemiesFollowing.Count))];
        }
    }