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

    public virtual bool MoveTo(Board.Position targetPosition)
    {
        List<Board.Position> availablePositions = GetAllowedMovements();
        bool isAllowed = board.IsCellValidAndFree(targetPosition)
            && availablePositions.Find(p => p.Column == targetPosition.Column && p.Row == targetPosition.Row) != null;

        if (isAllowed)
        {
            board.MovePiece(position, targetPosition);

            position.Column = targetPosition.Column;
            position.Row = targetPosition.Row;
            gameObject.transform.position = Board.ToCoords(position);
        }
        return isAllowed;
    }

    public virtual string GetName()
    {
        return "Piece";
    }

    public virtual List<Board.Position> GetAllowedMovements()
    {
        return null;
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
