using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationClips : MonoBehaviour
{
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    [SerializeField] AudioClip[] narrationClips;

    public Dictionary<string, AudioClip> AudioClips
    {
        get
        {
            return audioClips;
        }
    }
    void Start()
    {
        PopulateDictionary();
    }

    void PopulateDictionary()
    {
        audioClips.Add("Mercury", narrationClips[0]);
        audioClips.Add("Venus", narrationClips[1]);
        audioClips.Add("Earth", narrationClips[2]);
        audioClips.Add("Mars", narrationClips[3]);
        audioClips.Add("Jupiter", narrationClips[4]);
        audioClips.Add("Saturn", narrationClips[5]);
        audioClips.Add("Uranus", narrationClips[6]);
        audioClips.Add("Neptune", narrationClips[7]);
    }

}
