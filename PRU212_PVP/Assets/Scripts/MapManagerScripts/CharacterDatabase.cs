using UnityEngine;
[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    public Character[] character;

    public int CharacterCount
    {
        get
        {
            return character.Length;
        }
    }


    public Character GetCharacter(int index)
    {
        return character[index];
    }
    public Sprite getCharacterSprite(int index)
    {
        return character[index].characterSprite;
    }
}
