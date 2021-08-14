using UnityEngine;

public class BuildingConstruction : MonoBehaviour {

    /************************ FIELDS ************************/

    private float constructionTimer;
    private float constructionTimerMax;
    private BuildingTypeSO buildingType;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = spriteRenderer.material;
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        constructionTimer -= Time.deltaTime;

        constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
        if(constructionTimer <= 0f) {
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
            Destroy(gameObject);
        }
    }

    /************************ METHODS ************************/

    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType) {
        Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
        Transform buildingTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);
        return buildingConstruction;
    }

    private void SetBuildingType(BuildingTypeSO buildingType) {
        constructionTimerMax = buildingType.constructionTimerMax;
        this.buildingType = buildingType;
        constructionTimer = constructionTimerMax;

        spriteRenderer.sprite = buildingType.sprite;

        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        buildingTypeHolder.buildingType = buildingType;
    }

    public float GetConstructionTimerNormalized() {
        return 1 - constructionTimer / constructionTimerMax;
    }
}
