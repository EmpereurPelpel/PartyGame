using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    #region Animation params
    private const string IS_GROUNDED_PARAM = "IsGrounded";
    private const string FLOAT_CURRENT_SPEED_PARAM = "CurrentMoveSpeed";
    private const string ON_JUMP_PARAM = "OnJump";
    #endregion

    #region Movement values
    protected float moveSpeed = 5f;
    private float rotationSpeed = 5f;
    private float jumpVelocity = 5f;
    #endregion

    #region Self References
    private Animator animator;
    private Rigidbody rb;
    #endregion

    protected bool isGrounded { get; private set; }
    public PlayerNumber PlayerNumber { get; private set; }

    public void SetPlayerNumber(PlayerNumber playerNumber)
    {
        PlayerNumber = playerNumber;
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.Rebind();
    }
    protected void MovePlayer(Vector3 targetPosition)
    {
        if ( transform.position != targetPosition )
        {
            transform.forward = Vector3.Lerp(transform.forward, (targetPosition - transform.position).normalized, rotationSpeed * Time.deltaTime);
            transform.position = targetPosition;
            animator.SetFloat(FLOAT_CURRENT_SPEED_PARAM, moveSpeed);
        }
        else
        {
            StopMoveAnim();
        }
    }
    protected void DoJump()
    {
        rb.velocity += new Vector3(0, jumpVelocity, 0);
        animator.SetTrigger(ON_JUMP_PARAM);
    }

    protected void IsGrounded()
    {
        float radius = 0.05f;
        isGrounded = Physics.OverlapSphere(transform.position, radius, groundLayer).Length > 0;
        animator.SetBool(IS_GROUNDED_PARAM, isGrounded);
    }
    protected void StopMoveAnim()
    {
        animator.SetFloat(FLOAT_CURRENT_SPEED_PARAM, 0f);
    }

    public float GetJumpVelocity()
    {
        return jumpVelocity;
    }

    public void SetJumpVelocity(float newJumpVelocity)
    {
        jumpVelocity = newJumpVelocity;
    }
}
