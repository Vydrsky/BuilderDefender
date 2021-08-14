using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour {

    /************************ FIELDS ************************/
    private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        healthSystem = transform.GetComponentInParent<HealthSystem>();
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
            int missingHealth = healthSystem.HealthAmountMax - healthSystem.HealthAmount;
            int repairCost = missingHealth / 2;

            ResourceAmount[] resourceAmountCost = new ResourceAmount[] {
                new ResourceAmount { resourceType = goldResourceType,amount = repairCost}
            };
            if(ResourceManager.Instance.CanAfford(resourceAmountCost)){
                //Can afford repairs
                ResourceManager.Instance.SpendResources(resourceAmountCost);
                healthSystem.HealFull();
            }
            else {
                //cannot afford
                TooltipUI.Instance.Show("Cannot afford repair cost!", new TooltipUI.TooltipTimer { timer = 2f});
            }
            
        });
    }

    /************************ LOOPING ************************/
    

    /************************ METHODS ************************/
}
