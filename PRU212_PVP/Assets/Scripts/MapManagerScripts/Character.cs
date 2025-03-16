using UnityEngine;
[System.Serializable]
public class Character 
{
    public string characterName;
    public Sprite characterSprite;
    public float health;
    public float speed;
    public float atk;
    public float def;
    public float chakra;
    public GameObject character;

    public GameObject[] relatedComponent;
}
