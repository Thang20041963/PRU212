using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

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
        if (Input.GetKeyDown(KeyCode.S)) inputs.Add("block");
        if (Input.GetKeyDown(KeyCode.J)) inputs.Add("punchAttack");
        if (Input.GetKeyDown(KeyCode.K)) inputs.Add("kickAttack");
        if (Input.GetKeyDown(KeyCode.L)) inputs.Add("throwDart");
        if (Input.GetKeyDown(KeyCode.U)) inputs.Add("specialAttack1");
        if (Input.GetKeyDown(KeyCode.I)) inputs.Add("specialAttack2");
        
        return inputs;
    }

    private List<string> GetPlayer2Inputs()
    {
        List<string> inputs = new List<string>();

        if (Input.GetKey(KeyCode.LeftArrow)) inputs.Add("moveLeft");
        if (Input.GetKey(KeyCode.RightArrow)) inputs.Add("moveRight");
        if (Input.GetKey(KeyCode.UpArrow)) inputs.Add("jump");
        if (Input.GetKeyDown(KeyCode.DownArrow)) inputs.Add("block");
        if (Input.GetKeyDown(KeyCode.Keypad1)) inputs.Add("punchAttack");
        if (Input.GetKeyDown(KeyCode.Keypad2)) inputs.Add("kickAttack");
        if (Input.GetKeyDown(KeyCode.Keypad3)) inputs.Add("throwDart");
        if (Input.GetKeyDown(KeyCode.Keypad4)) inputs.Add("specialAttack1");
        if (Input.GetKeyDown(KeyCode.Keypad5)) inputs.Add("specialAttack2");

        return inputs;
    }
}