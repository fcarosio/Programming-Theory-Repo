using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceQueen : Piece
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override string GetName()
    {
        return "Queen";
    }

    public override List<Board.Position> GetAllowedMovements()
    {
        List<Board.Position> allowedPositions = new List<Board.Position>();
        List<Board.Position> dummy = new List<Board.Position>();

        ScanDirection(-1, 0, allowedPositions, dummy);
        ScanDirection(-1, 1, allowedPositions, dummy);
        ScanDirection(0, 1, allowedPositions, dummy);
        ScanDirection(1, 1, allowedPositions, dummy);
        ScanDirection(1, 0, allowedPositions, dummy);
        ScanDirection(1, -1, allowedPositions, dummy);
        ScanDirection(0, -1, allowedPositions, dummy);
        ScanDirection(-1, -1, allowedPositions, dummy);

        return allowedPositions;
    }

    public override List<Board.Position> GetEatable()
    {
        List<Board.Position> dummy = new List<Board.Position>();
        List<Board.Position> eatable = new List<Board.Position>();

        ScanDirection(-1, 0, dummy, eatable);
        ScanDirection(-1, 1, dummy, eatable);
        ScanDirection(0, 1, dummy, eatable);
        ScanDirection(1, 1, dummy, eatable);
        ScanDirection(1, 0, dummy, eatable);
        ScanDirection(1, -1, dummy, eatable);
        ScanDirection(0, -1, dummy, eatable);
        ScanDirection(-1, -1, dummy, eatable);

        return eatable;
    }

    public override bool MoveTo(Board.Position targetPosition)
    {
        bool isAllowed = base.MoveTo(targetPosition);

        return isAllowed;
    }
}
