using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //=================================================================
    // Variables
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

    [SerializeField]
    private List<Card> _cards = new List<Card>();

    [SerializeField]
    private int _numLives = 1;

    [SerializeField]
    private float _kyoiPoints = 0;

    [SerializeField]
    private GameObject _ennemiesFollowingPrefab;

    [SerializeField]
    private List<GameObject> _enemiesFollowing = new List<GameObject>();

    [SerializeField]
    private List<int> _enemiesFollowingPosition = new List<int>();

    //[SerializeField]
    //public List<Vector3> enemiesLastTurnPosition = new List<Vector3>();

    [SerializeField]
    private Vector3[] _trailPositions;

    [SerializeField]
    private float _trailMaxLength;

    [SerializeField]
    private int _maxBikeNumber;

    private bool _stopped = false;

    private Vector3 _lastRotation;

    private int _numDeaths = 0;

    private TrailRenderer _trailRenderer;

    [SerializeField]
    private Vector3 _ennemiesBounds;

    [SerializeField]
    private int _bikeNumber;

    private float _speed = 1f;

    //=================================================================
    // Properties
    //=================================================================

    public float KyoiPoints { get { return _kyoiPoints; } set { _kyoiPoints = value; } }
    public bool IsDead { get; private set; }
    public int BikeNumber { get; set; }
    public List<Card> Cards { get { return _cards; } }

    //=================================================================
    // Monobehaviour Methods
    //=================================================================

    // Use this for initialization
    void Start()
    {
        /*
        _ennemiesBounds = ennemiesFollowingPrefab.GetComponent<BoxCollider>().bounds.size;
        print("_________" + ennemiesFollowingPrefab.GetComponent<BoxCollider>().gameObject.name);
        print("_________" + ennemiesFollowingPrefab.GetComponent<BoxCollider>().bounds.size);
        */
        LevelManager.Instance.RegisterPlayer(_name, this);
        _trailRenderer = GetComponentInChildren<TrailRenderer>();

        _lastRotation = new Vector3(0, 90, 0);
        ShowCardsInUi();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead || LevelManager.Instance.IsGamePaused)
        {
            //Debug.Log("player dead or game paused");
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        DirectionInputs();

        //SetTrailTime();
        _trailPositions = new Vector3[_trailRenderer.positionCount];
        //Vector3[] positions = new Vector3[_trailRenderer.positionCount];
        int numPositions = _trailRenderer.GetPositions(_trailPositions);

        if (_trailPositions.Length > 1)
        {
            _trailPositions[0] = _trailPositions[1];
        }

        if (numPositions != _trailRenderer.positionCount)
        {
            Debug.Log("RRRRRRRRRRRRRRRRRRRRRRRR");
        }

        System.Array.Reverse(_trailPositions);

        if (_enemiesFollowing.Count > 0)
        {
            UpdateTrail();
        }

        CreateFollowingEnnemies();


        if (_name == "PlayerA") _speed = Mathf.Lerp(_minSpeed, _maxSpeed, (100 - LevelManager.Instance.KyoiSliderValue * 100) / 100);
        else _speed = Mathf.Lerp(_minSpeed, _maxSpeed, LevelManager.Instance.KyoiSliderValue);

        //ActionInputs();
    }

    //=================================================================
    // Private Methods
    //=================================================================

    //private void SetTrailTime()
    //{
    //    if (_name == "PlayerA") _trailRenderer.time = _trailMaxLength * ((100 - LevelManager.Instance.KyoiSliderValue * 100) / 100);
    //    else _trailRenderer.time = _trailMaxLength * LevelManager.Instance.KyoiSliderValue;
    //}

    private void DirectionInputs()
    {
        float axisY = Input.GetAxis(_name + "_AxisY");
        float axisX = Input.GetAxis(_name + "_AxisX");


        if (_stopped)
        {
            //Debug.Log("player stopped");
            _rigidbody.velocity = Vector3.zero;
        }
        else
        {
            _rigidbody.velocity = this.transform.forward * _speed;

            Vector3 rot = Vector3.down;

            if (axisY > 0 && this.transform.localEulerAngles != new Vector3(0, 180, 0))
            {
                rot = new Vector3(0, 0, 0);
            }
            else if (axisX > 0 && this.transform.localEulerAngles != new Vector3(0, 270, 0))
            {
                rot = new Vector3(0, 90, 0);
            }
            else if (axisY < 0 && this.transform.localEulerAngles != new Vector3(0, 0, 0))
            {
                rot = new Vector3(0, 180, 0);
            }
            else if (axisX < 0 && this.transform.localEulerAngles != new Vector3(0, 90, 0))
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

            PlayerDown();

            //if(this.transform.localEulerAngles == _lastRotation)
            //{
            //    Vector3 rot = this.transform.localEulerAngles;
            //    rot.y += 90;
            //    this.transform.localEulerAngles = rot;
            //}
            //else
            //{
            //    this.transform.localEulerAngles = _lastRotation;
            //    _lastRotation = new Vector3(0, 90, 0);
            //}

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
        if (c.CompareTag("EnemyBehindPlayer"))
        {
            //Debug.Log("check");
            EnemyBehindPlayers enemy = c.GetComponent<EnemyBehindPlayers>();
            //print("Hello : " + c.gameObject.name + " ? " + enemy.parentName + "??" + _name);

            //Condition 1
            if (enemy.parentName == _name)
            {
                //Debug.Log("merde1");
                return;
            }

            //Condition 2
            Player otherPlayer = LevelManager.Instance.GetOtherPlayer(this);
            if (otherPlayer.KyoiPoints < this.KyoiPoints)
            {
                //Debug.Log("merde2");
                return;
            }

            //Condition 3
            int angle = (int)Vector3.Angle(this.transform.forward, enemy.transform.forward);
            //Debug.Log("ANGLE: " + angle);

            if (angle != 90)
            {
                Debug.Log("merde3");
                return;
            }

            //Do Stuff
            float bikeScore = (100 / _maxBikeNumber);
            int otherPlayerBikes = _maxBikeNumber - _enemiesFollowing.Count;
            int numBikesToTransfer = otherPlayerBikes - otherPlayer._enemiesFollowing.IndexOf(enemy.gameObject);

            otherPlayer.KyoiPoints -= bikeScore * numBikesToTransfer;
            _kyoiPoints += bikeScore * numBikesToTransfer;


            //if (enemy.parentName != _name)
            //{
            //    Player p = LevelManager.Instance.Players[enemy.parentName == "PlayerA" ? 0 : 1];
            //    int nbBikeFollowing = p._enemiesFollowing.Count;
            //    int bikeTouched = p._enemiesFollowing.IndexOf(enemy.gameObject);
            //    float points = (100 / _maxBikeNumber);
            //    for (int i = bikeTouched; i < nbBikeFollowing; i++)
            //    {
            //        if (p._enemiesFollowing[i] != null)
            //        {
            //            print("JE TOUCHE : " + i);
            //            //p.enemiesFollowing[i].GetComponent<Collider>().enabled = false;
            //            p._enemiesFollowing[i].GetComponentInChildren<Renderer>().material.color = Color.red;
            //            //UnityEditor.EditorApplication.isPaused = true;

            //            p.KyoiPoints -= points;
            //            _kyoiPoints += points;
            //            //Destroy(p.enemiesFollowing[i]);

            //            //enemiesFollowing.RemoveAt(i);
            //            //enemiesLastTurnPosition.RemoveAt(i);
            //            //enemiesFollowingPosition.RemoveAt(i);
            //            //Destroy(p.enemiesFollowing[i]);
            //            //_bikeNumber--;
            //        }
            //    }
            //    print("YEYE" + points + "P .. " + p.KyoiPoints + "  " + _maxBikeNumber);
            //}
        }
    }


    private void StartMoving()
    {
        _stopped = false;
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

    /// <summary>
    /// Creates or destroys one following bike per call
    /// </summary>
    void CreateFollowingEnnemies()
    {
        int wantedAmountBikes = Mathf.RoundToInt(Mathf.Lerp(0, _maxBikeNumber, (_kyoiPoints / 100)));
        //int wantedAmountBikes = 1;
        if (/*trailPositions.Length > 10 &&*/ _bikeNumber < wantedAmountBikes)
        {
            //print("f" + wantedAmountBikes);
            float dist = .05f; //Vector3.Distance(trailPositions[4], trailPositions[5]);
            if (((_trailPositions.Length - 4) * dist > _enemiesFollowingPosition.Count * _ennemiesBounds.z))
            {
                if (_enemiesFollowing.Count == 0)
                {
                    Vector3 playerPos = this.transform.position;
                    float distance = 0;

                    for (int i = 1; i < _trailPositions.Length; i++)
                    {
                        if (i > 1)
                        {
                            distance += Vector3.Distance(_trailPositions[i - 1], _trailPositions[i]);
                        }

                        if (distance > 1)
                        {
                            GameObject newBike = Instantiate(_ennemiesFollowingPrefab);
                            _enemiesFollowing.Add(newBike);
                            newBike.name = newBike.name + _name + "_" + (_enemiesFollowing.Count - 1);
                            newBike.GetComponent<EnemyBehindPlayers>().parentName = _name;
                            _bikeNumber++;

                            _enemiesFollowingPosition.Add(i);
                            //Debug.Log("new Enemy Bike: id=" + (_enemiesFollowing.Count - 1) + "; trail position index = " + _enemiesFollowingPosition[_enemiesFollowingPosition.Count - 1]);

                            newBike.transform.position = _trailPositions[i];
                            break;
                        }
                    }
                }
                else if (_enemiesFollowing.Count > 0)
                {
                    Vector3 lastBikePos = _enemiesFollowing[_enemiesFollowing.Count - 1].transform.position;
                    float distance = 0;

                    for (int i = _enemiesFollowingPosition[_enemiesFollowing.Count - 1]; i < _trailPositions.Length; i++)
                    {
                        if (i > 1)
                        {
                            distance += Vector3.Distance(_trailPositions[i - 1], _trailPositions[i]);
                        }

                        if (distance > 1)
                        {
                            GameObject newBike = Instantiate(_ennemiesFollowingPrefab);
                            _enemiesFollowing.Add(newBike);
                            newBike.name = newBike.name + _name + "_" + (_enemiesFollowing.Count - 1);
                            newBike.GetComponent<EnemyBehindPlayers>().parentName = _name;
                            _bikeNumber++;

                            _enemiesFollowingPosition.Add(i);
                            //Debug.Log("new Enemy Bike: id=" + (_enemiesFollowing.Count - 1) + "; trail position index = " + _enemiesFollowingPosition[_enemiesFollowingPosition.Count - 1]);

                            newBike.transform.position = _trailPositions[i];
                            break;
                        }
                    }
                }
            }
        }
        else if (_bikeNumber > wantedAmountBikes && _enemiesFollowing.Count > wantedAmountBikes && _enemiesFollowing.Count > 0)
        {
            GameObject g = _enemiesFollowing[_enemiesFollowing.Count - 1];
            if (g != null) Destroy(g);
            _enemiesFollowing.RemoveAt(_enemiesFollowing.Count - 1);
            //enemiesLastTurnPosition.RemoveAt(enemiesLastTurnPosition.Count - 1);
            _enemiesFollowingPosition.RemoveAt(_enemiesFollowingPosition.Count - 1);

            if (_enemiesFollowing.Count != _enemiesFollowingPosition.Count)
            {
                Debug.Log("PROBLEM!!!!");
            }

            _bikeNumber--;
        }
    }

    /// <summary>
    /// Updates the rotation and position of the folowing bikes
    /// </summary>
    void UpdateTrail()
    {
        if (_enemiesFollowing.Count > 0)
        {
            Vector3 playerPos = this.transform.position;
            GameObject newBike = _enemiesFollowing[0];
            float distance = 0;

            for (int j = 1; j < _trailPositions.Length; j++)
            {
                if (j > 1)
                {
                    distance += Vector3.Distance(_trailPositions[j - 1], _trailPositions[j]);
                }

                if (distance > 1)
                {
                    newBike.transform.position = _trailPositions[j];

                    //Debug.Log("enemiesFollowingPosition[0]=" + _enemiesFollowingPosition[0]);
                    Vector3 previousTrailPos = _trailPositions[_enemiesFollowingPosition[0] - 1];
                    Vector3 currentTrailPos = _trailPositions[_enemiesFollowingPosition[0]];
                    Vector3 forward = previousTrailPos - currentTrailPos;
                    _enemiesFollowing[0].transform.forward = forward.normalized;

                    _enemiesFollowingPosition[0] = j;

                    break;
                }
            }
        }

        if (_enemiesFollowing.Count > 1)
        {
            for (int i = 1; i < _enemiesFollowing.Count; i++)
            {
                Vector3 lastBikePos = _enemiesFollowing[i - 1].transform.position;
                GameObject newBike = _enemiesFollowing[i];
                float distance = 0;

                for (int j = _enemiesFollowingPosition[i - 1]; j < _trailPositions.Length; j++)
                {
                    if (j > 1)
                    {
                        distance += Vector3.Distance(_trailPositions[i - 1], _trailPositions[i]);
                    }

                    if (distance > 1)
                    {
                        newBike.transform.position = _trailPositions[j];

                        Vector3 previousTrailPos = _trailPositions[_enemiesFollowingPosition[i] - 1];
                        Vector3 currentTrailPos = _trailPositions[_enemiesFollowingPosition[i]];
                        Vector3 forward = previousTrailPos - currentTrailPos;
                        _enemiesFollowing[i].transform.forward = forward.normalized;

                        _enemiesFollowingPosition[i] = j;

                        break;
                    }
                }
            }
        }


        //for (int i = 0; i < _enemiesFollowing.Count; i++)
        //{
        //    if (_enemiesFollowing[i] != null)
        //    {



        //ennemiesFollowing[i].transform.position = trailRenderer.GetPosition(trailRenderer.positionCount - (i + 1 * trailRenderer.positionCount)); //trailRenderer.GetPosition(i); 

        //moves the following bike
        //Debug.Log("i=" + i);
        //Debug.Log("enemy pos=" + enemiesFollowing[i].transform.position);
        //Debug.Log("trail pos=" + trailPositions[enemiesFollowingPosition[i]]);


        //_enemiesFollowing[i].transform.position = _trailPositions[_enemiesFollowingPosition[i]];

        ////
        //Debug.Log("enemyFollowingPosition:" + _enemiesFollowingPosition[i]);
        //Debug.Log("previous trailPosition: " + _trailPositions[_enemiesFollowingPosition[i] - 1]);
        //Debug.Log("current trailPosition: " + _trailPositions[_enemiesFollowingPosition[i]]);

        //Vector3 previousTrailPos = _trailPositions[_enemiesFollowingPosition[i] - 1];
        //Vector3 currentTrailPos = _trailPositions[_enemiesFollowingPosition[i]];
        //Vector3 forward = previousTrailPos - currentTrailPos;
        //_enemiesFollowing[i].transform.forward = forward.normalized;

        //if (enemiesFollowing[i].transform.rotation.eulerAngles.y == 90 || enemiesFollowing[i].transform.rotation.eulerAngles.y == 270)
        //{
        //    if (enemiesFollowing[i].transform.position.z > enemiesLastTurnPosition[i].z)
        //    {
        //        enemiesFollowing[i].transform.localEulerAngles = new Vector3(0, 0, 0);
        //        enemiesLastTurnPosition[i] = enemiesFollowing[i].transform.position;
        //    }
        //    else if (enemiesFollowing[i].transform.position.z < enemiesLastTurnPosition[i].z)
        //    {
        //        enemiesFollowing[i].transform.localEulerAngles = new Vector3(0, 180, 0);
        //        enemiesLastTurnPosition[i] = enemiesFollowing[i].transform.position;
        //    }
        //}

        //else if (enemiesFollowing[i].transform.rotation.eulerAngles.y == 0 || enemiesFollowing[i].transform.rotation.eulerAngles.y == 180)
        //{
        //    if (enemiesFollowing[i].transform.position.x > enemiesLastTurnPosition[i].x)
        //    {
        //        enemiesFollowing[i].transform.localEulerAngles = new Vector3(0, 90, 0);
        //        enemiesLastTurnPosition[i] = enemiesFollowing[i].transform.position;
        //    }
        //    else if (enemiesFollowing[i].transform.position.x < enemiesLastTurnPosition[i].x)
        //    {
        //        enemiesFollowing[i].transform.localEulerAngles = new Vector3(0, 270, 0);
        //        enemiesLastTurnPosition[i] = enemiesFollowing[i].transform.position;
        //    }

        //}
        //    }
        //}
    }

}
