
    private int _numDeaths = 0;
    [SerializeField]
    private float _kyoiPoints = 0;
    private TrailRenderer trailRenderer;
    [SerializeField]
    private GameObject ennemiesFollowingPrefab;
    private Vector3 _ennemiesBounds;
    [SerializeField]
    private List<GameObject> ennemiesFollowing = new List<GameObject>();
    [SerializeField]
    private Vector3[] trailPositions;
    public float KyoiPoints { get; private set; }
    public bool IsDead { get; private set; }

        LevelManager.Instance.RegisterPlayer(_name, this);

        //GameObject firstFollowing = Instantiate(ennemiesFollowingPrefab);
        //ennemiesFollowing.Add(firstFollowing);
        _ennemiesBounds = ennemiesFollowingPrefab.GetComponentInChildren<Renderer>().bounds.size;
        LevelManager.Instance.RegisterPlayer(this);
        trailRenderer = GetComponent<TrailRenderer>();

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
