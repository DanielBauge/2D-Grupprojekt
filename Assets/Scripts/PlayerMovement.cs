using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController Controller;
    [SerializeField] TrailRenderer TR;
    [SerializeField] Rigidbody2D RB;
    float HorizontalMovement = 0f;
    public float MoveSpeed = 40f;
    bool Jump = false;
    bool Crouch = false;
    bool CanDash = true;
    bool Dashing;
    float DashingPower = 24f;
    float DashingTime = 0.2f;
    float DashingCD = 1f;
    bool CanPause = false;
    bool Pausing;
    bool HasPaused;
    float PausingTime = 0.4f;
    void Update()
    {
        if (Dashing||Pausing)
        {
            return;
        }
        HorizontalMovement = Input.GetAxisRaw("Horizontal") * MoveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            Jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            Crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Crouch = false;
        }
        if(Input.GetKeyDown(KeyCode.Space) && CanDash)
        {
            StartCoroutine(Dash());
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && CanPause && !HasPaused)
        {
            StartCoroutine(Airpause());
        }
        if (!Controller.m_Grounded)
        {
            CanPause = true;
        }
        else if (Controller.m_Grounded)
        {
            CanPause = false;
            Pausing = false;
            HasPaused = false;
  
        }
    }
    private void FixedUpdate()
    {
        if (Dashing||Pausing)
        {
            return;
        }
        Controller.Move(HorizontalMovement * Time.fixedDeltaTime, Crouch, Jump);
        Jump = false;
    }
    IEnumerator Airpause()
    {
        HasPaused = true;
        CanPause = false;
        Pausing = true;
        RB.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(PausingTime);
        RB.constraints = RigidbodyConstraints2D.None;
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        Pausing = false;
    }
    IEnumerator Dash()
    {
        CanDash = false;
        Dashing = true;
        float OriginalGravity = RB.gravityScale;
        RB.gravityScale = 0f;
        RB.velocity = new Vector2(transform.localScale.x * DashingPower, 0f);
        TR.emitting = true;
        yield return new WaitForSeconds(DashingTime);
        TR.emitting = false;
        RB.gravityScale = OriginalGravity;
        Dashing = false;
        yield return new WaitForSeconds(DashingCD);
        CanDash = true;
    }
}
