using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    public string characterName;
    public Sprite characterSprite;
    public float health;
    public float speed;
    public float atk;
    public float def;
    public float mana;
    public LayerMask groundLayer;
    public Transform groundCheck;


    public float dartCooldownDuration = 0.5f; // Duration of cooldown in seconds
    private float dartCooldownTimer = 0.0f;

    public float punchCooldownDuration = 0.3f; // Cooldown for punch attack
    private float punchCooldownTimer = 0.0f;

    public float kickCooldownDuration = 0.3f; // Cooldown for kick attack
    private float kickCooldownTimer = 0.0f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteToDisplay;

    public InputHandler inputHandler;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteToDisplay = GetComponent<SpriteRenderer>();

        if (characterSprite != null)
        {
            spriteToDisplay.sprite = characterSprite;
            Debug.Log($"Sprite set to: {characterSprite.name}");
        }
        else
        {
            Debug.LogWarning("Character sprite is null!");
        }
    }

    public void SetUpCharacter(Character character)
    {
        characterName = character.characterName;
        characterSprite = character.characterSprite;
        health = character.health;
        speed = character.speed;
        atk = character.atk;
        def = character.def;
        mana = character.mana;

        if (spriteToDisplay != null && characterSprite != null)
        {
            spriteToDisplay.sprite = characterSprite;
            Debug.Log($"Sprite updated to: {characterSprite.name}");
        }
        else
        {
            Debug.LogWarning("Failed to update sprite: SpriteRenderer or characterSprite is null.");
        }
    }

    private void Update()
    {
        if (inputHandler == null) return;

        // Update the cooldown timer
        if (dartCooldownTimer > 0)
        {
            dartCooldownTimer -= Time.deltaTime;
        }

        if (punchCooldownTimer > 0)
        {
            punchCooldownTimer -= Time.deltaTime;
        }

        if (kickCooldownTimer > 0)
        {
            kickCooldownTimer -= Time.deltaTime;
        }

        List<string> currentInputs = inputHandler.GetCurrentInputs();
        HandleActions(currentInputs);
    }

    private void HandleActions(List<string> actions)
    {
        bool isMovingLeft = actions.Contains("moveLeft");
        bool isMovingRight = actions.Contains("moveRight");
        bool isJumping = actions.Contains("jump");

        bool isFighting = actions.Contains("punchAttack") ||
                      actions.Contains("kickAttack") ||
                      actions.Contains("specialAttack1") ||
                      actions.Contains("specialAttack2");

        // Allow only move and jump actions together, block fight actions
        if ((isMovingLeft || isMovingRight || isJumping) && isFighting)
        {
            Debug.Log("Cannot fight while moving or jumping.");
            return;
        }

        if (isMovingLeft && isJumping)
        {
            MoveLeftRight(-1);
            Jump();
        }
        else if (isMovingRight && isJumping)
        {
            MoveLeftRight(1);
            Jump();
        }
        else if (isMovingLeft)
        {
            MoveLeftRight(-1);
        }
        else if (isMovingRight)
        {
            MoveLeftRight(1);
        }
        else if (isJumping)
        {
            Jump();
        }
        else
        {
            StopMovement();
        }

        if (actions.Contains("block")) Block();
        if (actions.Contains("punchAttack")) PunchAttack();
        if (actions.Contains("kickAttack")) KickAttack();
        if (actions.Contains("throwDart")) ThrowDart();
        if (actions.Contains("specialAttack1")) SpecialAttack1();
        if (actions.Contains("specialAttack2")) SpecialAttack2();
    }

    private void MoveLeftRight(float direction)
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        transform.localScale = new Vector3(direction > 0 ? 1 : -1, 1, 1);
        animator.SetBool("run", true);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed); // Apply jump force
            animator.SetTrigger("jump");
        }

        animator.SetBool("grounded", true);
    }

    protected bool CanThrowDart()
    {
        return dartCooldownTimer <= 0;
    }

    protected void StartDartCooldown()
    {
        dartCooldownTimer = dartCooldownDuration;
    }

    protected bool CanPunch()
    {
        return punchCooldownTimer <= 0 && IsGrounded();
    }

    protected void StartPunchCooldown()
    {
        punchCooldownTimer = punchCooldownDuration;
    }

    protected bool CanKick()
    {
        return kickCooldownTimer <= 0 && IsGrounded();
    }

    protected void StartKickCooldown()
    {
        kickCooldownTimer = kickCooldownDuration;
    }

    public abstract void Block();
    public abstract void PunchAttack();
    public abstract void KickAttack();
    public abstract void SpecialAttack1();
    public abstract void SpecialAttack2();
    public abstract void ThrowDart();
    private void StopMovement()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        animator.SetBool("run", false);
        animator.SetBool("jump", false);
    }

    private bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}
