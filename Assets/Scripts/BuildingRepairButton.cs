using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour {

    /************************ FIELDS ************************/
    private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;
    private int repairCost;

    /************************ INITIALIZE ************************/
    private void Awake() {
        healthSystem = transform.GetComponentInParent<HealthSystem>();
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
            int missingHealth = healthSystem.HealthAmountMax - healthSystem.HealthAmount;
            repairCost = missingHealth / 2;

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

        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }



    /************************ LOOPING ************************/


    /************************ METHODS ************************/

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        int missingHealth = healthSystem.HealthAmountMax - healthSystem.HealthAmount;
        repairCost = missingHealth / 2;
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e) {
        int missingHealth = healthSystem.HealthAmountMax - healthSystem.HealthAmount;
        repairCost = missingHealth / 2;
    }

    private void OnMouseOver() {
        TooltipUI.Instance.Show("Repair Cost: <color=#"+ goldResourceType.colorHex +">" + repairCost + "G</color>");
    }

    private void OnMouseExit() {
        TooltipUI.Instance.Hide();
    }
}
