using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScreenUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] Image playerColor;
    [SerializeField] TextMeshProUGUI pieceName;
    [SerializeField] TextMeshProUGUI piecePosition;

    private Color[] PLAYER_COLORS = new Color[] { Color.white, Color.black };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowPlayer(GameManager.PlayerData playerData)
    {
        playerName.text = playerData.GetName();
        playerColor.color = PLAYER_COLORS[(int)(playerData.GetColor())];
    }

    public void ShowPiece(Piece piece)
    {
        if (piece != null)
        {
            pieceName.text = piece.GetName();
            int col = piece.GetPosition().Column + 1;
            int row = piece.GetPosition().Row + 1;
            piecePosition.text = "[" + col + "," + row + "]";
        }
        else
        {
            pieceName.text = "";
            piecePosition.text = "";
        }
    }
}
