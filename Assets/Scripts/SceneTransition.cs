using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Sceneinstance;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (Sceneinstance != null && Sceneinstance != this)
        {
            Destroy(gameObject);
            return;
        }

        Sceneinstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NextLevel(int whichLevel)
    {
        StartCoroutine(scenetransition(whichLevel));
    }
    IEnumerator scenetransition(int loadlvl)
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(loadlvl);
        animator.SetTrigger("Start");
    }
}
