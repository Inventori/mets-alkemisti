using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioPlayback : MonoBehaviour
{
    [SerializeField] private AudioClipsSO audioClipsSO;
    [SerializeField] private AudioSource audioSourcePrefab;
    [SerializeField] private int audioSourcePoolSize = 10;

    private AudioSource _voiceOverSource;
    private AudioSource _ambientSource;
    private AudioSource _musicSource;

    public AudioMixerGroup ambientMixerGroup;
    public AudioMixerGroup voiceOverixerGroup;
    public AudioMixerGroup sfxMixerGroup;
    public AudioMixerGroup musicMixerGroup;

    [SerializeField] private AudioClip ambientClip;

    [SerializeField] private Song automaticallyPlaySong;
    [SerializeField] private AudioClip introMusic;
    [SerializeField] private AudioClip inGameMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip gameCompletedMusic;

    [SerializeField] private GameObject subtitlePanel;
    [SerializeField] private Button proceedButton;
    [SerializeField] private TMP_Text subtitleText;

    private List<AudioSource> _audioSourcePool = new();

    private AudioClipData _currentClip;
    private Action<bool> _onClipFinished;

    private void Awake()
    {
        for (var i = 0; i < audioSourcePoolSize; i++)
        {
            var audioSource = Instantiate(audioSourcePrefab, transform);
            audioSource.outputAudioMixerGroup = sfxMixerGroup;
            _audioSourcePool.Add(audioSource);
        }

        _ambientSource = Instantiate(audioSourcePrefab, transform);
        _ambientSource.outputAudioMixerGroup = ambientMixerGroup;
        _ambientSource.loop = true;
        PlayAmbient();

        _voiceOverSource = Instantiate(audioSourcePrefab, transform);
        _voiceOverSource.outputAudioMixerGroup = voiceOverixerGroup;

        _musicSource = Instantiate(audioSourcePrefab, transform);
        _musicSource.outputAudioMixerGroup = musicMixerGroup;
        _musicSource.loop = true;
        
        PlaySong(automaticallyPlaySong);
    }

    private void OnEnable()
    {
        proceedButton.onClick.AddListener(OnProceedClicked);
    }

    private void OnDisable()
    {
        proceedButton.onClick.RemoveListener(OnProceedClicked);
    }
    
    private void PlayAmbient()
    {
        _ambientSource.clip = ambientClip;
        _ambientSource.Play();
    }

    public void PlaySong(Song song) 
    {
        switch (song)
        {
            case Song.Intro:
                PlayIntroMusic();
                break;
            case Song.InGame:
                PlayInGameMusic();
                break;
            case Song.GameOver:
                PlayGameOverMusic();
                break;
            case Song.GameCompleted:
                PlayGameCompletedMusic();
                break;
        }
    }
    
    public void PlayIntroMusic()
    {
        _musicSource.clip = introMusic;
        _musicSource.Play();
    }
    
    public void PlayInGameMusic()
    {
        _musicSource.clip = inGameMusic;
        _musicSource.Play();
    }

    public void PlayGameOverMusic()
    {
        _musicSource.clip = gameOverMusic;
        _musicSource.Play();
    }
    
    public void PlayGameCompletedMusic()
    {
        _musicSource.clip = gameCompletedMusic;
        _musicSource.Play();
    }
    
    public void PlaySfx(string id)
    {
        _currentClip = audioClipsSO.GetClip(id);
        if (_currentClip == null)
        {
            Debug.LogWarning($"Clip with id {id} not found");
            return;
        }

        if(_currentClip.audioClip != null)
        {
            var source = GetAudioSource();
            source.PlayOneShot(_currentClip.audioClip);
        }
    }

    public void PlayVoiceOver(string id, Action<bool> onClipFinished = null)
    {
        _currentClip = audioClipsSO.GetClip(id);
        if(onClipFinished != null)
        {
            _onClipFinished = onClipFinished;
            Debug.LogWarning("asd");
        }

        if (_currentClip == null)
        {
            Debug.LogWarning($"Clip with id {id} not found");
            return;
        }

        _voiceOverSource.Stop();
        _voiceOverSource.PlayOneShot(_currentClip.audioClip);
        
        subtitleText.text = _currentClip.subtitle;
        subtitlePanel.SetActive(true);
    }

    private AudioSource GetAudioSource()
    {
        return _audioSourcePool.FirstOrDefault(x => !x.isPlaying);
    }

    private void OnProceedClicked()
    {
        if (_currentClip == null)
        {
            VoiceOverFinished(true);
            return;
        }

        subtitleText.text = "";

        if (!string.IsNullOrEmpty(_currentClip.nextClipId))
        {
            PlayVoiceOver(_currentClip.nextClipId);
            VoiceOverFinished(false);
        }
        else
        {
            VoiceOverFinished(true);
        }
    }

    private void VoiceOverFinished(bool finished)
    {
        _onClipFinished?.Invoke(finished);
        if(finished)
        {
            _currentClip = null;
            _onClipFinished = null;
            subtitlePanel.SetActive(false);
        }
    }
}

public enum Song
{
    None,
    Intro,
    InGame,
    GameOver,
    GameCompleted
}