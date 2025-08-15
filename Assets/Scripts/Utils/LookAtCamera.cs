using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cameraTransform;

    [SerializeField]
    private bool invert;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (invert)
        {
            Vector3 direction = transform.position - cameraTransform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            transform.LookAt(cameraTransform);
        }
    }
}
