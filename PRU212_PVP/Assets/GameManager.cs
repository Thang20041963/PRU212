using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CharacterController player1;
    public CharacterController player2;

    private bool isGameOver = false;
    [SerializeField]
    private CharacterDatabase CharacterDB;
    [SerializeField]
    private MapDatabase MapDB;
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
        InitializePlayers();
        InitializeMap();
    }

    private void InitializePlayers()
    {
        int player1selectionId = PlayerPrefs.GetInt("1_selectedCharacterOption", 0);
        int player2selectionId = PlayerPrefs.GetInt("2_selectedCharacterOption", 0);

        Debug.Log(player1selectionId);
        Debug.Log(player2selectionId);

        Character player1Character = CharacterDB.GetCharacter(player1selectionId);
        Character player2Character = CharacterDB.GetCharacter(player2selectionId);

        if (player1Character.character != null)
        {
            GameObject p1Instance = Instantiate(player1Character.character, Vector3.left * 2, Quaternion.identity);
            player1 = p1Instance.GetComponent<CharacterController>();
        }
        else
        {
            Debug.LogError("Player 1 character prefab is missing!");
        }

        if (player2Character.character != null)
        {
            GameObject p2Instance = Instantiate(player2Character.character, Vector3.right * 2, Quaternion.identity);
            player2 = p2Instance.GetComponent<CharacterController>();
        }
        else
        {
            Debug.LogError("Player 2 character prefab is missing!");
        }

        if (player1 != null)
        {
            player1.SetUpCharacter(player1Character);
            player1.inputHandler.SetPlayerId(1);
        }

        if (player2 != null)
        {
            player2.SetUpCharacter(player2Character);
            player2.inputHandler.SetPlayerId(2);
        }
    }
    private void InitializeMap()
    {
        int mapselectionId = PlayerPrefs.GetInt("selectedMapOption");

        Map map = MapDB.GetMap(mapselectionId);

    }

    public void PlayerDied(CharacterController player)
    {
        if (isGameOver) return;

        isGameOver = true;

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
        // Add logic for ending the game
        Debug.Log("Game Over!");
        Invoke("RestartGame", 3f); // Restart after 3 seconds
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
