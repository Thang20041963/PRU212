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
        if (playerId == 1)
        {
            return HandlePlayer1Input();
        }
        else if (playerId == 2)
        {
            return HandlePlayer2Input();
        }

        return new List<string>();
    }

    private List<string> HandlePlayer1Input()
    {
        List<string> actions = new List<string>();

        if (Input.GetKey(KeyCode.A)) actions.Add("moveLeft");
        if (Input.GetKey(KeyCode.D)) actions.Add("moveRight");
        if (Input.GetKey(KeyCode.W)) actions.Add("jump");
        if (Input.GetKey(KeyCode.S)) actions.Add("block");
        if (Input.GetKey(KeyCode.J)) actions.Add("normalAttack");
        if (Input.GetKey(KeyCode.K)) actions.Add("jump");
        if (Input.GetKey(KeyCode.L)) actions.Add("dash");
        if (Input.GetKey(KeyCode.U)) actions.Add("specialAttack1");
        if (Input.GetKey(KeyCode.I)) actions.Add("specialAttack2");
        Debug.Log(actions);
        return actions;
    }

    private List<string> HandlePlayer2Input()
    {
        List<string> actions = new List<string>();

        if (Input.GetKey(KeyCode.LeftArrow)) actions.Add("moveLeft");
        if (Input.GetKey(KeyCode.RightArrow)) actions.Add("moveRight");
        if (Input.GetKey(KeyCode.UpArrow)) actions.Add("jump");
        if (Input.GetKey(KeyCode.DownArrow)) actions.Add("block");
        if (Input.GetKey(KeyCode.Keypad1)) actions.Add("normalAttack");
        if (Input.GetKey(KeyCode.Keypad2)) actions.Add("jump");
        if (Input.GetKey(KeyCode.Keypad3)) actions.Add("dash");
        if (Input.GetKey(KeyCode.Keypad4)) actions.Add("specialAttack1");
        if (Input.GetKey(KeyCode.Keypad5)) actions.Add("specialAttack2");

        return actions;
    }
}