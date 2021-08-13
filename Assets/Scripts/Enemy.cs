using UnityEngine;

public class Enemy : MonoBehaviour {

    /************************ FIELDS ************************/
    private Transform targetTransform;
    private Rigidbody2D rb;
    private HealthSystem healthSystem;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;

        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }

    

    /************************ LOOPING ************************/
    private void Update() {
        HandleMovement();
        HandleTargeting();
    }



    /************************ METHODS ************************/


    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Building building = collision.gameObject.GetComponent<Building>();
        if(building != null) {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }

    public static Enemy Create(Vector3 position) {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemy,position,Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private void HandleTargeting() {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer <= 0 && BuildingManager.Instance.GetHQBuilding() != null) {
            LookForTargets();
            lookForTargetTimer += lookForTargetTimerMax;
        }
    }

    private void HandleMovement() {
        if (targetTransform != null) {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            float moveSpeed = 6f;
            rb.velocity = moveDir * moveSpeed;
        }
        else {
            rb.velocity = Vector2.zero;
        }
    }

    private void LookForTargets() {
        float targetMaxRadius = 10f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,targetMaxRadius);
        foreach (var collider in colliders) {
            Building building = collider.GetComponent<Building>();
            if(building != null) {
                if (targetTransform == null) {
                    targetTransform = building.transform;
                }
                else {
                    if(Vector3.Distance(transform.position,building.transform.position) < Vector3.Distance(transform.position, targetTransform.position)) {
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if(targetTransform == null) {
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
    }
}
