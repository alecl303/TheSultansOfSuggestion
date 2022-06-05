using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    [SerializeField]
    private string musicTrack = "Ominous";

    void Start()
    {
        FindObjectOfType<SoundManager>().PlayMusicTrack(this.musicTrack);
    }
}
