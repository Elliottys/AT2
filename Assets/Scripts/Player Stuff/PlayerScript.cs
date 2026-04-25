using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Transform mesh;
    public Renderer material;

    public LayerMask collisionDetectionLayer;

    public Vector3 defaultMoveDirection;
    public Vector3 defaultGravityDirection;
    private Vector3 moveDirection;
    private Vector3 gravityDirection;

    public float speed;

    public float initialAcceleration;
    public float increasingAcceleration;
    public float maxAcceleration;

    public float groundTouching;
    public float groundMaxDistance;
    public enum MagnetTypes { red, blu };
    public MagnetTypes defaultMagnet;

    public AudioSource audWin;
    public AudioSource audDie;
    public AudioSource audUp;
    public AudioSource audDn;
    public AudioSource audLand;

    private MagnetTypes magnetType;
    private Vector3 spawnPosition;
    private bool onGround;
    private float acceleration = 0.0f;
    private float gravityInvertion = 1.0f;

    private int winning = 0;

    private Rigidbody body;
    private Camera mainCamera;

    public void ButtonInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (GetWin() == 0)
            {
                SetMagneticState(GetOppositePole(magnetType), true);
            }
        }
    }

    public void Spawn(Vector3 position)
    {
        spawnPosition = position + Vector3.up;
        Respawn();
    }
    
    public void Respawn()
    {
        if (GetWin() < 2)
        {
            audDie.Play();
        }
        SetWinState(0);
        transform.position = spawnPosition;
        moveDirection = defaultMoveDirection;
        gravityDirection = defaultGravityDirection;
        gravityInvertion = 1.0f;
        SetMagneticState(defaultMagnet, false);
    }
    
    public void SetWinState(int value)
    {
        winning = value;
        if (value == 1)
        {
            audWin.Play();
            moveDirection = moveDirection / 2;
            onGround = false;
        }
    }
    
    public int GetWin()
    {
        return winning;
    }
    
    private MagnetTypes GetOppositePole(MagnetTypes type)
    {
        if (type == MagnetTypes.red)
        {
            audUp.Play();
            return MagnetTypes.blu;
        }
        else
        {
            audDn.Play();
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
                audLand.Play();
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
