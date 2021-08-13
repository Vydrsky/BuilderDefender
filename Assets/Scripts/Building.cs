using UnityEngine;

public class Building : MonoBehaviour {

    /************************ FIELDS ************************/
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;

    /************************ INITIALIZE ************************/
    private void Awake() {
        
    }

    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax,true);
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/

    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }
}
