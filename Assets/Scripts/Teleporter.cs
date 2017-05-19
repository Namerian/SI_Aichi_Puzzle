using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject _highlight;

    [SerializeField]
    private string _nextLevelName = "MenuScene";

    private bool _active = false;

    // Use this for initialization
    void Start()
    {
        _highlight.gameObject.SetActive(false);
        LevelManager.Instance.RegisterTeleporter(this);
    }

    public void Activate()
    {
        _highlight.gameObject.SetActive(true);
        _active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_active && other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance._fxAudioSource, SoundManager.Instance._Win, false);
            SoundManager.Instance.Stop(SoundManager.Instance._motorAudioSource);
            SceneManager.LoadScene(_nextLevelName);
        }
    }
}
