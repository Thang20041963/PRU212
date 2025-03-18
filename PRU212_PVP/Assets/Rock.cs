using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject crackEffectPrefab;
    private Vector3 initialPosition;
    private float rockdamage;
    private string ownerTag;
    private Rigidbody2D rb;

    public void setOwnerTag(string tag)
    {
        ownerTag = tag;
    }

    public void setRockDamage(float damage)
    {
        rockdamage = damage;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        initialPosition = transform.position; // Lưu vị trí ban đầu
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string enemyTag = this.ownerTag == "Player1" ? "Player2" : "Player1";
        Debug.Log($"Viên đá chạm vào: {collision.gameObject.name}");
        if (collision.CompareTag("Ground"))
        {
            // Sử dụng Raycast để lấy vị trí chạm chính xác
            Vector2 hitPoint = GetGroundHitPoint();
            GameObject crack = Instantiate(crackEffectPrefab, hitPoint, Quaternion.identity);
            Destroy(crack, 1f);
            ResetRock();
        }
        else if (collision.CompareTag(enemyTag))
        {
            // Gây sát thương khi chạm enemy
            CharacterController playerHealth = collision.GetComponent<CharacterController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(rockdamage);
            }
            ResetRock();
        }
    }
    private Vector2 GetGroundHitPoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        return hit.collider != null ? hit.point : transform.position;
    }
    private void ResetRock()
    {
        gameObject.SetActive(false); // Ẩn viên đá
        transform.position = initialPosition;
        rb.linearVelocity = Vector2.zero; // Dừng mọi chuyển động
        rb.angularVelocity = 0;
    }
}
