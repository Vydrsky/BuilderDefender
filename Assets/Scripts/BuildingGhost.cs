using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    /************************ FIELDS ************************/

    private GameObject spriteGameObject;
    private ResourceNearbyOverlay resourceNearbyOverlay;

    /************************ INITIALIZE ************************/
    private void Awake() {
        
        spriteGameObject = transform.Find("sprite").gameObject;
        resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();

        Hide();
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
    }

    

    /************************ LOOPING ************************/
    private void Update() {
        transform.position = Utilities.GetMouseWorldPosition();
    }

    /************************ METHODS ************************/

    private void Show(Sprite ghostSprite) {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }
    private void Hide() {
        spriteGameObject.SetActive(false);
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e) {
        if (e.activeBuildingType == null) {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else {
            Show(e.activeBuildingType.sprite);
            if(e.activeBuildingType.hasResourceGeneratorData)
                resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);
            else
                resourceNearbyOverlay.Hide();
        }
    }
}
