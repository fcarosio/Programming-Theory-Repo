using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePawn : Piece
{
    [SerializeField] int moveOffset;
    private bool firstMove = true;

    // Start is called before the first frame update
    void Start()
    {
        Place();
        moveOffset *= 2;
    }

    // Update is called once per frame
    void Update()
    {
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
