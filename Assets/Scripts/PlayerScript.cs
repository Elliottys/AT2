using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // todo: lerp towards gravityDirection by direction amount that increases/decreases?

    public GameObject redMesh;
    public GameObject bluMesh;

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
            UpdateMesh();
        }
    }

    public void Start()
    {
        body = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        if (gravityInverted)
        {
            redMesh.SetActive(false);
            bluMesh.SetActive(true);
        }
        else
        {
            redMesh.SetActive(true);
            bluMesh.SetActive(false);
        }
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


