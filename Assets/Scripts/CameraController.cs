using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera cinemachineCamera;

    private CinemachineFollow cinemachineFollow;

    private float targetZoomY;

    private const float MOVE_SPEED = 5f;
    private const float ZOOM_SPEED = 1f;
    private const float ROTATION_SPEED = 50f;
    private const float MAX_HEIGHT = 14f;
    private const float MIN_HEIGHT = 2f;

    private void Start()
    {
        cinemachineFollow = cinemachineCamera.GetComponent<CinemachineFollow>();
        targetZoomY = cinemachineFollow.FollowOffset.y;
    }

    private void Update()
    {
        MoveCamera();
        // RotateCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        Vector2 cameraMoveDir = InputManager.Instance.GetCameraMoveDirection();

        Vector3 moveVector =
            transform.forward * cameraMoveDir.y + transform.right * cameraMoveDir.x;

        transform.position += moveVector * MOVE_SPEED * Time.deltaTime;

        float maxMapArea = 42f;
        float minMapArea = 0f;

        // Limit the area camera can move
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minMapArea, maxMapArea);
        pos.z = Mathf.Clamp(pos.z, minMapArea, maxMapArea);
        transform.position = pos;
    }

    // private void RotateCamera()
    // {
    //     float rotateInput = InputManager.Instance.GetCameraRotateAmount();
    //
    //     // Rotate the whole CameraController rig around the Y axis
    //     if (Mathf.Abs(rotateInput) > 0.01f)
    //     {
    //         transform.Rotate(
    //             Vector3.up,
    //             rotateInput * ROTATION_SPEED * Time.deltaTime,
    //             Space.World
    //         );
    //     }
    // }

    private void ZoomCamera()
    {
        float scrollDelta = InputManager.Instance.GetCameraZoomAmount();

        if (Mathf.Abs(scrollDelta) > 0.01f)
        {
            targetZoomY -= scrollDelta * ZOOM_SPEED;
            targetZoomY = Mathf.Clamp(targetZoomY, MIN_HEIGHT, MAX_HEIGHT);
        }

        Vector3 offset = cinemachineFollow.FollowOffset;
        float cameraSpeed = 8f;
        offset.y = Mathf.Lerp(offset.y, targetZoomY, Time.deltaTime * cameraSpeed);

        cinemachineFollow.FollowOffset = offset;
    }
}
