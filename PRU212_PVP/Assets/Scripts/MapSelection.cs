using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MapSelection : MonoBehaviour
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
   
}
