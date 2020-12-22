using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    bool isPlaying;
    AudioSource audioSource;

    public bool IsPlaying
    {
        get
        {
            return isPlaying;
        }
    }

    public void StartNarration(AudioClip clip)
    {
        audioSource.clip = clip;
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        musicPlayer.GetComponent<AudioSource>().volume *= 0.5f;
        audioSource.Play();
    }

    public void StopNarration()
    {
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        musicPlayer.GetComponent<AudioSource>().volume = 0.3f;
        audioSource.Stop();
    }
    void Start()
    {
        audioSource=gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource.isPlaying)
        {
            isPlaying = true;
        }
        else
        {
            isPlaying = false;
        }
    }
}
