using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    /************************ FIELDS ************************/
    public static SoundManager Instance;
    public enum Sound {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver,
    }

    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> audioClipDictionary;
    
    /************************ INITIALIZE ************************/
    private void Awake() { 
        
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioClipDictionary = new Dictionary<Sound, AudioClip>();
        foreach (Sound sound in System.Enum.GetValues(typeof(Sound))) {
            audioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/

    public void PlaySound(Sound sound) {
        audioSource.PlayOneShot(audioClipDictionary[sound]);
    }
}
