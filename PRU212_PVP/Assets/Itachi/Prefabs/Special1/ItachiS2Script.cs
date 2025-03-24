using UnityEngine;

public class ItachiS2Script : MonoBehaviour
{
    private float direction;
    private Animator animator;
    private string ownerTag;
    public float hitdamage;
    private Transform target;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDirection(float _direction)
    {
        Debug.Log("Special2");
        direction = _direction;
        gameObject.SetActive(true);
    }
    public void SetOwner(string tag)
    {

        ownerTag = tag;

    }

    public void SetDamage(float damage)
    {
        hitdamage = damage;
    }

    public void ActivateSpecial2(float _direction)
    {
        FindOpponent();
        if (target != null)
        {
            direction = _direction;
            gameObject.SetActive(true);

        }
    }

    private void FindOpponent()
    {
        string opponentTag = ownerTag == "Player1" ? "Player2" : "Player1";
        GameObject opponent = GameObject.FindGameObjectWithTag(opponentTag);

        if (opponent != null)
        {
            if (opponent != null)
            {
                opponent.GetComponent<CharacterController>().TakeDamage(hitdamage);
            }
        }
        else
        {
            Debug.LogWarning("No enemy");
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
