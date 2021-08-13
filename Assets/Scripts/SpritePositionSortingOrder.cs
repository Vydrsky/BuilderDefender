using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour {

    /************************ FIELDS ************************/
    [SerializeField] private bool runOnce;
    [SerializeField] private float positionOffsetY;
    private SpriteRenderer spriteRenderer;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    private void LateUpdate() {
        float precisionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * precisionMultiplier);
        if (runOnce) {
            Destroy(this);
        }
    }
    /************************ METHODS ************************/

}
