using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Renderer material;

    public LayerMask collisionDetectionLayer;

    public Vector3 moveDirection;
    public Vector3 gravityDirection;

    public float speed;

    public float initialAcceleration;
    public float increasingAcceleration;
    public float maxAcceleration;

    public float groundTouching;
    public float groundMaxDistance;

    private bool onGround;

    private enum MagnetTypes { red, blu };
    private MagnetTypes magnetType;

    private float acceleration = 0.0f;
    private float gravityInvertion = 1.0f;


    private Rigidbody body;
    private Camera mainCamera;

    public void ButtonInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SetMagneticState(GetOppositePole(magnetType));
        }
    }

    private MagnetTypes GetOppositePole(MagnetTypes type)
    {
        if (type == MagnetTypes.red)
        {
            return MagnetTypes.blu;
        }
        else
        {
            return MagnetTypes.red;
        }
    }

    private void UpdateMesh()
    {
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        switch (magnetType)
        {
            case MagnetTypes.red:
                {
                    propertyBlock.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f));
                    break;
                }
            case MagnetTypes.blu:
                {
                    propertyBlock.SetColor("_Color", new Color(0.0f, 0.0f, 1.0f));
                    break;
                }
            default:
                {
                    Debug.LogError("Invalid magnet type");
                    break;
                }
        }
        material.SetPropertyBlock(propertyBlock);
    }

    private void SetMagneticState(MagnetTypes newType)
    {
        magnetType = newType;
        onGround = false;
        acceleration = 1.0f;
        gravityInvertion *= -1.0f;
        UpdateMesh();
    }

    private void CheckForGround()
    {
        Debug.DrawRay(transform.position, (gravityDirection * groundMaxDistance) * gravityInvertion, Color.yellow);

        if (Physics.Raycast(transform.position, gravityDirection * gravityInvertion, out RaycastHit hit, groundMaxDistance, collisionDetectionLayer))
        {
            // Our raycast detected a floor within acceptable distance, become grounded and snap to floor if we are still airborne
            if (!onGround)
            {
                onGround = true;
            }
            transform.position = hit.point + (Vector3.up * gravityInvertion) * groundTouching;
        }
        else
        {
            if (onGround)
            {
                onGround = false;
            }
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        body = GetComponent<Rigidbody>();

        SetMagneticState(MagnetTypes.red);
    }

    private void FixedUpdate()
    {
        // First we update our speed and move direction
        body.velocity = moveDirection * speed;

        CheckForGround();

        if (onGround)
        {
            acceleration = 0.0f;
        }
        else
        {
            acceleration = Mathf.Clamp(acceleration + increasingAcceleration, 0, maxAcceleration);

            body.velocity += (gravityDirection * acceleration) * gravityInvertion;
        }
    }
}
