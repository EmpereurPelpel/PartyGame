using UnityEngine;

public class MinigamePlayerController : PlayerController
{
    #region Movement values
    private float walkSpeed = 3f;
    private float sprintSpeed = 5f;
    #endregion

    #region Minigame specific
    private float hasJumpTimerMax = 0.1f;
    private float hasJumpTimerCurrent = 0f;
    private MinigameInputManager inputManager;
    #endregion

    protected override void Start()
    {
        base.Start();
        inputManager = MinigameInputManager.Instance;
    }

    private void Update()
    {
        ManageMovement();
        ManageJump();
    }

    public void Init(PlayerNumber playerNumber)
    {
        SetPlayerNumber(playerNumber);
    }

    public void Init(PlayerNumber playerNumber, Vector3 startPosition)
    {
        Init(playerNumber);
        transform.position = startPosition;
    }

    private void ManageMovement()
    {
        if (MinigameManager.Instance.CurrentMinigamePhase != MinigamePhase.Playing)
            return;
        if (PlayerNumber == PlayerNumber.Player1)
            moveSpeed = inputManager.SprintPlayer1 ? sprintSpeed : walkSpeed;
        else
            moveSpeed = inputManager.SprintPlayer2 ? sprintSpeed : walkSpeed;
        Vector3 moveDirection;
        if (PlayerNumber == PlayerNumber.Player1)
            moveDirection = new Vector3(inputManager.MovePlayer1.x, 0f, inputManager.MovePlayer1.y).normalized;
        else
            moveDirection = new Vector3(inputManager.MovePlayer2.x, 0f, inputManager.MovePlayer2.y).normalized;
        Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        MovePlayer(targetPosition);
    }

    private void ManageJump()
    {
        ManageHasJumpTimer();
        IsGrounded();
        if (isGrounded && hasJumpTimerCurrent >= hasJumpTimerMax && MinigameManager.Instance.CurrentMinigamePhase == MinigamePhase.Playing)
        {
            bool isPlayer1 = PlayerNumber == PlayerNumber.Player1;
            if ((isPlayer1 && inputManager.JumpPlayer1) || (!isPlayer1 && inputManager.JumpPlayer2))
            {
                DoJump();
                hasJumpTimerCurrent = 0f;
            }
        }
    }

    private void ManageHasJumpTimer()
    {
        if (hasJumpTimerCurrent < hasJumpTimerMax)
            hasJumpTimerCurrent += Time.deltaTime;
    }

    public float GetWalkSpeed()
    {
        return walkSpeed;
    }

    public void SetWalkSpeed(float newWalkSpeed)
    {
        walkSpeed = newWalkSpeed;
    }

    public float GetSprintSpeed()
    {
        return sprintSpeed;
    }

    public void SetSprintSpeed(float newSprintSpeed)
    {
        sprintSpeed = newSprintSpeed;
    }
}
