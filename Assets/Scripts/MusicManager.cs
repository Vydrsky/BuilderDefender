using UnityEngine;

public class MusicManager : MonoBehaviour {

    /************************ FIELDS ************************/
    public static MusicManager Instance;

    private float volume = 0.5f;
    private AudioSource audioSource;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        Instance = this;


        volume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/

    public void IncreaseVolume(float amount) {
        volume += amount;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void DecreaseVolume(float amount) {
        volume -= amount;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public float GetVolume() {
        return volume;
    }
}
