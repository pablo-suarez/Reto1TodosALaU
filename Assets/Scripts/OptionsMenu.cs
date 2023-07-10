using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void ChangeVolume(float volume){
        audioMixer.SetFloat("GameVolume", volume);
    } 
}
