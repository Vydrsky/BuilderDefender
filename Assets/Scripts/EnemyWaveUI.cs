using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour {

    /************************ FIELDS ************************/

    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;
    private RectTransform waveDirectionIndicator;
    private RectTransform enemyClosestPositionIndicator;
    private Camera mainCamera;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        waveDirectionIndicator = transform.Find("enemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        enemyClosestPositionIndicator = transform.Find("enemyClosestPositionIndicator").GetComponent<RectTransform>();
    }

    private void Start() {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    

    /************************ LOOPING ************************/
    private void Update() {

        HandleNextWaveMessage();

        HandleEnemySpawnWavePositionIndicator();

        HandleClosestEnemyPositionIndicator();

    }

    /************************ METHODS ************************/

    private void SetMessageText(string message) {
        waveMessageText.SetText(message);
    }
    private void SetWaveNumberText(string message) {
        waveNumberText.SetText(message);
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e) {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void HandleClosestEnemyPositionIndicator() {
        float targetMaxRadius = 9999f;
        Enemy targetEnemy = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);
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
        if (targetEnemy != null) {
            Vector3 dirToClosestEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;
            enemyClosestPositionIndicator.anchoredPosition = dirToClosestEnemy * 300f;
            enemyClosestPositionIndicator.eulerAngles = new Vector3(0f, 0f, Utilities.GetAngleFromVector(dirToClosestEnemy));

            float distanceToClosestEnemy = Vector3.Distance(targetEnemy.transform.position, mainCamera.transform.position);
            enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
        }
        else {
            enemyClosestPositionIndicator.gameObject.SetActive(false);
        }
    }

    private void HandleEnemySpawnWavePositionIndicator() {
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        waveDirectionIndicator.anchoredPosition = dirToNextSpawnPosition * 350f;
        waveDirectionIndicator.eulerAngles = new Vector3(0f, 0f, Utilities.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToTheNextSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        waveDirectionIndicator.gameObject.SetActive(distanceToTheNextSpawnPosition > mainCamera.orthographicSize * 1.5f);
    }

    private void HandleNextWaveMessage() {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f) {
            SetMessageText("");
        }
        else {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }
}
