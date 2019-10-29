using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NearestNeighborAlg : AlgBase
{

    protected override List<Move> CalcImpl(List<TaxiDTO> taxis, 
        List<PassDTO> passengers, List<FanZoneDTO> fanZones)
    {
        List<Move> moves = new List<Move>();
        int notInZonesCount = passengers.Count;
        do
        {
            List<Move> newMoves1 = SearchPass(taxis, passengers, fanZones);
            foreach (Move newMove1 in newMoves1)
            {
                ApplyMove(newMove1, taxis, passengers, fanZones);
                moves.Add(newMove1);
            }
            List<Move> newMoves2 = FlyToZone(taxis, passengers, fanZones);
            foreach (Move newMove2 in newMoves2)
            {
                if (newMove2 != null)
                {
                    ApplyMove(newMove2, taxis, passengers, fanZones);
                    moves.Add(newMove2);
                }
            }
            notInZonesCount--;
        }
        while (notInZonesCount > 0);
        return moves;
    }

    private List<Move> SearchPass(List<TaxiDTO> taxis,
        List<PassDTO> passengers, List<FanZoneDTO> fanZones)
    {
        float minDistance = float.MaxValue;
        int passIndex = 0;
        int taxiIndex = 0;
        for (int i = 0; i < taxis.Count; i++)
        {
            if (taxis[i].PassNumber > 0)
            {
                continue;
            }
            for (int j = 0; j < passengers.Count; j++)
            {
                if (passengers[j].IsInZone)
                {
                    continue;
                }
                float d = DistanceSquared(taxis[i], passengers[j]);
                if (d < minDistance)
                {
                    minDistance = d;
                    taxiIndex = i;
                    passIndex = j;
                }
            }
        }
        List<Move> newMoves = new List<Move>();
        Move newMove1 = new Move();
        newMove1.OffsetX = passengers[passIndex].X - taxis[taxiIndex].X;
        newMove1.OffsetY = passengers[passIndex].Y - taxis[taxiIndex].Y;
        newMove1.TaxiNumbers = new List<int>();
        newMove1.TaxiNumbers.Add(taxis[taxiIndex].Number);
        newMoves.Add(newMove1);
        return newMoves;
    }

    private List<Move> FlyToZone(List<TaxiDTO> taxis,
        List<PassDTO> passengers, List<FanZoneDTO> fanZones)
    {
        TaxiDTO t = taxis.FirstOrDefault(c => c.PassNumber > 0);
        if (t != null)
        {
            float minDistance = float.MaxValue;
            int zoneIndex = 0;
            foreach (FanZoneDTO z in fanZones)
            {
                float d = DistanceSquared(t, z);
                if (d < minDistance)
                {
                    zoneIndex = z.Number - 1;
                    minDistance = d;
                }
            }

            List<Move> newMoves = new List<Move>();
            for (int i = 0; i < taxis.Count; i++)
            {
                if (CoordEquals(taxis[i], fanZones[zoneIndex]))
                {

                }
            }
            Move newMove1 = new Move();
            newMove1.OffsetX = fanZones[zoneIndex].X - t.X;
            newMove1.OffsetY = fanZones[zoneIndex].Y - t.Y;
            newMove1.TaxiNumbers = new List<int>();
            newMove1.TaxiNumbers.Add(t.Number);
            newMoves.Add(newMove1);
            return newMoves;
        }
        return null;
    }

}
