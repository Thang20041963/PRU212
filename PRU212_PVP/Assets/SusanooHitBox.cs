using UnityEngine;

public class SusanooHitBox : MonoBehaviour
{
    
    public Susanoo Susanoo;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string enemyTag = Susanoo.ownerTag == "Player1" ? "Player2" : "Player1";
        Debug.Log($"Chems vao : {collision.gameObject.name}");
        Debug.Log(enemyTag);
        if (collision.CompareTag(enemyTag))
        {
            Debug.Log($"Chems vao thang kia");
            CharacterController playerHealth = collision.GetComponent<CharacterController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(Susanoo.hitdamage);
            }

            
        }

    }
    
}