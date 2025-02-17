using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public TextMeshProUGUI nameText;
    public SpriteRenderer artworkSprite;
    public int playerID;
    private int selectedCharacterOption;

    void Start()
    {
      
        selectedCharacterOption = PlayerPrefs.GetInt($"{playerID}_selectedCharacterOption", 0);
        UpdateCharacter(selectedCharacterOption);
    }

    public void NextOption()
    {
        Debug.Log("Next");
        selectedCharacterOption = (selectedCharacterOption + 1) % characterDB.CharacterCount;
        UpdateCharacter(selectedCharacterOption);
        Save();
    }

    public void BackOption()
    {
        Debug.Log("Back");
        selectedCharacterOption = (selectedCharacterOption - 1 + characterDB.CharacterCount) % characterDB.CharacterCount;
        UpdateCharacter(selectedCharacterOption);
        Save();
    }

    private void UpdateCharacter(int selectOption)
    {
        Debug.Log(selectOption);
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
        PlayerPrefs.SetInt($"{playerID}_selectedCharacterOption", selectedCharacterOption);
    }



    private void OnDestroy()
    {
       
        Save();
    }
}
