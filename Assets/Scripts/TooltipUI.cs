using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour {

    /************************ FIELDS ************************/
    public static TooltipUI Instance { get; private set; }
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    [SerializeField]
    private RectTransform canvasRectTransform;
    private TooltipTimer tooltipTimer;

    /************************ INITIALIZE ************************/
    private void Awake() {
        Instance = this;
        Hide();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        HandleFollowMouse();

        if (tooltipTimer != null) {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer <= 0f) {
                Hide();
            }
        }
    }



    /************************ METHODS ************************/

    private void HandleFollowMouse() {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        Debug.Log(rectTransform.anchoredPosition);
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width) {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height) {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText) {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string tooltipText,TooltipTimer tooltipTimer = null) {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(tooltipText);
        HandleFollowMouse();
    }
    public void Hide() {
        gameObject.SetActive(false);
        
    }

    public class TooltipTimer {
        public float timer;
    }
}
