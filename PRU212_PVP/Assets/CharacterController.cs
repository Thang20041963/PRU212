using JetBrains.Annotations;
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
        string inputAction = inputHandler.GetCurrentInput();
        HandleAction(inputAction);
    }

    private void HandleAction(string action)
    {
        switch (action)
        {
            case "moveLeft":
                MoveLeftRight(-1);
                break;
            case "moveRight":
                MoveLeftRight(1);
                break;
            case "jump":
                Jump();
                break;
            case "block":
                Block();
                break;
            case "normalAtack":
                NormalAttack();
                break;
            case "dash":
                Dash();
                break;
            case "specialAttack1":
                SpecialAttack1();
                break;
            case "specialAttack2":
                SpecialAttack2();
                break;
            default:
                StopMovement();
                break;
        }
    }

    private void MoveLeftRight(float direction)
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        transform.localScale = new Vector3(direction > 0 ? 1 : -1, 1, 1);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed); // Apply jump force
    }

    private void Block()
    {
        Debug.Log($"{characterName} is blocking!");
    }

    private void NormalAttack()
    {
        Debug.Log($"{characterName} is performing a normal attack!");
    }

    private void SpecialAttack1()
    {
        Debug.Log($"{characterName} is performing special attack 1!");
    }

    private void SpecialAttack2()
    {
        Debug.Log($"{characterName} is performing special attack 2!");
    }

    private void Dash()
    {
        Debug.Log($"{characterName} is performing a dash!");
    }

    private void StopMovement()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
}
