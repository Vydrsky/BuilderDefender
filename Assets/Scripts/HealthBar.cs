using UnityEngine;

public class HealthBar : MonoBehaviour {

    /************************ FIELDS ************************/

    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        barTransform = transform.Find("bar");
    }

    private void Start() {
        UpdateBarVisible();
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
    }

    

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        UpdateBarVisible();
        UpdateBar();
    }
    private void UpdateBar() {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateBarVisible() {
        if (healthSystem.isFullHealth()) {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
        }
    }
}
