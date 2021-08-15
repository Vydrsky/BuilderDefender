using UnityEngine;

public class Building : MonoBehaviour {

    /************************ FIELDS ************************/
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;
    private Transform buildingDemolishButton;
    private Transform buildingRepairButton;

    /************************ INITIALIZE ************************/
    private void Awake() {
        buildingDemolishButton = transform.Find("pfBuildingDemolishUI");
        buildingRepairButton = transform.Find("pfBuildingRepairUI");
        HideBuildingDemolishButton();
        HideBuildingRepairButton();
    }

    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax,true);
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnDamaged += HealthSystem_OnHealed;
    }

    

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/

    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        Instantiate(Resources.Load<Transform>("pfBuildingDestroyedParticles"), transform.position, Quaternion.identity);
        CinemachineShake.Instance.ShakeCamera(10f, 0.2f);
        Destroy(gameObject);
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        ShowBuildingRepairButton();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
        CinemachineShake.Instance.ShakeCamera(7f, 0.15f);
        ChromaticAbberationEffect.Instance.SetWeight(1f);
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e) {
        if (healthSystem.isFullHealth()) {
            HideBuildingRepairButton();
        }
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

    private void ShowBuildingRepairButton() {
        if (buildingRepairButton != null)
            buildingRepairButton.gameObject.SetActive(true);
    }

    private void HideBuildingRepairButton() {
        if (buildingRepairButton != null)
            buildingRepairButton.gameObject.SetActive(false);
    }
}
