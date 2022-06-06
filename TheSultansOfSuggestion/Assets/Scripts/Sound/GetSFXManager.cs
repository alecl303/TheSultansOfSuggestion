using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSFXManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var soundManager = FindObjectOfType<SoundManager>();
        this.gameObject.GetComponent<Slider>().onValueChanged.AddListener(soundManager.ChangeSFXVolume);
        this.gameObject.GetComponent<Slider>().value = soundManager.GetSFXVolume();
    }

}
