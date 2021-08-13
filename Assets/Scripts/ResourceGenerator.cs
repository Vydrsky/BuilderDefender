using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    /************************ FIELDS ************************/
    private float timer;
    private float timerMax;
    private ResourceGeneratorData resourceGeneratorData;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
        int nearbyResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        
        float resourcePerSecond = ((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * (1f / timerMax)) + (1f / timerMax);
        timerMax = 1 / resourcePerSecond;
    }

    private void Start() {
        
    }


    /************************ LOOPING ************************/
    private void Update() {

        timer -= Time.deltaTime;
        if (timer <= 0f) {
            timer += timerMax;
            //Debug.Log("Ding " + buildingType.resourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }

    /************************ METHODS ************************/

    public ResourceGeneratorData GetResourceGeneratorData() {
        return resourceGeneratorData;
    }

    public float GetTimerNormalized() {
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSecond() {
        return 1 / timerMax;
    }

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData,Vector3 position) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider in colliders) {
            ResourceNode resourceNode = collider.gameObject.GetComponent<ResourceNode>();
            if (resourceNode != null) {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType) {
                    nearbyResourceAmount++;
                }
            }
        }
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }
}
