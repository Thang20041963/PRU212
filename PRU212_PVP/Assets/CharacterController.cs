using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterController : MonoBehaviour
{
    public string characterName;
    public Sprite characterSprite;
    public float health;
    public float speed;
    public float atk;
    public float def;
    public float chakra;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private float maxHealth = 100f;
    private float maxChakra = 100f;
    private float currentHealth;
    private float currentChakra;

    private HealthBar healthBar;
    private ChakraBar chakraBar;

    public float dartCooldownDuration = 0.5f; // Duration of cooldown in seconds
    private float dartCooldownTimer = 0.0f;

    public float punchCooldownDuration = 0.3f; // Cooldown for punch attack
    private float punchCooldownTimer = 0.0f;

    public float kickCooldownDuration = 0.3f; // Cooldown for kick attack
    private float kickCooldownTimer = 0.0f;


    public float special1CooldownDuration = 2; // Duration of cooldown in seconds
    private float special1CooldownTimer = 0.0f;

    public float special2CooldownDuration = 2; // Duration of cooldown in seconds
    private float special2CooldownTimer = 0.0f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteToDisplay;

    public InputHandler inputHandler;

    public Transform dartPoint;
    public Transform attackPoint;
    public float punchRange = 0.3f;
    public float kickRange = 0.5f;
    public GameObject[] darts;

    
    private void Start()
    {
        currentHealth = maxHealth;
        currentChakra = maxChakra;
        UpdateUI();
        AddDart();
    }

    public void AssignUIElements(HealthBar health, ChakraBar chakra)
    {


        healthBar = health;
        chakraBar = chakra;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (healthBar != null)
            healthBar.SetHealth(Convert.ToInt32(currentHealth));
        if (chakraBar != null)
            chakraBar.SetMaxchakra(Convert.ToInt32(currentChakra));
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("hurt");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.Instance.PlayerDied(this);
        }
        UpdateUI();
    }

    public void UseChakra(float amount)
    {
        currentChakra -= amount;
        if (currentChakra < 0) currentChakra = 0;
        UpdateUI();
    }
    public void getChakra(float amount)
    {
        if (currentChakra < maxChakra)
        {
            currentChakra += amount;
        }
        UpdateUI();
    }

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
        chakra = character.chakra;
        maxHealth = character.health;
        currentHealth = character.health;
        AddDart();
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

        if (special1CooldownTimer > 0)
        {
            special1CooldownTimer -= Time.deltaTime;
        }

        if (special2CooldownTimer > 0)
        {
            special2CooldownTimer -= Time.deltaTime;
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
        bool block = animator.GetBool("block");
        if (!block)
        {
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
            transform.localScale = new Vector3(direction > 0 ? 1 : -1, 1, 1);
            animator.SetBool("run", true);
        }

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

    protected bool CanSpecial1()
    {
        return special1CooldownTimer <= 0;
    }

    protected void StartSpecial1Cooldown()
    {
        special1CooldownTimer = special1CooldownDuration;
    }

    protected bool CanSpecial2()
    {
        return special2CooldownTimer <= 0;
    }

    protected void StartSpecial2Cooldown()
    {
        special2CooldownTimer = special2CooldownDuration;
    }

    public abstract void Block();
    public abstract void PunchAttack();
    public abstract void KickAttack();
    public void Block()
    {

        bool state = animator.GetBool("block");

        animator.SetBool("block", !state);

    }
    public void PunchAttack()
    {
        if (CanPunch())
        {
            GetComponent<Animator>().SetTrigger("punch");

            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, punchRange, LayerMask.GetMask("Character"));

            if (hit != null)
            {
                CharacterController target = hit.GetComponent<CharacterController>();
             
                if (target != null && target != this && target.animator.GetBool("block") == false)
                {
                    target.TakeDamage(atk);
                    Debug.Log($"Punch hit: {hit.name}, remaining health: {target.currentHealth}");
                }
            }

            StartPunchCooldown();
        }
    }
    public void KickAttack()
    {
        if (CanKick())
        {
            GetComponent<Animator>().SetTrigger("kick");

            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, kickRange, LayerMask.GetMask("Character"));

            if (hit != null)
            {
                CharacterController target = hit.GetComponent<CharacterController>();
                if (target != null && target != this && !target.animator.GetBool("block"))
                {
                    target.TakeDamage(atk);
                    Debug.Log($"Kick hit: {hit.name}, remaining health: {target.health}");
                }
            }

            StartKickCooldown();
        }
    }
    public abstract void SpecialAttack1();
    public abstract void SpecialAttack2();
    public void ThrowDart()
    {
        if (CanThrowDart())
        {
            GetComponent<Animator>().SetTrigger("throwDart");
            darts[FindDart()].transform.position = dartPoint.position;
            darts[FindDart()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
            StartDartCooldown();
        }
    }
    private void StopMovement()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        animator.SetBool("run", false);
    }

    private bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    public void AddDart()
    {
        // Find the DartHolder object
        GameObject dartHolder = GameObject.Find("DartHolder");

        if (dartHolder != null)
        {
            // Get all darts that are children of DartHolder
            darts = new GameObject[dartHolder.transform.childCount];
            for (int i = 0; i < dartHolder.transform.childCount; i++)
            {
                darts[i] = dartHolder.transform.GetChild(i).gameObject;
            }
        }
        else
        {
            Debug.LogError("DartHolder not found! Make sure it's in the scene.");
        }
    }
    private int FindDart()
    {
        for (int i = 0; i < darts.Length; i++)
        {
            if (!darts[i].gameObject.activeInHierarchy)
                return i;
        }
        return 0;
    }

}
