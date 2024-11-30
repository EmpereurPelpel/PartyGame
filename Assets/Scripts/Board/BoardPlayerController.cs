using UnityEngine;

public enum PlayerPhase
{
    WaitingToRoll,
    Rolling,
    Moving,
    EndTurn
}

public class BoardPlayerController : PlayerController
{
    [SerializeField] private Node currentNode;
    public Node CurrentNode => currentNode;

    #region Board Specific
    private Node targetNode;
    private PlayerPhase phase;
    private BoardInputManager inputManager;

    private int nodeMoveRoll;
    private int nodeMoveCount;

    private float rollTimerMax = 2f;
    private float rollTimerCurrent = 0f;
    #endregion

    private bool isThisPlayerTurn = false;

    protected override void Start()
    {
        base.Start();
        phase = isThisPlayerTurn ? PlayerPhase.WaitingToRoll : PlayerPhase.EndTurn;
        inputManager = BoardInputManager.Instance;
    }

    private void Update()
    {
        IsGrounded();
        switch (phase)
        {
            case PlayerPhase.WaitingToRoll:
                StartRolling();
                break;
            case PlayerPhase.Rolling:
                ManageRollTimer();
                break;
            case PlayerPhase.Moving:
                Move();
                break;
        }
    }

    private void Move()
    {
        MovePlayer(Vector3.MoveTowards(transform.position, targetNode.transform.position, moveSpeed * Time.deltaTime));
        if (transform.position == targetNode.transform.position)
        {
            nodeMoveCount++;
            currentNode = targetNode;
            if (nodeMoveCount >= nodeMoveRoll)
            {
                StopMoveAnim();
                phase = PlayerPhase.EndTurn;
                BoardManager.Instance.SavePlayerPositionOnBoard();
                GameManager.Instance.LoadMinigameFromNode(currentNode);
            }
            else
            {
                targetNode = currentNode.NextNode;
            }
        }
    }

    private void StartRolling()
    {
        if (inputManager.Roll)
        {
            DoJump();
            phase = PlayerPhase.Rolling;
            rollTimerCurrent = 0f;
        }
    }

    private void ManageRollTimer()
    {
        if (rollTimerCurrent < rollTimerMax)
        {
            rollTimerCurrent += Time.deltaTime;
        }
        else
        {
            RollResult();
        }
    }

    private void RollResult()
    {
        nodeMoveCount = 0;
        nodeMoveRoll = Random.Range(1, 6);
        targetNode = currentNode.NextNode;
        phase = PlayerPhase.Moving;
        Debug.Log($"Number rolled: {nodeMoveRoll}");
    }

    public void SetCurrentNode(Node newCurrentNode)
    {
        currentNode = newCurrentNode;
    }

    public void StartTurn()
    {
        isThisPlayerTurn = true;
    }
}
