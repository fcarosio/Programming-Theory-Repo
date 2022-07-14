using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceKing : Piece
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override string GetName()
    {
        return "King";
    }

    void CheckNext(int colOffset, int rowOffset, List<Board.Position> positions)
    {
        int c = position.Column + colOffset;
        int r = position.Row + rowOffset;

        Board.Position targetPosition = new Board.Position(c, r);
        if (board.IsCellValidAndFree(targetPosition))
        {
            positions.Add(targetPosition);
        }
    }

    public override List<Board.Position> GetAllowedMovements()
    {
        List<Board.Position> allowedPositions = new List<Board.Position>();

        CheckNext(-1, 1, allowedPositions);
        CheckNext(0, 1, allowedPositions);
        CheckNext(1, 1, allowedPositions);
        CheckNext(1, 0, allowedPositions);
        CheckNext(1, -1, allowedPositions);
        CheckNext(0, -1, allowedPositions);
        CheckNext(-1, -1, allowedPositions);
        CheckNext(-1, 0, allowedPositions);

        return allowedPositions;
    }

    public override bool MoveTo(Board.Position targetPosition)
    {
        bool isAllowed = base.MoveTo(targetPosition);

        return isAllowed;
    }
}
