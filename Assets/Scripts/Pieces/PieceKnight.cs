using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceKnight : Piece
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override string GetName()
    {
        return "Knight";
    }

    void CheckL(int colOffset, int rowOffset, List<Board.Position> positions)
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

        CheckL(-1, 2, allowedPositions);
        CheckL(1, 2, allowedPositions);
        CheckL(2, 1, allowedPositions);
        CheckL(2, -1, allowedPositions);
        CheckL(-1, -2, allowedPositions);
        CheckL(1, -2, allowedPositions);
        CheckL(-2, -1, allowedPositions);
        CheckL(-2, 1, allowedPositions);

        return allowedPositions;
    }

    public override bool MoveTo(Board.Position targetPosition)
    {
        bool isAllowed = base.MoveTo(targetPosition);

        return isAllowed;
    }
}
