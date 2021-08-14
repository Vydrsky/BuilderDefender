using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour {

    /************************ FIELDS ************************/

    public static GameOverUI Instance { get; private set; }
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        Instance = this;
        transform.Find("retryButton").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });
        transform.Find("mainMenuButton").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
        Hide();
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/

    public void Show() {
        gameObject.SetActive(true);

        transform.Find("youSurvivedText").GetComponent<TextMeshProUGUI>().SetText("You Survived " + EnemyWaveManager.Instance.GetWaveNumber() + " Waves!");
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
