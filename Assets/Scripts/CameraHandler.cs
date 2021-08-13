using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour {

    /************************ FIELDS ************************/
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float orthographicSize;
    private float targetOrthographicSize;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        
    }

    private void Start() {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    /************************ LOOPING ************************/
    private void Update() {
        HandleMovement();
        HandleZoom();
    }

    /************************ METHODS ************************/

    private void HandleMovement() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, y, 0f).normalized;

        float moveSpeed = 30f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
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
