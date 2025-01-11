using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Uinstance;

    [SerializeField] private GameObject[] panels;
    [SerializeField] private TextMeshProUGUI _lvlInfo;
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private int _levelScore;

    private void Awake()
    {
        if (Uinstance == null)
        {
            Uinstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ShowPanel(Panels.GamePanel);
    }
    public enum Panels
    {
        GamePanel,
        PausePanel,
        FinishPanel
    }

    public Panels CurrentPanel { get; private set; }

    private int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            UpdateScoreUI();
            if (_score >= _levelScore)
            {
                FinishLevel();
            }
        }
    }

    private void FinishLevel()
    {
        ShowPanel(Panels.FinishPanel);
    }
    public void NextLevel()
    {
        ShowPanel(Panels.FinishPanel);
    }

    private void UpdateScoreUI()
    {
        _scoreTxt.text = _score.ToString();
    }

    public void StartGame()
    {
        
    }
    public void ReturnMainMenu()
    {
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReStartGame()
    {
        
    }

    public void ShowPanel(Panels panel)
    {
        CurrentPanel = panel;

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == (int)panel);
        }
    }
}