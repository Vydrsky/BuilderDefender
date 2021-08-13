using UnityEngine;

public class Tower : MonoBehaviour {

    /************************ FIELDS ************************/
    private Enemy targetEnemy;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private Vector3 projectileSpawnPosition;
    private float shootTimer;
    [SerializeField] private float shootTimerMax;

    /************************ INITIALIZE ************************/
    private void Awake() {
        projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        HandleTargeting();
        HandleShooting();
    }

    /************************ METHODS ************************/
    private void LookForTargets() {
        float targetMaxRadius = 20f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        foreach (var collider in colliders) {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null) {
                if (targetEnemy == null) {
                    targetEnemy = enemy;
                }
                else {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position)) {
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }

    private void HandleTargeting() {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer <= 0 && BuildingManager.Instance.GetHQBuilding() != null) {
            LookForTargets();
            lookForTargetTimer += lookForTargetTimerMax;
        }
    }

    private void HandleShooting() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f) {
            if (targetEnemy != null)
                ArrowProjectile.Create(projectileSpawnPosition, targetEnemy);
            shootTimer += shootTimerMax;
        }
    }
}
