using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public int playerId;

    public void SetPlayerId(int id)
    {
        playerId = id;
    }

    public List<string> GetCurrentInputs()
    {
        List<string> inputs = new List<string>();

        if (playerId == 1)
        {
            inputs = GetPlayer1Inputs();
        }
        else if (playerId == 2)
        {
            inputs = GetPlayer2Inputs();
        }

        return inputs;
    }

    private List<string> GetPlayer1Inputs()
    {
        List<string> inputs = new List<string>();

        if (Input.GetKey(KeyCode.A)) inputs.Add("moveLeft");
        if (Input.GetKey(KeyCode.D)) inputs.Add("moveRight");
        if (Input.GetKey(KeyCode.W)) inputs.Add("jump");
        if (Input.GetKey(KeyCode.S)) inputs.Add("block");
        if (Input.GetKey(KeyCode.J)) inputs.Add("punchAttack");
        if (Input.GetKey(KeyCode.K)) inputs.Add("kickAttack");
        if (Input.GetKey(KeyCode.L)) inputs.Add("throwDart");
        if (Input.GetKey(KeyCode.U)) inputs.Add("specialAttack1");
        if (Input.GetKey(KeyCode.I)) inputs.Add("specialAttack2");

        return inputs;
    }

    private List<string> GetPlayer2Inputs()
    {
        List<string> inputs = new List<string>();

        if (Input.GetKey(KeyCode.LeftArrow)) inputs.Add("moveLeft");
        if (Input.GetKey(KeyCode.RightArrow)) inputs.Add("moveRight");
        if (Input.GetKey(KeyCode.UpArrow)) inputs.Add("jump");
        if (Input.GetKey(KeyCode.DownArrow)) inputs.Add("block");
        if (Input.GetKey(KeyCode.Keypad1)) inputs.Add("punchAttack");
        if (Input.GetKey(KeyCode.Keypad2)) inputs.Add("kickAttack");
        if (Input.GetKey(KeyCode.Keypad3)) inputs.Add("throwDart");
        if (Input.GetKey(KeyCode.Keypad4)) inputs.Add("specialAttack1");
        if (Input.GetKey(KeyCode.Keypad5)) inputs.Add("specialAttack2");

        return inputs;
    }
}