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
}
