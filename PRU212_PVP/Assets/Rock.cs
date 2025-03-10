using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject rockPrefab; // Prefab của viên đá
    public Transform spawnPoint;  // Điểm xuất hiện viên đá (có thể là trên đầu kẻ địch)

    public void PerformSpecialAttack()
    {
        StartCoroutine(SpawnRocks());
    }

    private IEnumerator SpawnRocks()
    {
        for (int i = 0; i < 3; i++) // Tạo 3 viên đá rơi liên tiếp
        {
            Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0, 0); // Xác định vị trí rơi ngẫu nhiên
            Instantiate(rockPrefab, spawnPoint.position + randomOffset, Quaternion.identity);
            yield return new WaitForSeconds(0.5f); // Delay giữa các viên đá
        }
    }
}
