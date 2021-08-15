using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour {

    /************************ FIELDS ************************/
    public static CameraHandler Instance { get; private set; }
    public bool EdgeScrolling { 
        get => edgeScrolling; 
        set { 
            edgeScrolling = value;
            PlayerPrefs.SetInt("edgeScrolling", edgeScrolling ? 1 : 0);
        } 
    }

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private PolygonCollider2D mapCollider;
    private float orthographicSize;
    private float targetOrthographicSize;
    private bool edgeScrolling;

    /************************ INITIALIZE ************************/
    private void Awake() {
        Instance = this;

        edgeScrolling = PlayerPrefs.GetInt("edgeScrolling",1) == 1;
    }

    private void Start() {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    /************************ LOOPING ************************/
    private void Update() {
        HandleMovement();
        HandleZoom();

        Debug.Log(mapCollider.bounds.min.x + orthographicSize / 2);

        
    }

    /************************ METHODS ************************/

    private void HandleMovement() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (EdgeScrolling) {
            float edgeScrollingSize = 30;
            if (Input.mousePosition.x > Screen.width - edgeScrollingSize) {
                x = 1f;
            }
            if (Input.mousePosition.x < edgeScrollingSize) {
                x = -1f;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrollingSize) {
                y = 1f;
            }
            if (Input.mousePosition.y < edgeScrollingSize) {
                y = -1f;
            }
        }

        Vector3 moveDir = new Vector3(x, y, 0f).normalized;

        float moveSpeed = 30f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        

        float xClamp = Mathf.Clamp(transform.position.x, mapCollider.bounds.min.x + orthographicSize * cinemachineVirtualCamera.m_Lens.Aspect, mapCollider.bounds.max.x - orthographicSize * cinemachineVirtualCamera.m_Lens.Aspect);
        float yClamp = Mathf.Clamp(transform.position.y, mapCollider.bounds.min.y + orthographicSize, mapCollider.bounds.max.y - orthographicSize);
        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }

    private void HandleZoom() {
        float zoomAmount = 2f;
        targetOrthographicSize -= Input.mouseScrollDelta.y * zoomAmount;

        float minOrthographicSize = 10;
        float maxOrthographicSize = 30;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    
}
