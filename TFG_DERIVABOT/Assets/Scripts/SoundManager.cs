using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] SoundClips;

    private Dictionary<string, AudioSource> _sources = new Dictionary<string, AudioSource>();

    public static SoundManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);

            foreach (AudioClip clip in SoundClips)
            {
                GameObject sourceObject = new GameObject(clip.name);
                sourceObject.transform.parent = transform;

                AudioSource source = sourceObject.AddComponent<AudioSource>();
                source.clip = clip;
                source.volume = 0.5f;

                _sources.Add(clip.name, source);
            }
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.OnCambioVolumen += OnCambioVolumen;
    }

    private void OnCambioVolumen(float volumen)
    {
        foreach(AudioSource source in _sources.Values)
        {
            source.volume = volumen;    
        }
    }

    public void PlayClip(string clip, float start = 0)
    {
        if (_sources.TryGetValue(clip, out AudioSource audioSource))
        {
            audioSource.time = start * audioSource.clip.length;
            audioSource.Play();
        }
    }

    public void StopClip(string clip)
    {
        if (_sources.TryGetValue(clip, out AudioSource audioSource))
            audioSource.Stop();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCambioVolumen -= OnCambioVolumen;
    }
}
