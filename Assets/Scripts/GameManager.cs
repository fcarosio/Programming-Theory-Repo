using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public class PlayerData
    {
        private string name;
        private PlayerSettings.PlayerColor color;

        public PlayerData(string name, PlayerSettings.PlayerColor color)
        {
            this.name = name;
            this.color = color;
        }

        public string GetName()
        {
            return name;
        }

        public PlayerSettings.PlayerColor GetColor()
        {
            return color;
        }
    }

    private static GameManager instance = null;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private Board board;
    private PlayerData currentPlayer;

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
        board = GameObject.Find("Board").GetComponent<Board>();
        board.ResetBoard();

        currentPlayer = players[0];

        CameraController cameraCtrl = Camera.main.GetComponent<CameraController>();
        cameraCtrl.SetViewFor(currentPlayer);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddPlayer(string name, PlayerSettings.PlayerColor color)
    {
        PlayerData playerData = new PlayerData(name, color);
        int index = (int)playerData.GetColor();
        players[index] = playerData;
    }

    public void StartGame()
    {
    }
}
