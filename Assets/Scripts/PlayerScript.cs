using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // todo: lerp towards gravityDirection by direction amount that increases/decreases?

    public float cameraOffsetX;
    public float cameraOffsetY;
    public float speed;
    public Vector3 moveDirection;
    public Vector3 gravityDirection;
    public bool gravityInverted;

    private Rigidbody body;
    private Camera mainCamera;

    public void ButtonInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            gravityInverted = !gravityInverted;
        }
    }

    public void Start()
    {
        body = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    public void Update()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, cameraOffsetX, cameraOffsetY);
        body.velocity = moveDirection * speed;
        if (gravityInverted)
        {
            body.velocity -= gravityDirection;
        }
        else
        {
            body.velocity += gravityDirection;
        }
    }
}


