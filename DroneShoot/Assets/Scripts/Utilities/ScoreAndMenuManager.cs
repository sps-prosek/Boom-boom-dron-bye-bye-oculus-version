using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreAndMenuManager : MonoBehaviour
{
    public DroneShotCounter dsc;
    public GameObject UI;
    public GameObject UIPosition;
    private string _scoreFilePath;
    private List<string[]> _scoreList = new List<string[]>();
    public TMP_InputField input;
    public string userInput = "Host";

    public LineRenderer leftHandLineRenderer;
    public XRInteractorLineVisual leftHandLineVisual;
    public LineRenderer rightHandLineRenderer;
    public XRInteractorLineVisual rightHandLineVisual;
    public VRMotionControl vrmc;
    public ShootingManager sm;
    public VRLookingAt vrla;
    public MotionControl mc;
    public LookingAt la;
    void Start()
    {
        _scoreFilePath  = Application.persistentDataPath + "/score.csv";                
    }
    public void KillPlayer()
    {
        GameObject newUI = Instantiate(UI);
        newUI.transform.position = UIPosition.transform.position;
        newUI.transform.SetParent(UIPosition.transform);
        newUI.GetComponent<HandleUIIterations>().samm = gameObject.GetComponent<ScoreAndMenuManager>();
        input = newUI.transform.GetChild(1).GetComponent<TMP_InputField>();
        leftHandLineRenderer.enabled = true;
        rightHandLineRenderer.enabled = true;
        leftHandLineVisual.enabled = true;
        rightHandLineVisual.enabled = true;
        vrmc.enabled = false;
        sm.enabled = false;
        vrla.enabled = false;
        mc.enabled = false;
        la.enabled = false;
    }
    public void LoadScore(){
        if (File.Exists(_scoreFilePath))
        {
            string[] lines = File.ReadAllLines(_scoreFilePath);
            foreach (string line in lines)
            {
                _scoreList.Add(line.Split(";"));
            }
        }
    }
    public void SaveScore()
    {
        if (!File.Exists(_scoreFilePath)){
            FileStream newFile = File.Create(_scoreFilePath);
            newFile.Close();
        }
        LoadScore();
        int killCount = dsc.numberOFDestroyedDrones;
        string[] roundScore = new string[2];
        userInput = input.text;
        string userInputCut = "";
        if (userInput.Length > 8)
        {
            for (int i = 0; i < 8; i++)
            {
                userInputCut += userInput[i];
            }
        }else{
            userInputCut = userInput;
        }     
        roundScore[0] = userInputCut;
        roundScore[1] = killCount.ToString();
        _scoreList.Add(roundScore);
        _scoreList.Sort((x,y) => Int32.Parse(y[1]).CompareTo(Int32.Parse(x[1]))); // sort list
        string data = "";
        foreach (string[] score in _scoreList)
        {
            data += $"{score[0]};{score[1]}\n";
        }
        File.WriteAllText(_scoreFilePath, data);
        GoToMenu();
    }

    public void GoToMenu()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
    }
}
