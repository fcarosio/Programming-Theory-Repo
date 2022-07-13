using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] PlayerSettings[] players;
    [SerializeField] Button accept;

    // Start is called before the first frame update
    void Start()
    {
        accept.onClick.AddListener(Accept);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Accept()
    {
        GameManager gameMng = GameManager.Instance;
        gameMng.AddPlayer(players[0].GetName(), players[0].GetColor());
        gameMng.AddPlayer(players[1].GetName(), players[1].GetColor());
        SceneManager.LoadScene(2);
    }
}
