using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Transform mesh;
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

    [SerializeField] private bool onGround;

    [SerializeField] private enum MagnetTypes { red, blu };
    [SerializeField] private MagnetTypes magnetType;

    [SerializeField] private float acceleration = 0.0f;
    [SerializeField] private float gravityInvertion = 1.0f;


    [SerializeField] private Rigidbody body;
    [SerializeField] private Camera mainCamera;

    public void ButtonInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SetMagneticState(GetOppositePole(magnetType), true);
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

    private void SetMagneticState(MagnetTypes newType, bool flipGravity)
    {
        magnetType = newType;
        onGround = false;
        acceleration = 1.0f;
        if (flipGravity)
        {
            gravityInvertion *= -1.0f;
        }
        UpdateMesh();
    }

    private bool CheckIfGrounded()
    {
        Debug.DrawRay(transform.position, (gravityDirection * groundMaxDistance) * gravityInvertion, Color.yellow);

        if (Physics.Raycast(transform.position, gravityDirection * gravityInvertion, out RaycastHit hit, groundMaxDistance, collisionDetectionLayer))
        {
            // Our raycast detected a floor within acceptable distance, become grounded and snap to floor if we are still airborne
            transform.position = hit.point + (Vector3.up * gravityInvertion) * groundTouching;
            if (!onGround)
            {
                onGround = true;
            }
        }
        else
        {
            if (onGround)
            {
                onGround = false;
            }
        }

        return onGround;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        body = GetComponent<Rigidbody>();

        SetMagneticState(MagnetTypes.blu, false);
    }

    private void Update()
    {
        // First we update our speed and move direction
        body.velocity = moveDirection * speed;

        float rotationAmount = (body.velocity.magnitude * 33.333f * (acceleration + 1.0f)) * gravityInvertion;

        mesh.RotateAround(transform.position, Vector3.back, rotationAmount * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (CheckIfGrounded())
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
