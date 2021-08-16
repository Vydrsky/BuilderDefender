using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour {

    /************************ FIELDS ************************/

    public static BuildingManager Instance { get; private set; }

    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;
    private Camera mainCamera;

    public event EventHandler<OnActiveBuildingTypeChangeEventArgs> OnActiveBuildingTypeChange;

    public class OnActiveBuildingTypeChangeEventArgs : EventArgs {
        public BuildingTypeSO activeBuildingType;
    }

    [SerializeField] private Building hqBuilding;

    /************************ INITIALIZE ************************/
    private void Awake() {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        
    }

    private void Start() {
        mainCamera = Camera.main;
        hqBuilding.gameObject.GetComponent<HealthSystem>().OnDied += HQ_OnDied;
    }

    

    /************************ LOOPING ************************/
    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (activeBuildingType != null){
                if (CanSpawnBuilding(activeBuildingType, Utilities.GetMouseWorldPosition(), out string errorMessage)) {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray)) {
                        ResourceManager.Instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                        //Instantiate(activeBuildingType.prefab, Utilities.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstruction.Create(Utilities.GetMouseWorldPosition(),activeBuildingType);
                        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                    }
                    else {
                        TooltipUI.Instance.Show("Cannot Afford " + activeBuildingType.GetConstructionResourceCostString(), new TooltipUI.TooltipTimer { timer = 2f });
                    }
                }
                else {
                    TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f });
                }
            }
        }
        
    }

    /************************ METHODS ************************/

    private void HQ_OnDied(object sender, EventArgs e) {
        GameOverUI.Instance.Show();
        SoundManager.Instance.PlaySound(SoundManager.Sound.GameOver);
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType) {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChange?.Invoke(this,new OnActiveBuildingTypeChangeEventArgs { activeBuildingType = this.activeBuildingType});
    }

    public BuildingTypeSO GetActiveBuildingType() {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType,Vector3 position, out string errorMessage) {
        BoxCollider2D boxCollider = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] colliders = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider.offset,boxCollider.size,0);

        bool isAreaClear = colliders.Length == 0;
        if (!isAreaClear) {
            errorMessage = "Area is not clear!";
            return false;
        }

        colliders = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider in colliders) {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null) {
                if(buildingTypeHolder.buildingType == buildingType) {
                    errorMessage = "Too close to another building of the same type";
                    return false;
                }
            }
        }
        float maxConstructionRadius = 25f;
        colliders = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider in colliders) {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                errorMessage = null;
                return true;
            }
        }
        errorMessage = "Too far from any other building";
        return false;
    }

    public Building GetHQBuilding() {
        return hqBuilding;
    }
}
