using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    /************************ FIELDS ************************/
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        transform.Find("playButton").GetComponent<Button>().onClick.AddListener(() => {
            Time.timeScale = 1f;
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });
        transform.Find("quitButton").GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
            Debug.Log("game quitting");
        });
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/
}
