using UnityEngine;

public class HealthBar : MonoBehaviour {

    /************************ FIELDS ************************/

    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;
    private Transform separatorContainer;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        barTransform = transform.Find("bar");
        separatorContainer = transform.Find("separatorContainer");
    }

    private void Start() {
        ConstructHealthBarSeparators();

        UpdateBarVisible();
        healthSystem.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    

    /************************ LOOPING ************************/
    

    /************************ METHODS ************************/

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        UpdateBarVisible();
        UpdateBar();
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e) {
        UpdateBarVisible();
        UpdateBar();
    }

    private void HealthSystem_OnHealthAmountMaxChanged(object sender, System.EventArgs e) {
        ConstructHealthBarSeparators();
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

    private void ConstructHealthBarSeparators() {
        Transform separatorTemplate = separatorContainer.Find("separatorTemplate");
        separatorTemplate.gameObject.SetActive(false);

        foreach (Transform separatorTransform in separatorContainer) {
            if (separatorTransform == separatorTemplate) continue;
            Destroy(separatorTransform.gameObject);
        }

        int healthAmountPerSeparator = 10;
        float barSize = 4.2f;
        float barOneHealthAmountSize = barSize / healthSystem.HealthAmountMax;
        int healthSeparatorCount = Mathf.FloorToInt(healthSystem.HealthAmountMax / 10);

        for (int i = 1; i < healthSeparatorCount; i++) {
            Transform separatorTransform = Instantiate(separatorTemplate, separatorContainer);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeparator, 0f, 0f);
        }
    }
}
