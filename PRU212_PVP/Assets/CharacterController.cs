using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
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
        List<string> currentInputs = inputHandler.GetCurrentInputs();
        HandleActions(currentInputs);
    }

    private void HandleActions(List<string> actions)
    {
        bool isMovingLeft = actions.Contains("moveLeft");
        bool isMovingRight = actions.Contains("moveRight");
        bool isJumping = actions.Contains("jump");

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

    private void Block() => Debug.Log($"{characterName} is blocking!");
    private void PunchAttack() 
    {
    
    }
    private void KickAttack() => Debug.Log($"{characterName} is performing a normal attack!");
    private void SpecialAttack1() => Debug.Log($"{characterName} is performing special attack 1!");
    private void SpecialAttack2() => Debug.Log($"{characterName} is performing special attack 2!");
    private void ThrowDart() => Debug.Log($"{characterName} is throwing dart!");
    private void StopMovement()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        animator.SetBool("run", false);
        animator.SetBool("jump", false);
    }

    private bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}
