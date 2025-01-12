using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public TextMeshProUGUI nameText;
    public SpriteRenderer artworkSprite;

    private int selectedCharacterOption;

    void Start()
    {
       
        selectedCharacterOption = PlayerPrefs.GetInt("selectedCharacterOption", 0);
        UpdateCharacter(selectedCharacterOption);
    }

    public void NextOption()
    {
        
        selectedCharacterOption = (selectedCharacterOption + 1) % characterDB.CharacterCount;
        UpdateCharacter(selectedCharacterOption);
        Save();
    }

    public void BackOption()
    {
        
        selectedCharacterOption = (selectedCharacterOption - 1 + characterDB.CharacterCount) % characterDB.CharacterCount;
        UpdateCharacter(selectedCharacterOption);
        Save();
    }

    private void UpdateCharacter(int selectOption)
    {
        Character character = characterDB.GetCharacter(selectOption);

      
        if (artworkSprite.sprite != character.characterSprite)
        {
            artworkSprite.sprite = character.characterSprite;
        }

        if (nameText.text != character.characterName)
        {
            nameText.text = character.characterName;
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedCharacterOption", selectedCharacterOption);
    }

    public void ChangeScene(int sceneID)
    {
        Save();
        SceneManager.LoadScene(sceneID);
    }

    private void OnDestroy()
    {
       
        Save();
    }
}
