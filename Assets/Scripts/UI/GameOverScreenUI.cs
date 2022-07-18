using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreenUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winner;
    [SerializeField] Button restartGame;

    // Start is called before the first frame update
    void Start()
    {
        restartGame.onClick.AddListener(RestartGame);
    }

    public void SetWinner(GameManager.PlayerData playerData)
    {
        gameObject.SetActive(true);
        winner.text = playerData.GetName() + " wins!";
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
