using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuUI : MonoBehaviour {

    /************************ FIELDS ************************/

    private TextMeshProUGUI soundVolumeText;
    private TextMeshProUGUI musicVolumeText;
    
    
    /************************ INITIALIZE ************************/
    private void Awake() {

        soundVolumeText = transform.Find("soundVolumeText").GetComponent<TextMeshProUGUI>();
        musicVolumeText = transform.Find("musicVolumeText").GetComponent<TextMeshProUGUI>();

        transform.Find("soundUpButton").GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.Instance.IncreaseVolume(0.1f);
            UpdateText();
        });
        transform.Find("soundDownButton").GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.Instance.DecreaseVolume(0.1f);
            UpdateText();
        });
        transform.Find("musicUpButton").GetComponent<Button>().onClick.AddListener(() => {
            MusicManager.Instance.IncreaseVolume(0.1f);
            UpdateText();
        });
        
        transform.Find("musicDownButton").GetComponent<Button>().onClick.AddListener(() => {
            MusicManager.Instance.DecreaseVolume(0.1f);
            UpdateText();
        });

        transform.Find("mainMenuButton").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        transform.Find("edgeScrollingToggle").GetComponent<Toggle>().onValueChanged.AddListener((bool set) => {
            CameraHandler.Instance.EdgeScrolling = set;
        });

        
    }

    private void Start() {
        gameObject.SetActive(false);
        UpdateText();

        transform.Find("edgeScrollingToggle").GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.Instance.EdgeScrolling);
    }

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/

    private void UpdateText() {
        soundVolumeText.SetText(Mathf.FloorToInt(SoundManager.Instance.GetVolume()*10).ToString());
        musicVolumeText.SetText(Mathf.FloorToInt(MusicManager.Instance.GetVolume()*10).ToString());
    }

    public void ToggleVisible() {
        gameObject.SetActive(!gameObject.activeSelf);
        ToggleTimeScale();
    }

    private void ToggleTimeScale() {
        if (gameObject.activeSelf) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }

    
}
