using System;
using TMPro;
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
    [SerializeField] private TMP_Text  winnerAnnoucement;
    [SerializeField] private TMP_Text Round;
    [SerializeField] private GameObject winnerRoundPanel;
    [SerializeField] private TMP_Text roundAnnouncementText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button homeButton;
    private int nor; // number of rounds
    private int healthsetting;
    private int currentRound = 1;
    private string roundAnnoucment = "";
    private int p1score = 0;
    private int p2score = 0;

    private Vector3 player1StartPosition;
    private Vector3 player2StartPosition;
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
        Round.text = "Round " + 1;
        nor = PlayerPrefs.GetInt("NoRSetting", 1);
        gameOverPanel.SetActive(false); // Ẩn panel lúc đầu
        InitializePlayers();
        InitializeMap();
        Time.timeScale = 1f;
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
            player1StartPosition = p1Instance.transform.position;
            // Thêm relatedComponent vào Scene Hierarchy
            if (player1Character.relatedComponent != null)
            {
                foreach (GameObject component in player1Character.relatedComponent)
                {
                    GameObject relatedObj = Instantiate(component, Vector3.zero, Quaternion.identity);
                    relatedObj.name = component.name.Replace("(Clone)", "") + "_P1";
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
            player2StartPosition = p2Instance.transform.position;
            // Thêm relatedComponent vào Scene Hierarchy
            if (player2Character.relatedComponent != null)
            {
                foreach (GameObject component in player2Character.relatedComponent)
                {
                    GameObject relatedObj = Instantiate(component, Vector3.zero, Quaternion.identity);
                    relatedObj.name = component.name.Replace("(Clone)", "") + "_P2";
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
            Debug.Log(player1.health);
            Debug.Log(player1.chakra);
            player1.inputHandler.SetPlayerId(1);
            healthBarP1.SetMaxHealth(Convert.ToInt32(player1.health));
            chakraBarP1.SetMaxchakra(Convert.ToInt32(player1.chakra));
            player1.AssignUIElements(healthBarP1, chakraBarP1); // Attach UI elements
        }

        if (player2 != null)
        {
            player2.tag = "Player2";
            player2.SetUpCharacter(player2Character);
            Debug.Log(player2.health);
            Debug.Log(player2.chakra);
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
        if (map.mapComponents != null)
        {
            foreach (MapComponent component in map.mapComponents)
            {
                Instantiate(component.mapComponent, new Vector3(component.positionX, component.positionY, component.positionZ), Quaternion.identity);
            }
        }
     }

    public void PlayerDied(CharacterController player)
    {
        if (isGameOver) return;

        isGameOver = true;
        player.getAnimator().SetTrigger("die");
        
        if (player == player1)
        {
            winnerAnnoucement.text = player1.characterName + " has been defeated! " + player2.characterName +" wins!";
            roundAnnoucment = "Player 2 - " + player2.characterName +" - wins round!";
            p2score++;
        }
        else if (player == player2)
        {
            winnerAnnoucement.text = player2.characterName + " has been defeated! " + player1.characterName + " wins!";
            roundAnnoucment = "Player 1 - " + player1.characterName + " - wins round!";
            p1score++;

        }
        currentRound++;
       
        if (currentRound > nor || p1score >= ((nor/2) + 1) || p2score >= ((nor / 2) + 1))
        {
            Invoke("EndGame", 2f);
         
        }
        else
        {
            
            Invoke("RestartRound", 2f); // Delay before restarting
        }
    }
    private void RestartRound()
    {
        
        isGameOver = false;
        player1.enabled = false;
        player2.enabled = false;
        // Hiển thị panel thông báo
        winnerRoundPanel.SetActive(true);
        roundAnnouncementText.text = roundAnnoucment;

        // Dừng game lại
        Time.timeScale = 0f;

        // Gán sự kiện cho các nút
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(ContinueGame);

        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(GoToHome);
    }

    private void ContinueGame()
    {
        Round.text = "Round " + currentRound;
        // Ẩn thông báo
        winnerRoundPanel.SetActive(false);
        player1.enabled = true;
        player2.enabled = true;
        // Reset lại nhân vật và thanh máu/chakra
        player1.ResetCharacter();
        player2.ResetCharacter();

        player1.transform.position = player1StartPosition;
        player2.transform.position = player2StartPosition;

        // Ẩn bảng Game Over nếu đang hiển thị
        gameOverPanel.SetActive(false);

        // Tiếp tục game
        Time.timeScale = 1f;
    }

    private void GoToHome()
    {
        // Load lại menu chính (hoặc đổi Scene)
        SceneManager.LoadScene("MainMenuScene");
    }


    private void EndGame()
    {
        player1.GetComponent<SpriteRenderer>().enabled = false;
        player2.GetComponent<SpriteRenderer>().enabled = false;
        gameOverPanel.SetActive(true); // Hiện panel thông báo kết thúc game
                                       // Time.timeScale = 0f;
        Destroy(gameObject); // Hủy GameManager cũ
    }

    public void RestartGame()
    {
        Destroy(gameObject); // Hủy GameManager cũ
        SceneManager.LoadScene("FightingScene");
    }
}
