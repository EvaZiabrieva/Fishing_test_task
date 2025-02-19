using System;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class DefaultAudioPlayer : MonoBehaviour
{
    [SerializeField] private string _clipId;
    private AudioSystem _audioSystem;
    private AudioSource _audioSource;

    public void Initialize()
    {
        _audioSystem = SystemsContainer.GetSystem<AudioSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        _audioSystem.PlaySound(_clipId, _audioSource, false);
    }

    public void Stop()
    {
        _audioSystem.StopSound(_audioSource);
    }
}
