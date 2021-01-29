using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // GAMEOBJECT COMPONENTS
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D pushBox;
    private Collider2D hurtBox;
    private AudioController audioController;
    private CheckHit checkHit;
    private CheckGround checkGround;
    private Material matDefault;
    [SerializeField] private Material matWhite;

    // VARIABLES
    [SerializeField] private int life;
    [SerializeField] private int form;
    private float moveSpeed;
    private float jumpForce;
    [SerializeField] private bool onTheGround;

    //Initializes
    void Awake()
    {
        // VARIABLES
        form = 1;
        life = 6;
        moveSpeed = 9f;
        jumpForce = 900f;

        ReloadComponents();
    }

    private void ReloadComponents()
    {
        // Gets CHILDREN depending on the current Form
        GameObject Form = gameObject.transform.Find("Form"+form).gameObject;
        GameObject PushBox = Form.transform.Find("PushBox").gameObject;
        GameObject HurtBox = Form.transform.Find("HurtBox").gameObject;
        GameObject GroundBox = Form.transform.Find("GroundBox").gameObject;
        GameObject audioControllerObject = gameObject.transform.Find("AudioController").gameObject;

        // Gets COMPONENTS from the Children
        pushBox = PushBox.GetComponent<Collider2D>();
        hurtBox = HurtBox.GetComponent<Collider2D>();

        audioController = audioControllerObject.GetComponent<AudioController>();
        checkHit = HurtBox.GetComponent<CheckHit>();
        checkGround = GroundBox.GetComponent<CheckGround>();

        animator = Form.GetComponent<Animator>();
        spriteRenderer = Form.GetComponent<SpriteRenderer>();

        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();

        //Getting the dafault material for later use.
        matDefault = spriteRenderer.material;
    }

    // FixedUpdate is called multiple times per frame.
    void FixedUpdate()
    {
        CheckHit();
        CheckGround();
        CheckMovementInput();
    }

    // Update is called once per frame.
    // Is executed after FixedUpdate.
    void Update() 
    {
        
    }

    // COMPLETE?: Changes to an specific form.
    private void ChangeForm(int num)
    {
        //Disables the OLD Form
        GameObject Form = gameObject.transform.Find("Form"+form).gameObject;
        Form.SetActive(false);
        
        //Enables the NEW Form & gets it's new components.
        form = num;
        ReloadComponents();
    }

    // Checks Input, calls for Movement, and sets facing side.
    private void CheckMovementInput()
    {
        // Gets Axis Input
        float HorizontalAxis = Input.GetAxisRaw("Horizontal");
        float VerticalAxis = Input.GetAxisRaw("Vertical");

        
        // If Left or Right is pressed.
        if (HorizontalAxis > 0.5f || HorizontalAxis < -0.5f)
        {
            MovementOnX(HorizontalAxis);
            animator.SetBool("Moving", true);
            FlipSprite(HorizontalAxis);
        }

        // If NO Left or Right is pressed.
        if(HorizontalAxis < 0.1f && HorizontalAxis > -0.1f)
        {
            if(onTheGround)
                MovementOnX(0f);

            animator.SetBool("Moving", false);
        }

        if(Input.GetButton("Jump"))
        {
            if (onTheGround)
            {
                // Adds a vertical force to the player.
                rigidBody2D.AddForce(new Vector2(0f, jumpForce));
                checkGround.onTheGround = false;
                animator.SetBool("Jumping", true);
            }
        }
    }
    
    // Changes the velocity of the Rigidbody according to the values passed (-1f, 0f, or 1f).
    // Because it can recieve 0f as value, this function can stop movement.
    // moveHorizontal : X Value.
    private void MovementOnX(float moveHorizontal)
    {
        float movementDirection = moveHorizontal * moveSpeed;

        if(!onTheGround)
            movementDirection = movementDirection / 1.5f;

        rigidBody2D.velocity = new Vector2(movementDirection, rigidBody2D.velocity.y);
    }

    // Flips the sprite on the X Axis according to the Axis input.
    // Needs to be called by another function, which must check the facing sides first.
    private void FlipSprite(float HorizontalAxis)
    {
        if(HorizontalAxis > 0.1f)
        {
            spriteRenderer.flipX = false;
        }

        if(HorizontalAxis < 0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    //COMPLETE: 
    private void CheckGround()
    {
        onTheGround = checkGround.onTheGround;

        if (onTheGround)
        {
            animator.SetBool("Jumping", false);
        }

        if (!onTheGround)
        {
            animator.SetBool("Falling", true);
        }
    }

    // Checks if the HurtBox has collided with a HitBox from another Object.
    private void CheckHit()
    {
        if(checkHit.isHurt)
        {
            GetHurt();
        }
    }

    // Sets required variables to execute the hurting state.
    private void GetHurt()
    {
        checkHit.isHurt = false;
        hurtBox.enabled = false;
        life--;

        // Make a knockback or some hurting effect!!!
        FlashWhite();

        if(life<=0)
        {
            KillItself();
        }
        else
        {
            Invoke("FlashBack", 0.1f);
            Invoke("FlashWhite", 0.2f);
            Invoke("FlashBack", 0.3f);
            Invoke("FlashWhite", 0.4f);
            Invoke("FlashBack", 0.5f);
            Invoke("StopHurt", 0.6f);
        }
    }

    private void FlashWhite()
    {
        spriteRenderer.material = matWhite;
    }

    private void FlashBack()
    {
        spriteRenderer.material = matDefault;
    }

    // Sets required variables to stop the hurting state.
    public void StopHurt()
    {
        hurtBox.enabled = true;
    }
    private void KillItself()
    {
        Invoke("StopHurt", 0.1f);

        gameObject.SetActive(false);
        //Destroy(gameObject);

        //DETENER EL JUEGO!!!
    }
}
