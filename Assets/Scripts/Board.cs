using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public class Position
    {
        public int Column { get; set; }
        public int Row { get; set; }

        public Position(int column, int row)
        {
            Column = column;
            Row = row;
        }
    }

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

    public Piece[,] pieces = new Piece[N_CELLS, N_CELLS];

    // Start is called before the first frame update
    void Start()
    {
        ResetBoard();
    }

    public void ResetBoard()
    {
        for (int i = 0; i < N_CELLS; ++i)
        {
            for (int j = 0; j < N_CELLS; ++j)
            {
                pieces[i, j] = null;
            }
        }

        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("Piece"))
        {
            Destroy(piece);
        }

        AddWhiteTeam();
        AddBlackTeam();
    }

    private bool IsAllowed(int pos)
    {
        return pos >= 0 && pos < N_CELLS;
    }

    private bool IsAllowed(Board.Position position)
    {
        return IsAllowed(position.Column) && IsAllowed(position.Row);
    }

    public bool IsFree(Board.Position position)
    {
        return IsAllowed(position) && pieces[position.Column, position.Row] == null;
    }

    public bool IsBusy(Board.Position position)
    {
        return IsAllowed(position) && pieces[position.Column, position.Row] != null;
    }

    public bool IsBusy(Board.Position position, PlayerSettings.PlayerColor color)
    {
        return IsBusy(position) ? GetAt(position).GetColor() == color : false;
    }

    public Piece GetAt(Board.Position position)
    {
        return IsAllowed(position) ? pieces[position.Column, position.Row] : null;
    }

    public void MovePiece(Position from, Position to)
    {
        Piece p = pieces[from.Column, from.Row];
        pieces[from.Column, from.Row] = null;
        pieces[to.Column, to.Row] = p;
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

    void AddPiece(GameObject pieceObj, int col, int row, float angle)
    {
        Vector3 position = ToCoords(col, row);
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Instantiate(pieceObj, position, rotation);

        Piece piece = pieceObj.GetComponent<Piece>();
        pieces[col, row] = piece;
    }

    static float ToCoord(int pos)
    {
        return (pos - ((float)(N_CELLS - 1)) * 0.5f) * STEP;
    }

    static int ToPosition(float coord)
    {
        return Mathf.RoundToInt(coord / STEP + (N_CELLS - 1) * 0.5f);
    }

    public static Vector3 ToCoords(int col, int row)
    {
        return new Vector3(ToCoord(col), BOARD_Y, ToCoord(row));
    }

    public static Vector3 ToCoords(Position position)
    {
        return ToCoords(position.Column, position.Row);
    }

    public static Position ToPosition(float x, float y, float z)
    {
        return new Position(ToPosition(x), ToPosition(z));
    }

    public static Position ToPosition(Vector3 position)
    {
        return ToPosition(position.x, position.y, position.z);
    }
}
