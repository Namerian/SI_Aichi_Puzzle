using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioMixerGroup _fxMixer, _musicMixer, _motorMixer;

    public AudioSource _fxAudioSource;

    public AudioSource _musicAudioSource;

    public AudioSource _motorAudioSource;

    public AudioClip _audioClips;

    public AudioClip _GameMusic;

    public AudioClip _MenuMusic;

    public AudioClip _HitEnemy;

    public AudioClip _HitPlayer;

    public AudioClip _Lose;

    public AudioClip _MotorSound;

    public AudioClip _PreRevive;

    public AudioClip _Revive;

    public AudioClip _SelectMenuItem;

    public AudioClip _Win;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            _fxAudioSource = gameObject.AddComponent<AudioSource>();
            _fxAudioSource.outputAudioMixerGroup = _fxMixer;
            _musicAudioSource = gameObject.AddComponent<AudioSource>();
            _musicAudioSource.outputAudioMixerGroup = _musicMixer;
            _motorAudioSource = gameObject.AddComponent<AudioSource>();
            _motorAudioSource.outputAudioMixerGroup = _motorMixer;
            PlaySound(_musicAudioSource, _MenuMusic, true);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySound(AudioSource audioSource, AudioClip audioClip, bool loop)
    {
        //audioSource.clip = audioClip;
        audioSource.loop = loop;
        if (loop)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else audioSource.PlayOneShot(audioClip);
        print("PLAY : " + audioClip.name);
    }

    public void Stop(AudioSource audioSource)
    {
        audioSource.Stop();
    }
}
