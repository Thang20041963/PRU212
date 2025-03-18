using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    private float direction;
    private bool hit;
    private BoxCollider2D boxCollider;
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

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
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
                    Debug.Log($"Dart hit {enemy.name}, remaining health: {enemy.health}");
                }
            }
        }

        Invoke(nameof(Deactivate), 0.5f); // Give time for explosion animation
    }

    public void SetDirection(float _direction)
    {
        Debug.Log("Phi tieu");
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX = - localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
