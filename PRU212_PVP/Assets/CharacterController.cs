﻿using JetBrains.Annotations;
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
    private float maxHealth ;
    private float maxChakra ;
    private float currentHealth;
    private float currentChakra;

    private HealthBar healthBar;
    private ChakraBar chakraBar;

    public float dartCooldownDuration = 1f; // Duration of cooldown in seconds
    private float dartCooldownTimer = 0.0f;

    public float punchCooldownDuration = 1f; // Cooldown for punch attack
    private float punchCooldownTimer = 0.0f;

    public float kickCooldownDuration = 1f; // Cooldown for kick attack
    private float kickCooldownTimer = 0.0f;

    public float special1CooldownDuration = 2; // Duration of cooldown in seconds
    private float special1CooldownTimer = 0.0f;

    public float special2CooldownDuration = 2; // Duration of cooldown in seconds
    private float special2CooldownTimer = 0.0f;

    public float sp1Charka;
    public float sp2Charka;


    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteToDisplay;

    public InputHandler inputHandler;

    public Transform dartPoint;
    public Transform attackPoint;
    public float punchRange = 0.2f;
    public float kickRange = 0.2f;
    public GameObject[] darts;
    private bool isStunned = false; // Add this flag

    private bool isWait = false;

    private bool isBlock = false;


    public bool getBlockState()
    {
        return isBlock;
    }
    public void setBlockState(bool block)
    {
        isBlock = block;
    }
    public void ResetCharacter()
    {
        // Restore health and chakra to their maximum values
        currentHealth = health;
        currentChakra = chakra/2;

        // Update UI elements
        healthBar?.SetHealth((int)currentHealth);
        chakraBar?.Setchakra((int)currentChakra);

        // Reset animation state
        if (animator != null)
        {
            animator.ResetTrigger("die");
            animator.ResetTrigger("hurt");
            animator.Play("Idle");
        }


        animator.SetBool("block", false);
        // Reset other states
        isStunned = false;
        isWait = false;
        isBlock = false;
        currentAttackState = AttackState.None;

        // Stop any ongoing movement
        rb.linearVelocity = Vector2.zero;
    }



    public bool getWaitState()
    {
        return isWait;
    }
    public void setWaitState(bool wait)
    {
        Debug.Log(wait);
        isWait = wait;
    }
    private enum AttackState { None, Punching, Kicking, Throwing }
    private AttackState currentAttackState = AttackState.None;

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
    public Animator getAnimator()
    {
        return this.animator;
    }

    private void UpdateUI()
    {
        if (healthBar != null)
            healthBar.SetHealth(Convert.ToInt32(currentHealth));
        if (chakraBar != null)
            chakraBar.Setchakra(Convert.ToInt32(currentChakra));
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("hurt");
        StopMovement();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.Instance.PlayerDied(this);
        }
        else
        {
            StartCoroutine(StunCharacter(1f)); 
        }
        UpdateUI();
    }

    private IEnumerator StunCharacter(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    public void UseChakra(float amount)
    {
        currentChakra -= amount;
        if (currentChakra < 0) currentChakra = 0;
        UpdateUI();
    }
    public void gainChakra(float amount)
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

        var healthsetting = PlayerPrefs.GetInt("HealthSetting", 100);
        characterName = character.characterName;
        characterSprite = character.characterSprite;
        speed = character.speed;
        atk = character.atk;
        def = character.def;
        health = healthsetting;
        maxHealth = healthsetting;
        currentHealth = healthsetting;
        chakra = character.chakra;
        maxChakra = character.chakra;
        currentChakra = character.chakra / 2;
        sp1Charka = character.sp1Charka;
        sp2Charka = character.sp2Charka;
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
        if (isStunned) return;
        bool isMovingLeft = actions.Contains("moveLeft");
        bool isMovingRight = actions.Contains("moveRight");
        bool isJumping = actions.Contains("jump");

        bool isFighting = actions.Contains("punchAttack") ||
                      actions.Contains("kickAttack") ||
                      actions.Contains("specialAttack1") ||
                      actions.Contains("specialAttack2");

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
        if (!block && isWait == false)
        {
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
            //transform.localScale = new Vector3(direction > 0 ? 1 : -1, 1, 1);
            //this.transform.localScale = new Vector3(transform.localScale.x * (direction > 0 ? 1 : -1), transform.localScale.y, transform.localScale.z);
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (direction > 0 ? 1 : -1);
            transform.localScale = scale;
            animator.SetBool("run", true);
        }

    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed*1.5f); // Apply jump force
            animator.SetTrigger("jump");
        }

        animator.SetBool("grounded", true);
    }

    protected bool CanPunch()
    {
        return punchCooldownTimer <= 0 && IsGrounded() && currentAttackState == AttackState.None && isBlock == false;
    }

    protected bool CanKick()
    {
        return kickCooldownTimer <= 0 && IsGrounded() && currentAttackState == AttackState.None && isBlock == false;
    }

    protected bool CanThrowDart()
    {
        return dartCooldownTimer <= 0 && currentAttackState == AttackState.None && isBlock == false;
    }


    protected void StartDartCooldown()
    {
        dartCooldownTimer = dartCooldownDuration;
    }



    protected void StartPunchCooldown()
    {
        punchCooldownTimer = punchCooldownDuration;
    }



    protected void StartKickCooldown()
    {
        kickCooldownTimer = kickCooldownDuration;
    }
    protected bool CanSpecial1()
    {
        return special1CooldownTimer <= 0 && currentChakra >= sp1Charka  && isBlock == false;
    }

    protected void StartSpecial1Cooldown()
    {
        special1CooldownTimer = special1CooldownDuration;
    }

    protected bool CanSpecial2()
    {
        return special2CooldownTimer <= 0 && currentChakra >= sp2Charka && isBlock == false;
    }

    protected void StartSpecial2Cooldown()
    {
        special2CooldownTimer = special2CooldownDuration;
    }

 

    public void Block()
    {

        bool state = animator.GetBool("block");

        animator.SetBool("block", !state);
        setBlockState(!state);
    }
    public void PunchAttack()
    {
        if (CanPunch())
        {
            currentAttackState = AttackState.Punching;
            GetComponent<Animator>().SetTrigger("punch");
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, punchRange, LayerMask.GetMask("Character"));

            if (hit != null)
            {
                
                CharacterController target = hit.GetComponent<CharacterController>();
                Debug.Log(target.tag);
                gainChakra(5);
                if (target != null && target != this && !target.animator.GetBool("block"))
                {
                    target.TakeDamage(atk);
                }
            }

            StartPunchCooldown();
            StartCoroutine(ResetAttackStateAfterCooldown(punchCooldownDuration));
        }
    }

    public void KickAttack()
    {
        if (CanKick())
        {
            currentAttackState = AttackState.Kicking;
            GetComponent<Animator>().SetTrigger("kick");
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, kickRange, LayerMask.GetMask("Character"));

            if (hit != null)
            {
                CharacterController target = hit.GetComponent<CharacterController>();
                Debug.Log(target.tag);
                gainChakra(5);
                if (target != null && target != this && !target.animator.GetBool("block"))
                {
                    target.TakeDamage(atk);
                }
            }

            StartKickCooldown();
            StartCoroutine(ResetAttackStateAfterCooldown(kickCooldownDuration));
        }
    }
    public bool getBlockStatus()
    {
        return animator.GetBool("block");
    }
    public abstract void SpecialAttack1();
    public abstract void SpecialAttack2();
    public void ThrowDart()
    {
        if (CanThrowDart())
        {
            currentAttackState = AttackState.Throwing;
            GetComponent<Animator>().SetTrigger("throwDart");
            darts[FindDart()].transform.position = dartPoint.position;
            darts[FindDart()].GetComponent<Projectile>().SetOwner(this.tag);
            darts[FindDart()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
            StartDartCooldown();
            StartCoroutine(ResetAttackStateAfterCooldown(dartCooldownDuration));
        }
    }

    private IEnumerator ResetAttackStateAfterCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        currentAttackState = AttackState.None;
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