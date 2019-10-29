using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgMovesProvider : MovesProvider
{
    public DataProvider Reader;
    public AlgBase Alg;

    public override List<Move> GetMoves()
    {
        return Alg.Calculate(Reader.GetTaxis(), Reader.GetPassengers(), Reader.GetFanZones());
    }
}
