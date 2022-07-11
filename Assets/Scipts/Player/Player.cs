
using UnityEngine;

public class Player : SingletonBehaviour<Player>
{
    // Start is called before the first frame update
    private float xInput;
    private float yInput;
    private bool isWalking;
    private bool isRunning;
    private ToolEffect toolEffect;
    private bool isCarrying;
    private bool isUsingToolRight;
    private bool isUsingToolLeft;
    private bool isUsingToolUp;
    private bool isUsingToolDown;
    private bool isLiftingToolRight;
    private bool isLiftingToolLeft;
    private bool isLiftingToolUp;
    private bool isLiftingToolDown;
    private bool isSwingingToolRight;
    private bool isSwingingToolLeft;
    private bool isSwingingToolUp;
    private bool isSwingingToolDown;
    private bool isPickingRight;
    private bool isPickingLeft;
    private bool isPickingUp;
    private bool isPickingDown;
    private bool isIdle;

    public Rigidbody2D rigidbody2D;

    private Direction PlayerDirection;

    private float movementSpeed;

    private Camera camera;
    public bool _playerInputIsDisable = false;
    
    public bool PlayerInputIsDisable
    {
        get => _playerInputIsDisable;
        set => _playerInputIsDisable = value;
    }

    protected override void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (!PlayerInputIsDisable)
        {
            ResetTrigger();
            PlayerInputMovement();
            PlayerWalkingInput();
        
            EventHandler.CallMovementEvent( xInput, yInput, isWalking, isRunning, isIdle, isCarrying, toolEffect,isUsingToolRight,
                isUsingToolLeft,
                isUsingToolUp,
                isUsingToolDown,
                isLiftingToolRight,
                isLiftingToolLeft,
                isLiftingToolUp,
                isLiftingToolDown,
                isSwingingToolRight,
                isSwingingToolLeft,
                isSwingingToolUp,
                isSwingingToolDown,
                isPickingRight,
                isPickingLeft,
                isPickingUp,
                isPickingDown, false, false, false, false); 
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        Vector2 move = new Vector2(xInput * movementSpeed * Time.deltaTime, yInput * movementSpeed * Time.deltaTime);
        rigidbody2D.MovePosition( rigidbody2D.position + move);
    }
    
    private void ResetTrigger()
    {
     isWalking = false;
     isRunning = false;
     toolEffect = ToolEffect.none;
     isCarrying = false;
     isUsingToolRight = false;
     isUsingToolLeft = false;
     isUsingToolUp = false;
     isUsingToolDown = false;
     isLiftingToolRight = false;
     isLiftingToolLeft = false;
     isLiftingToolUp = false;
     isLiftingToolDown = false;
     isSwingingToolRight = false;
     isSwingingToolLeft = false;
     isSwingingToolUp = false;
     isSwingingToolDown = false;
     isPickingRight = false;
     isPickingLeft = false;
     isPickingUp = false;
     isPickingDown = false;
     isIdle = false;
    }

    void PlayerInputMovement()
    {
         xInput = Input.GetAxis("Horizontal");
         yInput = Input.GetAxis("Vertical");
        
        if (xInput != 0 && yInput != 0)
        {
            xInput *= 0.71f;
            yInput *= 0.71f;
        }

        if (xInput != 0 || yInput != 0)
        {

            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.runningSpeed;

            if (xInput < 0)
            {
                PlayerDirection = Direction.left;
            }
            else if (xInput > 0)
            {
                PlayerDirection = Direction.right;
            }
            
            else if (yInput < 0)
            {
                PlayerDirection = Direction.down;
            }
            
            else if (yInput > 0)
            {
                PlayerDirection = Direction.up;
            }
        }
        else
        {
            isWalking = false;
            isRunning = false;
            isIdle = true;
        }
    }

    void PlayerWalkingInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isRunning = false;
            isWalking = true;
            isIdle = false;
            movementSpeed = Settings.walkingSpeed;
        }
        else
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.runningSpeed;
        }
    }

    public void ResetMovement()
    {
        xInput = 0.0f;
        yInput = 0.0f;
        isWalking = false;
        isRunning = false;
        isIdle = true;
    }
    public void DisablePlayerInputAndResetMovement()
    {
        DisablePlayerInput();
        ResetMovement();
        
        EventHandler.CallMovementEvent( xInput, yInput, isWalking, isRunning, isIdle, isCarrying, toolEffect,isUsingToolRight,
            isUsingToolLeft,
            isUsingToolUp,
            isUsingToolDown,
            isLiftingToolRight,
            isLiftingToolLeft,
            isLiftingToolUp,
            isLiftingToolDown,
            isSwingingToolRight,
            isSwingingToolLeft,
            isSwingingToolUp,
            isSwingingToolDown,
            isPickingRight,
            isPickingLeft,
            isPickingUp,
            isPickingDown, false, false, false, false); 
        PlayerMovement();
        
    }

    public void DisablePlayerInput()
    {
        PlayerInputIsDisable = true;
    }
    
    public void EnablePlayerInput()
    {
        PlayerInputIsDisable = false;
    }
    
    public Vector3 GetPlayerViewportPosition()
    {
        return camera.WorldToViewportPoint(transform.position);
    }
}
