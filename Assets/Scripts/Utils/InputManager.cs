using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("InputManager have more than one instance!");
        }

        Instance = this;
    }

    public Vector3 GetMouseOnScreenPosition()
    {
        return Input.mousePosition;
    }

    public Vector2 GetCameraMoveDirection()
    {
        Vector2 cameraMoveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            cameraMoveDir.y += 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            cameraMoveDir.x -= 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            cameraMoveDir.y -= 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            cameraMoveDir.x += 1f;
        }

        return cameraMoveDir;
    }

    public float GetCameraRotateAmount()
    {
        float rotateAmount = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rotateAmount += 1f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            rotateAmount -= 1f;
        }

        return rotateAmount;
    }

    public float GetCameraZoomAmount()
    {
        return Input.mouseScrollDelta.y;
    }
}
