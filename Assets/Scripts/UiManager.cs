using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Uinstance;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] TextMeshProUGUI _lvlInfo;
    [SerializeField] TextMeshProUGUI _scoreTxt;
    [SerializeField] int _levelScore;

    private void Awake()
    {
        if (Uinstance == null)
        {
            Uinstance = this;
        }
        else return;
    }
    private void OnEnable()
    {
        //_lvlInfo.text =
    }

    public enum Panels
    {
        GamePanel,
        PausePanel,
    }

    public Panels CurrentPanel;

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
                ChangeScene();
            }
        }
    }

    private void ChangeScene()
    {
        
    }

    private void UpdateScoreUI()
    {
        _scoreTxt.text = _score.ToString();
    }
    public void StartGame()
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

        switch (panel)
        {
            case Panels.GamePanel:
                _gamePanel.SetActive(true);
                _pausePanel.SetActive(false);
                break;

            case Panels.PausePanel:
                _gamePanel.SetActive(false);
                _pausePanel.SetActive(true);
                break;
        }
    }

    public void TogglePause()
    {
        if (CurrentPanel == Panels.GamePanel)
        {
            ShowPanel(Panels.PausePanel);
        }
        else
        {
            ShowPanel(Panels.GamePanel);
        }
    }
}