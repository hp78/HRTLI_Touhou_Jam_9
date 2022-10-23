using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartStage1()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void StartStage2()
    {
        SceneManager.LoadScene("Stage 2");
    }

    public void StartStage3()
    {
        SceneManager.LoadScene("Stage 3");
    }



    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
