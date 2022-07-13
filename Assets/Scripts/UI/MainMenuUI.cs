using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button startGame;
    [SerializeField] Button quitGame;

    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(StartGame);
        quitGame.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
