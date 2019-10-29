using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Generator : DataProvider
{
    private int MaxT = 20;
    private int MaxP = 500;
    private int MaxZ = 20;
    private int MaxS = 10000;
    public string OutFileName = "test.txt";

    List<DTO> taxis;
    List<DTO> passengers;
    List<DTO> fanZones;

    Random r;

    void Start()
    {
        Init();
	}

    private bool inited = false;

    private void Init()
    {
        if (!inited)
        {
            inited = true;
            Generate();
        }
    }

    public void Generate()
    {
        while (!GenerateTest())
        { }
        WriteToFile();
    }
    
    private bool GenerateTest()
    {
        r = new Random();
        int t = r.Next(1, MaxT + 1);
        int p = r.Next(1, MaxP + 1);
        int z = r.Next(1, MaxZ + 1);
        int s = r.Next(1, MaxS + 1);
        if (t + p + z > (2 * s + 1) * ((2 * s + 1)))
            return false;

        List<DTO> allObjects = new List<DTO>();
        taxis = GenerateObjects(t, s, allObjects);
        passengers = GenerateObjects(p, s, allObjects);
        fanZones = GenerateObjects(z, s, allObjects);

        return true;
    }

    private void WriteToFile()
    {
        string fullFileName = Path.Combine(UnityEngine.Application.dataPath, OutFileName);
        List<string> lines = new List<string>();
        SerializeList(taxis, lines);
        SerializeList(passengers, lines);
        SerializeList(fanZones, lines);
        File.WriteAllLines(fullFileName, lines.ToArray());
    }

    private void SerializeList(List<DTO> list, List<string> lines)
    {
        lines.Add(list.Count.ToString());
        foreach (DTO obj in list)
        {
            lines.Add(obj.X + " " + obj.Y);
        }
    }

    private List<DTO> GenerateObjects(int count, int s, List<DTO> allObjects)
    {
        List<DTO> objects = new List<DTO>();

        while (objects.Count < count)
        {
            DTO newObj = new DTO();
            newObj.Number = objects.Count + 1;
            do
            {
                newObj.X = r.Next(-s, s + 1);
                newObj.Y = r.Next(-s, s + 1);
            }
            while (IsOverOtherObject(newObj, allObjects));
            objects.Add(newObj);
            allObjects.Add(newObj);
        }
        return objects;
    }

    private bool IsOverOtherObject(DTO newObj, List<DTO> allObjects)
    {
        foreach (DTO obj in allObjects)
        {
            if ((obj.X == newObj.X) && (obj.Y == newObj.Y))
            {
                return true;
            }
        }
        return false;
    }

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
}
