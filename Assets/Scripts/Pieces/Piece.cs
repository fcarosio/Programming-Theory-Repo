using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] PlayerSettings.PlayerColor color;
    [SerializeField] protected Board.Position position;

    protected Board board;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
}
