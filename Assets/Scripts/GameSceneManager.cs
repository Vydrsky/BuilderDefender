using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager {

    /************************ FIELDS ************************/
    
    public enum Scene {
        GameScene,
        MainMenuScene
    }

    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    /************************ METHODS ************************/
}
