using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePawn : Piece
{
    [SerializeField] int moveOffset;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override bool CanMoveTo(int col, int row)
    {
        return base.CanMoveTo(col, row)
            &&  row == this.row + moveOffset && Mathf.Abs(this.column - col) <= 1;
    }
}
