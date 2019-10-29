using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyMovesProvider : MovesProvider
{
    public override List<Move> GetMoves()
    {
        return new List<Move>();
    }
}
