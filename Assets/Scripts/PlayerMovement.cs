using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private Animator anim;
    private bool grounded;
    private float originalJumpSpeed;
    private bool boostedJump;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip runningSound;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalJumpSpeed = jumpSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
           Jump();
           SoundManager.instance.PlaySound(jumpSound);
        }

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }

    private void Jump()
    {   
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    public void BoostJump(float multiplier, float duration)
    {
        if (!boostedJump)
        {
            jumpSpeed *= multiplier;
            boostedJump = true;
            Invoke(nameof(ResetJumpBoost), duration); // Reset jump after duration
        }
    }

    private void ResetJumpBoost()
    {
        jumpSpeed = originalJumpSpeed;
        boostedJump = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
