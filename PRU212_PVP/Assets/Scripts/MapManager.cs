using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MapManager : MonoBehaviour
{

    public MapDatabase MapDB;
    public TextMeshProUGUI nameText;
    public SpriteRenderer artworkSprite;

    private int selectedMapOption = 0;
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedMapOption"))
        {
            selectedMapOption = 0;
        }
        else
        {
            Load();
        }
        UpdateMap(selectedMapOption);
    }
    public void NextOption()
    {
        Debug.Log("click");
        selectedMapOption++;
        if (selectedMapOption >= MapDB.MapCount)
        {
            selectedMapOption = 0;
        }
        UpdateMap(selectedMapOption);
        Save();
    }
    public void BackOption()
    {
        Debug.Log("click");
        selectedMapOption--;
        if (selectedMapOption < 0)
        {
            selectedMapOption = MapDB.MapCount - 1;
        }
        UpdateMap(selectedMapOption);
        Save();
    }

    private void UpdateMap(int selectOption)
    {
        Map Map = MapDB.GetMap(selectOption);
        artworkSprite.sprite = Map.mapSprite;
        nameText.text = Map.mapName;
    }
    private void Load()
    {
        selectedMapOption = PlayerPrefs.GetInt("selectedMapOption");
    }
    private void Save()
    {
        PlayerPrefs.SetInt("selectedMapOption", selectedMapOption);
    }
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
