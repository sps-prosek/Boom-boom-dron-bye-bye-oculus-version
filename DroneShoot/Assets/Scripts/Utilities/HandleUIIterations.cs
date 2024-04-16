using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleUIIterations : MonoBehaviour
{
    public ScoreAndMenuManager samm;

    public void HandleInputData(string input)
    {
        samm.userInput = input;
    }

    public void GoToMenu()
    {
        samm.GoToMenu();
    }

    public void SaveData()
    {
        samm.SaveScore();
    }
}
