using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static EmptyEvent onPlayerDisappeared = new EmptyEvent();

    [SerializeField] private float runSpeed = 4f;

    [SerializeField] private float jumpSpeed = 8f;

    [SerializeField] private float maxJumpTime = .4f;

    [SerializeField] private float groundCheckRadius = 0.2f;

    [SerializeField] private Vector2 groundCheckOffset = new Vector2(0, -.93f);

    [SerializeField] private LayerMask whatIsGround = 8;

    private Rigidbody2D playerBody;

    private Vector3 currentCheckPoint;

    private PlayerStateController stateController;

    private bool isGrounded = false;
    private bool isFacingRight = true;
    private bool isJumpAvailable = true; // false when jump in air
    private bool isGuidedJump = false;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();

        stateController = new PlayerStateController(GetComponent<Animator>());

        GameManager.onAssignCheckpoint.AddListener((position) =>
        {
            Debug.Log("Current checkpoint " + position);
            currentCheckPoint = position;
        });

        GameManager.onGameStarted.AddListener((progress) =>
        {
            isGuidedJump = GameManager.CurrentSettings.jumpmode == Settings.Jumpmode.Guided;
            Debug.Log("Game started");
            Activate();
            transform.position = currentCheckPoint;
        });

        CameraController.AssignPlayerObject(transform);
    }

    void Update()
    {

        if (stateController.IsCurrentStateInRange(State.Appear, State.Disappear))
        {
            return;
        }
        if (isGrounded || isGuidedJump)
        {
            float moveDirection = Input.GetAxis("Horizontal");
            playerBody.velocity = new Vector2(moveDirection * runSpeed, playerBody.velocity.y);
            if (moveDirection == 0)
            {
                stateController.SetState(State.Idle);
            }
            else if (isGrounded)
            {
                stateController.SetState(State.Run);
            }
            if ((moveDirection > 0 && !isFacingRight) || (moveDirection < 0 && isFacingRight))
                Flip();
        }

        if (Controls.IsJumpKeyDown && isJumpAvailable)
        {
            StartCoroutine(JumpRoute());
        }
        else if (Controls.IsJumpKeyUp)
        {
            StopCoroutine(JumpRoute());
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(transform.posVector2() + groundCheckOffset, groundCheckRadius, whatIsGround);

        if (!isGrounded && !stateController.IsCurrentStateInRange(State.Appear, State.Disappear))
        {
            if (!stateController.IsCurrentState(State.Jump)) stateController.SetState(State.Jump);
            stateController.SetVertSpeedInfo(playerBody.velocity.y);
        }
        else if (isGrounded)
        {
            isJumpAvailable = true;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.FlipX();
    }

    private IEnumerator JumpRoute()
    {
        if (!isGrounded) isJumpAvailable = false;
        float jumpTimer = 0;
        while (jumpTimer < maxJumpTime)
        {
            jumpTimer += Time.fixedDeltaTime;
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpSpeed - jumpTimer * 10);
            yield return new WaitForFixedUpdate();
        }
    }

    public void DamageSelf()
    {
        Disappear();
    }

    private void Disappear()
    {
        playerBody.velocity = new Vector2(0, 0);
        stateController.SetState(State.Disappear);
    }

    //called from animation
    public void AfterDisapper()
    {
        onPlayerDisappeared?.Invoke();
        Appear();
    }

    private void Appear()
    {
        transform.localPosition = currentCheckPoint;
        stateController.SetState(State.Appear);
    }

    //called from animation
    public void Activate()
    {
        stateController.SetState(isGrounded ? State.Idle : State.Jump);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.posVector2() + groundCheckOffset, groundCheckRadius);
    }
}