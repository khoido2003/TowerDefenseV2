using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance { get; private set; }

    private Camera camera;

    [SerializeField]
    public LayerMask groundlayerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Mouse World Instance in the scene!");
        }
        Instance = this;

        camera = Camera.main;
    }

    public Vector3 GetWorldPosition()
    {
        Ray ray = camera.ScreenPointToRay(InputManager.Instance.GetMouseOnScreenPosition());

        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, groundlayerMask);

        return raycastHit.point;
    }
}
