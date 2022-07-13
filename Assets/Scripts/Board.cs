using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("White Team")]
    [SerializeField] GameObject whiteKing;
    [SerializeField] GameObject whiteQueen;
    [SerializeField] GameObject whiteBishop;
    [SerializeField] GameObject whiteKnight;
    [SerializeField] GameObject whiteRook;
    [SerializeField] GameObject whitePawn;

    [Header("Black Team")]
    [SerializeField] GameObject blackKing;
    [SerializeField] GameObject blackQueen;
    [SerializeField] GameObject blackBishop;
    [SerializeField] GameObject blackKnight;
    [SerializeField] GameObject blackRook;
    [SerializeField] GameObject blackPawn;

    public static int N_CELLS = 8;
    private const float BOARD_Y = 1.2f;
    private const float STEP = 2.56f;
    public static float FORWARD = 0.0f;
    public static float REVERSE = 180.0f;

    // Start is called before the first frame update
    void Start()
    {
        ResetBoard();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetBoard()
    {
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("Piece"))
        {
            Destroy(piece);
        }

        AddWhiteTeam();
        AddBlackTeam();
    }

    void AddWhiteTeam()
    {
        AddTeam(whiteKing, whiteQueen, whiteBishop, whiteKnight, whiteRook, whitePawn, 0, 1, FORWARD);
    }

    void AddBlackTeam()
    {
        AddTeam(blackKing, blackQueen, blackBishop, blackKnight, blackRook, blackPawn, 7, 6, REVERSE);
    }

    void AddTeam(GameObject king, GameObject queen, GameObject bishop, GameObject knight, GameObject rook, GameObject pawn,
        int backRow, int frontRow, float rotation)
    {
        AddPiece(rook, 0, backRow, rotation);
        AddPiece(knight, 1, backRow, rotation);
        AddPiece(bishop, 2, backRow, rotation);
        AddPiece(queen, 3, backRow, rotation);
        AddPiece(king, 4, backRow, rotation);
        AddPiece(bishop, 5, backRow, rotation);
        AddPiece(knight, 6, backRow, rotation);
        AddPiece(rook, 7, backRow, rotation);
        for (int i = 0; i < N_CELLS; ++i)
        {
            AddPiece(pawn, i, frontRow, rotation);
        }
    }

    void AddPiece(GameObject piece, int col, int row, float angle)
    {
        Vector3 position = ToPosition(col, row);
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Instantiate(piece, position, rotation);
    }

    static float ToCoord(int pos)
    {
        return (pos - (N_CELLS * 0.5f - 0.5f)) * STEP;
    }

    public static Vector3 ToPosition(int col, int row)
    {
        return new Vector3(ToCoord(col), BOARD_Y, ToCoord(row));
    }
}
