using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // todo: lerp towards gravityDirection by direction amount that increases/decreases?

    public Vector3 moveDirection;
    public Vector3 gravityDirection;
    public float speed;
    public bool gravityInverted;

    private Rigidbody body;

    public void ButtonInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            gravityInverted = true;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            gravityInverted = false;
        }
    }

    public void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Update()
    {
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


