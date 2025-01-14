using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Uinstance;

    [SerializeField] private GameObject[] panels;
    [SerializeField] private Inputs _inputs;
    [SerializeField] private GameObject GamePanelStuff;
    [SerializeField] private TextMeshProUGUI _lvlInfo;
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private int _levelScore;
    [SerializeField] private int _maxSceneCount;
    public enum Panels
    {
        GamePanel,
        PausePanel,
        FinishPanel,
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
        _maxSceneCount = SceneManager.sceneCountInBuildSettings;
    }

    private void FinishLevel()
    {
        GamePanelStuff.SetActive(false);
        ShowPanel(Panels.FinishPanel);
    }
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % _maxSceneCount;

        SceneTransition.Sceneinstance.NextLevel(nextSceneIndex);
    }

    private void UpdateScoreUI()
    {
        _scoreTxt.text = _score.ToString();
    }

    public void StartGame()
    {
        SceneTransition.Sceneinstance.NextLevel(1);
    }
    public void ReturnMainMenu()
    {
        SceneTransition.Sceneinstance.NextLevel(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        _inputs.SetMouseClickTime();
        ShowPanel(Panels.PausePanel);
    }
    public void ResumeGame()
    {
        ShowPanel(Panels.GamePanel);
        _inputs.SetMouseClickTime();
    }

    public void ReStartGame()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        SceneTransition.Sceneinstance.NextLevel(currentscene);
    }

    void ShowPanel(Panels panel)
    {
        CurrentPanel = panel;

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == (int)panel);
        }
    }
}