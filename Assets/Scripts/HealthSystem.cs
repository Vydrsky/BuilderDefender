using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour {

    /************************ FIELDS ************************/
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    private int healthAmount;
    [SerializeField]
    private int healthAmountMax;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        healthAmount = healthAmountMax;
        
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/

    public void Damage(int damageAmount) {
        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount,0,healthAmountMax);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (isDead()) {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool isDead() {
        return healthAmount == 0;
    }

    public bool isFullHealth() {
        return healthAmount == healthAmountMax;
    }

    public int GetHealthAmount() {
        return healthAmount;
    }
    public float GetHealthAmountNormalized() {
        return (float)healthAmount/healthAmountMax;
    }
    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount) {
        this.healthAmountMax = healthAmountMax;

        if (updateHealthAmount) {
            healthAmount = healthAmountMax;
        }
    }
}
