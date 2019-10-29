using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlgBase : MonoBehaviour
{
	public List<Move> Calculate(List<DTO> taxis, List<DTO> passengers, List<DTO> fanZones)
    {
        List<TaxiDTO> taxiDTOs = new List<TaxiDTO>();
        foreach (DTO t in taxis)
        {
            TaxiDTO tDTO = new TaxiDTO();
            t.CopyTo(tDTO);
            taxiDTOs.Add(tDTO);
        }

        List<PassDTO> passDTOs = new List<PassDTO>();
        foreach (DTO p in passengers)
        {
            PassDTO pDTO = new PassDTO();
            p.CopyTo(pDTO);
            passDTOs.Add(pDTO);
        }

        List<FanZoneDTO> zoneDTOs = new List<FanZoneDTO>();
        foreach (DTO z in fanZones)
        {
            FanZoneDTO zDTO = new FanZoneDTO();
            z.CopyTo(zDTO);
            zoneDTOs.Add(zDTO);
        }

        return CalcImpl(taxiDTOs, passDTOs, zoneDTOs);
    }

    protected virtual List<Move> CalcImpl(List<TaxiDTO> taxis, List<PassDTO> passengers, List<FanZoneDTO> fanZones)
    {
        return new List<Move>();
    }

    protected float DistanceSquared(DTO obj1, DTO obj2)
    {
        return (((obj1.X - obj2.X) * (obj1.X - obj2.X)) +
            ((obj1.Y - obj2.Y) * (obj1.Y - obj2.Y)));
    }

    protected bool CoordEquals(DTO obj1, DTO obj2)
    {
        return ((obj1.X == obj2.X) && (obj1.Y == obj2.Y));
    }

    protected void ApplyMove(Move move, List<TaxiDTO> taxis,
        List<PassDTO> passengers, List<FanZoneDTO> fanZones)
    {
        foreach (int tIndex in move.TaxiNumbers)
        {
            taxis[tIndex - 1].X = taxis[tIndex - 1].X + move.OffsetX;
            taxis[tIndex - 1].Y = taxis[tIndex - 1].Y + move.OffsetY;
            int passIndex = taxis[tIndex - 1].PassNumber - 1;
            if (passIndex >= 0)
            {
                passengers[passIndex].X = taxis[tIndex - 1].X;
                passengers[passIndex].Y = taxis[tIndex - 1].Y;
                foreach (FanZoneDTO zone in fanZones)
                {
                    if (CoordEquals(passengers[passIndex], zone))
                    {
                        taxis[tIndex - 1].PassNumber = 0;
                        passengers[passIndex].IsInZone = true;
                        break;
                    }
                }
            }
            else
            {
                foreach (PassDTO p in passengers)
                {
                    if (CoordEquals(taxis[tIndex - 1], p))
                    {
                        taxis[tIndex - 1].PassNumber = p.Number;
                        break;
                    }
                }
            }
        }
    }

    protected class TaxiDTO : DTO
    {
        public int PassNumber = 0;
    }

    protected class PassDTO : DTO
    {
        public bool IsInZone = false;
    }

    protected class FanZoneDTO : DTO
    {

    }
}
