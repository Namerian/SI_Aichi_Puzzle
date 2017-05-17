using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private List<string> _levelNames;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnLevelButtonPressed(int id)
    {
        if(_levelNames.Count >= id)
        {
            SceneManager.LoadScene("PersistentScene", LoadSceneMode.Additive);
            SceneManager.LoadScene(_levelNames[id]);
        }
    }
}
