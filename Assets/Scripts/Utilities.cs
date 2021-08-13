using UnityEngine;

public static class Utilities {

    /************************ FIELDS ************************/

    private static Camera mainCamera;

    /************************ METHODS ************************/

    public static Vector3 GetMouseWorldPosition() {
        if (mainCamera == null)
            mainCamera = Camera.main;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }

    public static Vector3 GetRandomDirection() {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector) {
        float angle = Mathf.Atan2(vector.y,vector.x) * Mathf.Rad2Deg;
        return angle;
    }
}
