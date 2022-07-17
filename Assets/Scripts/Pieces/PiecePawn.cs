using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePawn : Piece
{
    [SerializeField] int moveOffset;
    private bool firstMove = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moveOffset *= 2;
    }

    public override string GetName()
    {
        return "Pawn";
    }

    public override List<Board.Position> GetAllowedMovements()
    {
        List<Board.Position> allowedPositions = new List<Board.Position>();

        int direction = moveOffset >= 0 ? 1 : -1;
        int value = Mathf.Abs(moveOffset);

        for (int i = 1; i <= value; ++i)
        {
            int offset = i * direction;
            Board.Position targetPosition = new Board.Position(position.Column, position.Row + offset);

            allowedPositions.Add(targetPosition);
        }

        return allowedPositions;
    }

    public override List<Board.Position> GetEatable()
    {
        List<Board.Position> eatable = new List<Board.Position>();

        int direction = moveOffset >= 0 ? 1 : -1;
        Board.Position targetPosition = new Board.Position(position.Column - 1, position.Row + direction);
        CheckEatable(targetPosition, eatable);
        targetPosition = new Board.Position(position.Column + 1, position.Row + direction);
        CheckEatable(targetPosition, eatable);

        return eatable;
    }

    public override bool MoveTo(Board.Position targetPosition)
    {
        bool isAllowed = base.MoveTo(targetPosition);
        if (firstMove)
        {
            firstMove = false;
            moveOffset /= 2;
        }

        return isAllowed;
    }
}
