using UnityEngine;

public class ItachiS1Script : MonoBehaviour
{
    private bool hit;
    private BoxCollider2D boxCollider;
    private float direction;
    private Animator animator;

    public int damage = 10;
    private string ownerTag;

    public void SetOwner(string tag)
    {
        ownerTag = tag;
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void SetDirection(float _direction)
    {
        Debug.Log("Special1");
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit) return;
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("explode");

        string enemyTag = this.ownerTag == "Player1" ? "Player2" : "Player1";

        if (collision.CompareTag(enemyTag)) // Check if the collided object is tagged "Player"
        {
            CharacterController enemy = collision.GetComponent<CharacterController>();
            if (enemy != null)
            {
                bool isblock = enemy.getBlockStatus(); // Only call this if enemy is not null
                if (!isblock)
                {
                    enemy.TakeDamage(damage);
                    Debug.Log($"Fireball hit {enemy.name}, remaining health: {enemy.health}");
                }
            }
        }
        Invoke(nameof(Deactivate), 0.5f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
