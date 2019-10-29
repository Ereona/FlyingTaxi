using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputDataProvider : DataProvider
{
    public string DataFileName = "input.txt";

    public override List<DTO> GetTaxis()
    {
        Init();
        return taxis;
    }

    public override List<DTO> GetPassengers()
    {
        Init();
        return passengers;
    }

    public override List<DTO> GetFanZones()
    {
        Init();
        return fanZones;
    }

    private void Init()
    {
        if (!inited)
        {
            inited = true;

            string fullDataFileName = Path.Combine(Application.dataPath, DataFileName);
            string[] dataLines = File.ReadAllLines(fullDataFileName);
            int index = 0;
            taxis = ReadObjects(dataLines, ref index);
            passengers = ReadObjects(dataLines, ref index);
            fanZones = ReadObjects(dataLines, ref index);
        }
    }

    private List<DTO> ReadObjects(string[] dataLines, ref int index)
    {
        int count = int.Parse(dataLines[index]);
        index++;
        List<DTO> result = new List<DTO>();
        for (int i = 0; i < count; i++)
        {
            string[] xy = dataLines[index + i].Split(' ');
            DTO obj = new DTO();
            obj.Number = i + 1;
            obj.X = int.Parse(xy[0]);
            obj.Y = int.Parse(xy[1]);
            result.Add(obj);
        }
        index += count;
        return result;
    }

    private bool inited = false;
    private List<DTO> taxis;
    private List<DTO> passengers;
    private List<DTO> fanZones;
}
