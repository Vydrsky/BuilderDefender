using UnityEngine;
using System.Collections.Generic;
using System;
public class ResourceManager : MonoBehaviour {

    /************************ FIELDS ************************/

    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    public event EventHandler OnResourceAmmountChanged;
    [SerializeField] private List<ResourceAmount> startingResourceAmountList;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach(ResourceTypeSO resourceType in resourceTypeList.list) {
            resourceAmountDictionary[resourceType] = 0;
        }
        foreach (ResourceAmount resourceAmount in startingResourceAmountList) {
            AddResource(resourceAmount.resourceType,resourceAmount.amount);
        }
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/


    public void AddResource(ResourceTypeSO resourceType, int amount) {
        if(resourceType != null)
            resourceAmountDictionary[resourceType] += amount;

        OnResourceAmmountChanged?.Invoke(this,EventArgs.Empty);

        //Debug.Log(resourceAmountDictionary[resourceType]);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType) {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray) {
        foreach (var resourceAmount in resourceAmountArray) {
            if(!(GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)) {
                return false;
            }
        }
        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmountArray) {
        foreach (var resourceAmount in resourceAmountArray) {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
       
    }
}
