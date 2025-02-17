using UnityEngine;

[CreateAssetMenu(fileName = "PrefabDataBase", menuName = "Scriptable Objects/PrefabDataBase")]
public class PrefabDataBase : ScriptableObject
{
    public string CharacterName;
    public GameObject[] character;

    public int CharacterCount
    {
        get
        {
            return character.Length;
        }
    }


    public GameObject GetCharacter(int index)
    {
        return character[index];
    }
}
