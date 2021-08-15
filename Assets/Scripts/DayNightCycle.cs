using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour {

    /************************ FIELDS ************************/
    [SerializeField] private Gradient gradient;
    [SerializeField] private float secondsPerDay = 10f;
    private Light2D light2d;
    private float dayTime;
    private float dayTimeSpeed;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        light2d = GetComponent<Light2D>();
        dayTimeSpeed = 1 / secondsPerDay;
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        dayTime += Time.deltaTime * dayTimeSpeed;
        light2d.color = gradient.Evaluate(dayTime % 1f);
    }

    /************************ METHODS ************************/
}
