using UnityEngine;

public class Building : MonoBehaviour {

    /************************ FIELDS ************************/
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;
    private Transform buildingDemolishButton;

    /************************ INITIALIZE ************************/
    private void Awake() {
        buildingDemolishButton = transform.Find("pfBuildingDemolishUI");
        HideBuildingDemolishButton();
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

    private void OnMouseEnter() {
        ShowBuildingDemolishButton();
    }

    private void OnMouseExit() {
        HideBuildingDemolishButton();
    }

    private void ShowBuildingDemolishButton() {
        if (buildingDemolishButton != null)
            buildingDemolishButton.gameObject.SetActive(true);
    }

    private void HideBuildingDemolishButton() {
        if (buildingDemolishButton != null)
            buildingDemolishButton.gameObject.SetActive(false);
    }
}
