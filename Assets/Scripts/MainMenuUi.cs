using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUi : MonoBehaviour
{
    public void StartGame()
    {
        SceneTransition.Sceneinstance.NextLevel(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
