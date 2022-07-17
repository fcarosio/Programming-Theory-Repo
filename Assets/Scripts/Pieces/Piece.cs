using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] PlayerSettings.PlayerColor color;
    [SerializeField] protected Board.Position position;

    protected Board board;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Place();
    }

    public PlayerSettings.PlayerColor GetColor()
    {
        return color;
    }

    public Board.Position GetPosition()
    {
        return position;
    }

    protected void Place()
    {
        board = GameObject.Find("Game Board").GetComponent<Board>();
        position = Board.ToPosition(gameObject.transform.position);
    }

    void MovePiece(Board.Position targetPosition)
    {
        board.MovePiece(position, targetPosition);

        position.Column = targetPosition.Column;
        position.Row = targetPosition.Row;
        gameObject.transform.position = Board.ToCoords(position);
    }

    public virtual bool MoveTo(Board.Position targetPosition)
    {
        List<Board.Position> availablePositions = GetAllowedMovements();
        bool isAllowed = board.IsCellValidAndFree(targetPosition)
            && availablePositions.Find(p => p.Column == targetPosition.Column && p.Row == targetPosition.Row) != null;

        if (isAllowed)
        {
            MovePiece(targetPosition);
        }
        return isAllowed;
    }

    void Capture(Piece piece)
    {
        piece.gameObject.transform.Translate(Vector3.up * -10.0f);
        piece.gameObject.SetActive(false);
        piece.gameObject.tag = "Captured";
    }

    public bool Eat(Piece piece)
    {
        List<Board.Position> eatable = GetEatable();

        bool isEatable = board.IsCellValidAndBusy(piece.GetPosition())
            && eatable.Find(p => p.Column == piece.GetPosition().Column && p.Row == piece.GetPosition().Row) != null;
        
        if (isEatable && piece.GetColor() != this.GetColor())
        {
            Capture(piece);

            MovePiece(piece.GetPosition());

            return true;
        }

        return false;
    }

    public virtual string GetName()
    {
        return "Piece";
    }

    public virtual List<Board.Position> GetAllowedMovements()
    {
        return new List<Board.Position>();
    }

    protected void CheckEatable(Board.Position targetPosition, List<Board.Position> positions)
    {
        GameManager gameMng = GameManager.Instance;
        GameManager.PlayerData playerData = gameMng.GetCurrentPlayer();

        Piece piece = board.GetAt(targetPosition);
        if (piece != null && piece.GetColor() == PlayerSettings.GetOther(playerData.GetColor()))
        {
            positions.Add(targetPosition);
        }
    }

    public virtual List<Board.Position> GetEatable()
    {
        return new List<Board.Position>();
    }

    protected void CheckDirection(int colOffset, int rowOffset, List<Board.Position> positions)
    {
        int c = position.Column + colOffset;
        int r = position.Row + rowOffset;

        while (c >= 0 && c < Board.N_CELLS && r >= 0 && r < Board.N_CELLS)
        {
            Board.Position targetPosition = new Board.Position(c, r);
            if (board.IsCellValidAndFree(targetPosition))
            {
                positions.Add(targetPosition);
            }
            else
            {
                break;
            }

            c += colOffset;
            r += rowOffset;
        }
    }
}
