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

    void ScanL(int colOffset, int rowOffset, List<Board.Position> movable, List<Board.Position> eatable)
    {
        int c = position.Column + colOffset;
        int r = position.Row + rowOffset;
        PlayerSettings.PlayerColor oppositeColor = PlayerSettings.GetOther(GameManager.Instance.CurrentPlayer.Color);

        Board.Position targetPosition = new Board.Position(c, r);
        if (board.IsFree(targetPosition))
        {
            movable.Add(targetPosition);
        }
        else if (board.IsBusy(targetPosition))
        {
            if (board.GetAt(targetPosition).GetColor() == oppositeColor)
            {
                eatable.Add(targetPosition);
            }
        }
    }

    public override List<Board.Position> GetAllowedMovements()
    {
        List<Board.Position> allowedPositions = new List<Board.Position>();
        List<Board.Position> dummy = new List<Board.Position>();

        ScanL(-1, 2, allowedPositions, dummy);
        ScanL(1, 2, allowedPositions, dummy);
        ScanL(2, 1, allowedPositions, dummy);
        ScanL(2, -1, allowedPositions, dummy);
        ScanL(-1, -2, allowedPositions, dummy);
        ScanL(1, -2, allowedPositions, dummy);
        ScanL(-2, -1, allowedPositions, dummy);
        ScanL(-2, 1, allowedPositions, dummy);

        return allowedPositions;
    }

    public override List<Board.Position> GetEatable()
    {
        List<Board.Position> dummy = new List<Board.Position>();
        List<Board.Position> eatable = new List<Board.Position>();

        ScanL(-1, 2, dummy, eatable);
        ScanL(1, 2, dummy, eatable);
        ScanL(2, 1, dummy, eatable);
        ScanL(2, -1, dummy, eatable);
        ScanL(-1, -2, dummy, eatable);
        ScanL(1, -2, dummy, eatable);
        ScanL(-2, -1, dummy, eatable);
        ScanL(-2, 1, dummy, eatable);

        return eatable;
    }

    public override bool MoveTo(Board.Position targetPosition)
    {
        bool isAllowed = base.MoveTo(targetPosition);

        return isAllowed;
    }
}
