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
    private float _maxSpeed;

    [SerializeField]
    private float _minSpeed;

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
    public List<GameObject> enemiesFollowing = new List<GameObject>();

    [SerializeField]
    public List<int> enemiesFollowingPosition = new List<int>();

    [SerializeField]
    public List<Vector3> enemiesLastTurnPosition = new List<Vector3>();

    [SerializeField]
    private Vector3[] trailPositions;

    [SerializeField]
    private float _trailMaxLength;

    [SerializeField]
    private int _maxBikeNumber;

    //=================================================================
    // Variables - private
    //=================================================================

    private bool _stopped = false;
    private Vector3 _lastRotation;
    private int _numDeaths = 0;
    private TrailRenderer trailRenderer;
    private Vector3 _ennemiesBounds;
    private int _bikeNumber;
    private float _speed = 1f;

    //=================================================================
    // Properties
    //=================================================================

    public float KyoiPoints { get { return _kyoiPoints; } set { _kyoiPoints = value; } }
    public bool IsDead { get; private set; }
    public int BikeNumber { get; set; }

    //=================================================================
    // Monobehaviour Methods
    //=================================================================

    // Use this for initialization
    void Start()
    {
        _ennemiesBounds = ennemiesFollowingPrefab.GetComponentInChildren<Renderer>().bounds.size;
        LevelManager.Instance.RegisterPlayer(_name, this);
        trailRenderer = GetComponent<TrailRenderer>();

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

        SetTrailTime();
        CreateFollowingEnnemies();
        trailPositions = new Vector3[trailRenderer.positionCount];
        trailRenderer.GetPositions(trailPositions);
        if (enemiesFollowing.Count > 0) UpdateTrail();

        if (_name == "PlayerA") _speed = Mathf.Lerp(_minSpeed, _maxSpeed, (100 - LevelManager.Instance.KyoiSliderValue * 100) / 100);
        else _speed = Mathf.Lerp(_minSpeed, _maxSpeed, LevelManager.Instance.KyoiSliderValue);

        //ActionInputs();
    }

    //=================================================================
    // Private Methods
    //=================================================================

    private void SetTrailTime()
    {
        if (_name == "PlayerA") trailRenderer.time = _trailMaxLength * ((100 - LevelManager.Instance.KyoiSliderValue * 100) / 100);
        else trailRenderer.time = _trailMaxLength * LevelManager.Instance.KyoiSliderValue;
    }

    private void DirectionInputs()
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
        if (collision.collider.CompareTag("Wall") && !IsDead)
        {
            //_stopped = true;

            _rigidbody.velocity = Vector3.zero;

            this.transform.localEulerAngles = _lastRotation;
            _lastRotation = new Vector3(0, 90, 0);

            //Invoke("StartMoving", _stopTime);
        }
        else if (collision.collider.CompareTag("Enemy") && !IsDead)
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

    void OnTriggerEnter(Collider c)
    {
        ////////////////////////////////////////////// à revoiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiir
        if (c.CompareTag("EnemyBehindPlayer"))
        {
            EnemyBehindPlayers e = c.GetComponent<EnemyBehindPlayers>();
            if(e.parentName != _name)
            {
                Player p = LevelManager.Instance.Players[e.parentName == "PlayerA" ? 0 : 1];
                int nbBikeFollowing = p.enemiesFollowing.Count;
                float bikeTouched = p.enemiesFollowing.IndexOf(e.gameObject);
                float points = -10;//p.KyoiPoints / _bikeNumber;
                p.KyoiPoints -= points;
                _kyoiPoints += points;
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
        float f;
        if (_name == "PlayerA") f = (100 - LevelManager.Instance.KyoiSliderValue * 100) / 100;
        else f = LevelManager.Instance.KyoiSliderValue;

        if ((trailRenderer.positionCount * trailRenderer.minVertexDistance >
            enemiesFollowing.Count * _ennemiesBounds.z) && _bikeNumber < _maxBikeNumber * f)
        {
            GameObject g = Instantiate(ennemiesFollowingPrefab);
            enemiesFollowing.Add(g);
            enemiesFollowingPosition.Add(trailRenderer.positionCount - 1);
            enemiesLastTurnPosition.Add(g.transform.position);
            g.GetComponent<EnemyBehindPlayers>().parentName = _name;
            _bikeNumber++;
        }

        if (_bikeNumber > _maxBikeNumber * f)
        {
            Destroy(enemiesFollowing[enemiesFollowing.Count - 1]);
            enemiesFollowing.RemoveAt(enemiesFollowing.Count - 1);
            enemiesFollowingPosition.RemoveAt(enemiesFollowingPosition.Count - 1);
            _bikeNumber--;
        }

    }

    void UpdateTrail()
    {
        for (int i = 0; i < enemiesFollowing.Count; i++)
        {
            //ennemiesFollowing[i].transform.position = trailRenderer.GetPosition(trailRenderer.positionCount - (i + 1 * trailRenderer.positionCount)); //trailRenderer.GetPosition(i); 
            enemiesFollowing[i].transform.position = trailPositions[enemiesFollowingPosition[i]];

            if (enemiesFollowing[i].transform.rotation.eulerAngles.y == 90 || enemiesFollowing[i].transform.rotation.eulerAngles.y == 270)
            {
                if (enemiesFollowing[i].transform.position.z > enemiesLastTurnPosition[i].z)
                {
                    enemiesFollowing[i].transform.localEulerAngles = new Vector3(0, 0, 0);
                    enemiesLastTurnPosition[i] = enemiesFollowing[i].transform.position;
                }
                else if (enemiesFollowing[i].transform.position.z < enemiesLastTurnPosition[i].z)
                {
                    enemiesFollowing[i].transform.localEulerAngles = new Vector3(0, 180, 0);
                    enemiesLastTurnPosition[i] = enemiesFollowing[i].transform.position;
                }
            }

            else if (enemiesFollowing[i].transform.rotation.eulerAngles.y == 0 || enemiesFollowing[i].transform.rotation.eulerAngles.y == 180)
            {
                if (enemiesFollowing[i].transform.position.x > enemiesLastTurnPosition[i].x)
                {
                    enemiesFollowing[i].transform.localEulerAngles = new Vector3(0, 90, 0);
                    enemiesLastTurnPosition[i] = enemiesFollowing[i].transform.position;
                }
                else if (enemiesFollowing[i].transform.position.x < enemiesLastTurnPosition[i].x)
                {
                    enemiesFollowing[i].transform.localEulerAngles = new Vector3(0, 270, 0);
                    enemiesLastTurnPosition[i] = enemiesFollowing[i].transform.position;
                }

            }
        }
    }

}
