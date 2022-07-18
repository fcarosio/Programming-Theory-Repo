using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public class PlayerData
    {
        private string name;
        public PlayerSettings.PlayerColor Color { get; private set; }
        public int TimeCount { get; set; }

        public PlayerData(string name, PlayerSettings.PlayerColor color)
        {
            this.name = name;
            Color = color;
            TimeCount = 0;
        }

        public string GetName()
        {
            return name;
        }
    }

    private static GameManager instance = null;

    public static GameManager Instance
    {
        get { return instance; }
    }

    public bool GameActive { get; private set; }
    public PlayerData CurrentPlayer { get; private set; }

    private Board board;

    private PlayerData[] players = new PlayerData[PlayerSettings.NUM_COLORS];

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void AddPlayer(string name, PlayerSettings.PlayerColor color)
    {
        PlayerData playerData = new PlayerData(name, color);
        int index = (int)playerData.Color;
        players[index] = playerData;
    }

    private void TurnTo(PlayerData player)
    {
        CurrentPlayer = player;

        CameraController cameraCtrl = Camera.main.GetComponent<CameraController>();
        cameraCtrl.SetViewFor(CurrentPlayer);
    }

    public void StartGame()
    {
        board = GameObject.Find("Game Board").GetComponent<Board>();
        board.ResetBoard();

        GameActive = true;

        TurnTo(players[0]);
    }

    public void GameOver()
    {
        GameActive = false;
    }

    public void NextTurn()
    {
        int newIndex = ((int)CurrentPlayer.Color + 1) % PlayerSettings.NUM_COLORS;
        TurnTo(players[newIndex]);
    }
}
