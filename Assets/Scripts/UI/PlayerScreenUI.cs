using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScreenUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI pieceName;
    [SerializeField] TextMeshProUGUI piecePosition;

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
    }

    public void ShowPiece(Piece piece)
    {
        pieceName.text = piece.GetName();
        int col = piece.GetPosition().Column + 1;
        int row = piece.GetPosition().Row + 1;
        piecePosition.text = "[" + col + "," + row + "]";
    }
}
