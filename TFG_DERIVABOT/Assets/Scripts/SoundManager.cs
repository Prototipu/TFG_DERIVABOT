using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] soundClips;
    private Button button;
    private AudioSource audioSource;

    private void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    public void PlaySound(int audioClip)
    {
        AudioClip selectedClip = soundClips[audioClip];
        if (selectedClip != null)
        {
            audioSource.PlayOneShot(selectedClip);
        }
    }
}
