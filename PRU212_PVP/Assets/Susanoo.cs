using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Susanoo : MonoBehaviour
{
    private Animator animator;
    public float hitdamage;
    public string ownerTag;
    public SpriteRenderer spriteRenderer;
    public Collider2D col;
    private Transform target;
   
    public void SetOwnerTag(string tag)
    {
        ownerTag = tag;
    }

    public void SetDamage(float damage)
    {
        hitdamage = damage;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        // Không kích hoạt animation ngay lập tức trong Start
    }
    

    public void ActivateSusanoo()
    {
        FindOpponent();
        if (target != null)
        {
            if(target.transform.localScale.x*this.transform.localScale.x>0)
            {
                this.transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
            }
           
            transform.position = target.position + new Vector3(2.2f * (ownerTag == "Player1" ? -1 : 1), 0, 0);
            animator.SetTrigger("Slash"); // Kích hoạt animation "Slash"
        }
    }
    private void FindOpponent()
    {
        string opponentTag = ownerTag == "Player1" ? "Player2" : "Player1";
        GameObject opponent = GameObject.FindGameObjectWithTag(opponentTag);

        if (opponent != null)
        {
            target = opponent.transform;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy đối thủ!");
        }
    }
}