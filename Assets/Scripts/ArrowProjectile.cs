using UnityEngine;

public class ArrowProjectile : MonoBehaviour {

    /************************ FIELDS ************************/
    private Enemy targetEnemy;
    private Vector3 lastMoveDir;
    private float lifespan = 2f;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        Vector3 moveDir;

        if (targetEnemy != null) {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else {
            moveDir = lastMoveDir;
        }
        float moveSpeed = 20f;
        transform.position += moveDir * Time.deltaTime * moveSpeed;
        transform.eulerAngles = new Vector3(0, 0, Utilities.GetAngleFromVector(moveDir));
        lifespan -= Time.deltaTime;
        if(lifespan <= 0f) {
            Destroy(gameObject);
        }
    }

    /************************ METHODS ************************/

    private void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null) {
            Destroy(gameObject);
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
        }
    }

    private void SetTarget(Enemy targetEnemy) {
        this.targetEnemy = targetEnemy;
    }

    public static ArrowProjectile Create(Vector3 position, Enemy enemy) {
        Transform pfArrowProjectile = GameAssets.Instance.pfArrowProjectile;
        Transform arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

        ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);
        return arrowProjectile;
    }
}
