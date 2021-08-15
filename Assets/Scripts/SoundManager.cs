using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    /************************ FIELDS ************************/
    public static SoundManager Instance;

    private float volume = 0.5f;
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

        volume = PlayerPrefs.GetFloat("soundVolume", 0.5f);
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
        audioSource.PlayOneShot(audioClipDictionary[sound],volume);
    }

    public void IncreaseVolume(float amount) {
        volume += amount;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public void DecreaseVolume(float amount) {
        volume -= amount;
        if (volume < 0.05f)
            volume = 0;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public float GetVolume() {
        return volume;
    }
}
