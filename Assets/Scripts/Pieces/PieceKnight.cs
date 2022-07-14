using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceKnight : Piece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string GetName()
    {
        return "Knight";
    }

    public override List<Board.Position> GetAllowedMovements()
    {
        List<Board.Position> allowedPositions = new List<Board.Position>();

        return allowedPositions;
    }

    public override bool MoveTo(Board.Position targetPosition)
    {
        bool isAllowed = base.MoveTo(targetPosition);

        return isAllowed;
    }
}
