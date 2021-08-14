using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour {

    /************************ FIELDS ************************/
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    public event EventHandler OnHealed;
    public event EventHandler OnHealthAmountMaxChanged;
    private int healthAmount;
    [SerializeField]
    private int healthAmountMax;

    public int HealthAmount => healthAmount;
    public int HealthAmountMax => healthAmountMax;

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

    public void Heal(int healAmount) {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull() {
        healthAmount = healthAmountMax;

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public bool isDead() {
        return healthAmount == 0;
    }

    public bool isFullHealth() {
        return healthAmount == healthAmountMax;
    }

    
    public float GetHealthAmountNormalized() {
        return (float)healthAmount/healthAmountMax;
    }
    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount) {
        this.healthAmountMax = healthAmountMax;

        if (updateHealthAmount) {
            healthAmount = healthAmountMax;
        }
        OnHealthAmountMaxChanged?.Invoke(this, EventArgs.Empty);
    }
}
