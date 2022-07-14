using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceRook : Piece
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override string GetName()
    {
        return "Rook";
    }

    public override List<Board.Position> GetAllowedMovements()
    {
        List<Board.Position> allowedPositions = new List<Board.Position>();

        CheckDirection(-1, 0, allowedPositions);
        CheckDirection(1, 0, allowedPositions);
        CheckDirection(0, -1, allowedPositions);
        CheckDirection(0, 1, allowedPositions);

        return allowedPositions;
    }

    public override bool MoveTo(Board.Position targetPosition)
    {
        bool isAllowed = base.MoveTo(targetPosition);

        return isAllowed;
    }
}
