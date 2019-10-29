using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputMovesProvider : MovesProvider
{
    public string MovesFileName = "output.txt";

    public override List<Move> GetMoves()
    {
        Init();
        return moves;
    }

    private void Init()
    {
        if (!inited)
        {
            inited = true;

            string fullMovesFileName = Path.Combine(Application.dataPath, MovesFileName);
            string[] moveLines = File.ReadAllLines(fullMovesFileName);
            moves = ReadMoves(moveLines);
        }
    }

    private List<Move> ReadMoves(string[] moveLines)
    {
        int count = int.Parse(moveLines[0]);
        List<Move> result = new List<Move>();
        for (int i = 1; i <= count; i++)
        {
            Move newMove = ReadMove(moveLines[i]);
            result.Add(newMove);
        }
        return result;
    }

    private Move ReadMove(string line)
    {
        string[] lineParts = line.Split(' ');
        Move newMove = new Move();
        newMove.OffsetX = int.Parse(lineParts[1]);
        newMove.OffsetY = int.Parse(lineParts[2]);
        int taxiCount = int.Parse(lineParts[3]);
        newMove.TaxiNumbers = new List<int>();
        for (int i = 0; i < taxiCount; i++)
        {
            newMove.TaxiNumbers.Add(int.Parse(lineParts[4 + i]));
        }
        return newMove;
    }

    private bool inited = false;
    private List<Move> moves;

}
