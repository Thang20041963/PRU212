using UnityEngine;
using UnityEngine.TextCore.Text;


public class InputHandler : MonoBehaviour
{
    public int playerId;

    public void SetPlayerId(int id)
    {
        playerId = id;
    }

    public string GetCurrentInput()
    {
        if (playerId == 1)
        {
            return HandlePlayer1Input();
        }
        else if (playerId == 2)
        {
            return HandlePlayer2Input();
        }

        return "";
    }

    private string HandlePlayer1Input()
    {
        if (Input.GetKey(KeyCode.A)) return "moveLeft";
        if (Input.GetKey(KeyCode.D)) return "moveRight";
        if (Input.GetKey(KeyCode.W)) return "jump";
        if (Input.GetKey(KeyCode.S)) return "block";
        if (Input.GetKey(KeyCode.J)) return "normalAtack";
        if (Input.GetKey(KeyCode.K)) return "jump";
        if (Input.GetKey(KeyCode.L)) return "dash";
        if (Input.GetKey(KeyCode.U)) return "specialAttack1";
        if (Input.GetKey(KeyCode.I)) return "specialAttack2";
        return "";
    }

    private string HandlePlayer2Input()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) return "moveLeft";
        if (Input.GetKey(KeyCode.RightArrow)) return "moveRight";
        if (Input.GetKey(KeyCode.UpArrow)) return "jump";
        if (Input.GetKey(KeyCode.DownArrow)) return "block";
        if (Input.GetKey(KeyCode.Keypad1)) return "normalAtack";
        if (Input.GetKey(KeyCode.Keypad2)) return "jump";
        if (Input.GetKey(KeyCode.Keypad3)) return "dash";
        if (Input.GetKey(KeyCode.Keypad4)) return "specialAttack1";
        if (Input.GetKey(KeyCode.Keypad5)) return "specialAttack2";
        return "";
    }
}


