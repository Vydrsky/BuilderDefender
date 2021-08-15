using UnityEngine;
using UnityEngine.Rendering;

public class ChromaticAbberationEffect : MonoBehaviour {

    /************************ FIELDS ************************/
    public static ChromaticAbberationEffect Instance { get; private set; }

    private Volume volume;

    /************************ INITIALIZE ************************/
    private void Awake() {
        Instance = this;
        volume = GetComponent<Volume>();
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        if(volume.weight > 0) {
            float decreaseSpeed = 1f;
            volume.weight -= Time.deltaTime * decreaseSpeed;
        }
    }

    /************************ METHODS ************************/

    public void SetWeight(float weight) {
        volume.weight = weight;
    }
}
