using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour {

    /************************ FIELDS ************************/
    [SerializeField] private BuildingConstruction buildingConstruction;
    private Image constructionProgressImage;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        constructionProgressImage = transform.Find("mask").Find("image").GetComponent<Image>();
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }

    /************************ METHODS ************************/
}
