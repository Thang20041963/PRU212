using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public int playerId;

    private string[] MoveSet = { "moveLeft", "moveRight", "jump", "block", "punchAttack", "kickAttack", "throwDart", "specialAttack1", "specialAttack2" };

    private Dictionary<string, bool> inputType = new Dictionary<string, bool>()
    {
        { "moveLeft", true }, { "moveRight", true }, { "jump", true },  // GetKey
        { "block", false }, { "punchAttack", false }, { "kickAttack", false },
        { "throwDart", false }, { "specialAttack1", false }, { "specialAttack2", false } // GetKeyDown
    };

    private KeyCode[] player1Keys =
    {
        KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.U, KeyCode.I
    };

    private KeyCode[] player2Keys =
    {
        KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow,
        KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad5
    };

    private void Start()
    {
        int randomMode = PlayerPrefs.GetInt("isRandomMode", 0);
        if (randomMode == 1)
        {
            InvokeRepeating(nameof(ShuffleMoveSets), 30f, 30f);
        }
        }

        public void SetPlayerId(int id)
    {
        playerId = id;
    }

    private void Update()
    {

    }

    public List<string> GetCurrentInputs()
    {
        List<string> inputs = new List<string>();

        KeyCode[] keys = (playerId == 1) ? player1Keys : player2Keys;

        for (int i = 0; i < MoveSet.Length; i++)
        {
            string action = MoveSet[i];
            KeyCode key = keys[i];

            if (inputType[action])
            {
                if (Input.GetKey(key)) inputs.Add(action);
            }
            else
            {
                if (Input.GetKeyDown(key)) inputs.Add(action);
            }
        }

        return inputs;
    }

    private void ShuffleMoveSets()
    {
        System.Random random = new System.Random();
        int n = MoveSet.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            (MoveSet[i], MoveSet[j]) = (MoveSet[j], MoveSet[i]);  // Hoán đổi vị trí
        }
    }
}
