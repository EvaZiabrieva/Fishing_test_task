using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour, ISystem
{
    [SerializeField] private List<AudioClipReference> _audioClipReferences;
    [SerializeField] private AudioSource _globalSource;

    private Dictionary<string, AudioClip> _audioClips;

    [Serializable]
    public class AudioClipReference
    {
        [field: SerializeField] public string Id { get; private set; }
        [field:SerializeField] public AudioClip Clip { get; private set; }
    }

    public bool IsInitialized => _audioClips != null;

    public void Initialize()
    {
        _audioClips = new Dictionary<string, AudioClip>(_audioClipReferences.Count);
        foreach (AudioClipReference reference in _audioClipReferences)
        {
            if (_audioClips.ContainsKey(reference.Id))
            {
                Debug.LogWarning($"Audio clip with id {reference.Id} is duplicated. Check references list");
                continue;
            }

            _audioClips.Add(reference.Id, reference.Clip);
        }
    }

    public void Shutdown()
    {
        _audioClips?.Clear();
    }

    public void PlayGlobalSound(string id, bool loop, bool forceStop = true)
    {
        if(!GetAudioClip(id, out AudioClip clip))
        {
            return;
        }

        if(_globalSource.isPlaying && !forceStop)
        {
            Debug.LogWarning($"Global audio source is already playing clip {_globalSource.clip.name}");
            return;
        }

        PlayOnSource(_globalSource, clip, loop);
    }

    public void StopGlobalSound(AudioSource source)
    {
        StopSound(_globalSource);
    }

    public void PlaySound(string id, AudioSource source, bool loop = false)
    {
        if (!GetAudioClip(id, out AudioClip clip))
        {
            return;
        }

        PlayOnSource(source, clip, loop);
    }

    public void StopSound(AudioSource source)
    {
        if(source.isPlaying)
        {
            source.Stop();
        }
    }

    private bool GetAudioClip(string id, out AudioClip clip)
    {
        if (!_audioClips.TryGetValue(id, out clip))
        {
            Debug.LogError($"Audio clip with id {id} not found");
            return false;
        }

        return true;
    }

    private void PlayOnSource(AudioSource source, AudioClip clip, bool loop)
    {
        source.Stop();
        source.clip = clip;
        source.loop = loop;
        source.Play();
    }
}
