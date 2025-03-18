using UnityEngine;


public class RockFall : MonoBehaviour
{
    public string ownerTag;
    
    private Transform target;
    void Start()
    {
        FindOpponent();
        
    }
    private void Update()
    {
        
        transform.position = target.position + new Vector3(0, 5, 0);
    }
    public void SetOwnerTag(string tag)
    {
        ownerTag = tag;
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
