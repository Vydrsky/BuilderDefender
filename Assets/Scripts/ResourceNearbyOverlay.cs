using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour {

    /************************ FIELDS ************************/

    private ResourceGeneratorData resourceGeneratorData;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        Hide();
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / (float)resourceGeneratorData.maxResourceAmount * 100f);
        transform.Find("text").GetComponent<TextMeshPro>().SetText("+ " + percent + "%");
    }

    /************************ METHODS ************************/

    public void Show(ResourceGeneratorData resourceGeneratorData) {
        this.resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
