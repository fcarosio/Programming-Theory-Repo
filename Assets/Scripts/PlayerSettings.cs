using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    private static string[] PLAYER_COLOR_NAMES = new string[] { "White", "Black" };
    private static Color[] PLAYER_COLORS = new Color[] { Color.white, Color.black };

    public static PlayerColor GetOther(PlayerColor color)
    {
        int newColorIndex = (((int)color) + 1) % NUM_COLORS;
        return (PlayerColor)newColorIndex;
    }

    public static int NUM_COLORS { get { return PLAYER_COLORS.Length; } }

    public enum PlayerColor { White = 0, Black };

    [SerializeField] TMP_InputField playerName;
    [SerializeField] PlayerColor defaultPlayerColor;
    [SerializeField] Button whiteColor;
    [SerializeField] Button blackColor;
    [SerializeField] GameObject[] colorIndicators;
    [SerializeField] TextMeshProUGUI selectedPlayerColor;
    [SerializeField] PlayerSettings otherPlayer;

    private PlayerColor playerColor;

    // Start is called before the first frame update
    void Start()
    {
        SetColor(defaultPlayerColor);

        whiteColor.onClick.AddListener(SetWhite);
        blackColor.onClick.AddListener(SetBlack);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetColor(PlayerColor color)
    {
        playerColor = color;
        int colorIndex = (int)color;
        colorIndicators[colorIndex].SetActive(true);

        PlayerColor otherColor = GetOther(color);
        int otherColorIndex = (int)GetOther(color);
        colorIndicators[otherColorIndex].SetActive(false);

        selectedPlayerColor.text = PLAYER_COLOR_NAMES[colorIndex];
    }

    void SetWhite()
    {
        PlayerColor color = PlayerColor.White;
        SetColor(color);
        otherPlayer.SetColor(GetOther(color));
    }

    void SetBlack()
    {
        PlayerColor color = PlayerColor.Black;
        SetColor(color);
        otherPlayer.SetColor(GetOther(color));
    }

    public string GetName()
    {
        string name = playerName.text;
        if (string.IsNullOrEmpty(name))
        {
            int num = ((int)playerColor) + 1;
            name = string.Format("Player {0}", num);
        }
        return name;
    }

    public PlayerColor GetColor()
    {
        return playerColor;
    }
}
