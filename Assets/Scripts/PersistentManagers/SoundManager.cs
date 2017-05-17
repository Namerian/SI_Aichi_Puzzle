using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private List<AudioClip> _audioClips;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySound(string audioClipName)
    {
        foreach(AudioClip clip in _audioClips)
        {
            if(clip.name == audioClipName)
            {
                _audioSource.clip = clip;
                _audioSource.Play();
                break;
            }
        }
    }
}
