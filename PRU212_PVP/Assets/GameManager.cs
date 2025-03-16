using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for UI handling

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CharacterController player1;
    public CharacterController player2;

    private bool isGameOver = false;

    [SerializeField] private CharacterDatabase CharacterDB;
    [SerializeField] private MapDatabase MapDB;

    // UI Elements
    [SerializeField] private HealthBar healthBarP1;
    [SerializeField] private HealthBar healthBarP2;
    [SerializeField] private ChakraBar chakraBarP1;
    [SerializeField] private ChakraBar chakraBarP2;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject backGround;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameOverPanel.SetActive(false); // Ẩn panel lúc đầu
        InitializePlayers();
        InitializeMap();
    }

    private void InitializePlayers()
    {
        int player1selectionId = PlayerPrefs.GetInt("1_selectedCharacterOption", 0);
        int player2selectionId = PlayerPrefs.GetInt("2_selectedCharacterOption", 0);

        Character player1Character = CharacterDB.GetCharacter(player1selectionId);
        Character player2Character = CharacterDB.GetCharacter(player2selectionId);

        if (player1Character.character != null)
        {
            GameObject p1Instance = Instantiate(player1Character.character, Vector3.left * 2, Quaternion.identity);
            player1 = p1Instance.GetComponent<CharacterController>();

            // Thêm relatedComponent vào Scene Hierarchy
            if (player1Character.relatedComponent != null)
            {
                foreach (GameObject component in player1Character.relatedComponent)
                {
                    Instantiate(component, Vector3.zero, Quaternion.identity);
                }
            }
        }
        else
        {
            Debug.LogError("Player 1 character prefab is missing!");
        }

        if (player2Character.character != null)
        {
            GameObject p2Instance = Instantiate(player2Character.character, Vector3.right * 2, Quaternion.identity);
            player2 = p2Instance.GetComponent<CharacterController>();
            p2Instance.transform.localScale = new Vector3(p2Instance.transform.localScale.x * (-1), p2Instance.transform.localScale.y, p2Instance.transform.localScale.z);

            // Thêm relatedComponent vào Scene Hierarchy
            if (player2Character.relatedComponent != null)
            {
                foreach (GameObject component in player2Character.relatedComponent)
                {
                    Instantiate(component, Vector3.zero, Quaternion.identity);
                }
            }
        }
        else
        {
            Debug.LogError("Player 2 character prefab is missing!");
        }

        if (player1 != null)
        {
            player1.tag = "Player1";
            player1.SetUpCharacter(player1Character);
            player1.inputHandler.SetPlayerId(1);
            healthBarP1.SetMaxHealth(Convert.ToInt32(player1.health));
            chakraBarP1.SetMaxchakra(Convert.ToInt32(player1.chakra));
            player1.AssignUIElements(healthBarP1, chakraBarP1); // Attach UI elements
        }

        if (player2 != null)
        {
            player2.tag = "Player2";
            player2.SetUpCharacter(player2Character);
            player2.inputHandler.SetPlayerId(2);
            healthBarP2.SetMaxHealth(Convert.ToInt32(player2.health));
            chakraBarP2.SetMaxchakra(Convert.ToInt32(player2.chakra));
            player2.AssignUIElements(healthBarP2, chakraBarP2); // Attach UI elements
        }
    }


    private void InitializeMap()
    {
        int mapselectionId = PlayerPrefs.GetInt("selectedMapOption");
        Map map = MapDB.GetMap(mapselectionId);
        backGround.GetComponent<Image>().sprite = map.mapSprite;
    }

    public void PlayerDied(CharacterController player)
    {
        if (isGameOver) return;

        isGameOver = true;
        player.getAnimator().SetTrigger("die");

        if (player == player1)
        {
            Debug.Log("Player 1 has been defeated! Player 2 wins!");
        }
        else if (player == player2)
        {
            Debug.Log("Player 2 has been defeated! Player 1 wins!");
        }

        EndGame();
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        gameOverPanel.SetActive(true); // Hiện panel thông báo kết thúc game

    }

    public void RestartGame()
    {
        Destroy(gameObject); // Hủy GameManager cũ
        SceneManager.LoadScene("FightingScene");
    }
}
