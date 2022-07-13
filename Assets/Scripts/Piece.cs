using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] PlayerSettings.PlayerColor color;

    protected int column;
    protected int row;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private bool IsAllowed(int pos)
    {
        return pos >= 0 && pos < Board.N_CELLS;
    }

    public virtual bool CanMoveTo(int col, int row)
    {
        return IsAllowed(col) && IsAllowed(row);
    }

    protected bool MoveTo(int col, int row)
    {
        bool isAllowed = CanMoveTo(col, row);
        if (isAllowed)
        {
            this.row = row;
            this.column = col;
            gameObject.transform.position = Board.ToPosition(this.column, this.row);
        }
        return isAllowed;
    }
}
