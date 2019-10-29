using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public DataProvider Reader;
    public MovesProvider MoveReader;
    public LerpController Vis;
    public Scaler ScaleProvider;

    public GameObject TaxiPrefab;
    public GameObject PassengerPrefab;
    public GameObject FanZonePrefab;

    private List<Taxi> taxis = new List<Taxi>();
    private List<Passenger> passengers = new List<Passenger>();
    private List<FanZone> fanZones = new List<FanZone>();
    private List<Move> moves;
    private int moveIndex = 0;

    private bool continued;
    private List<Taxi> MovingTaxis = new List<Taxi>();
	
	void Start()
    {
        Init();
	}

    private void Init()
    {
        CreateTaxis();
        CreatePassengers();
        CreateFanZones();
        moves = MoveReader.GetMoves();
        List<NumberedObject> allObjects = new List<NumberedObject>();
        if (ScaleProvider != null)
        {
            allObjects.AddRange(taxis.Cast<NumberedObject>());
            allObjects.AddRange(passengers.Cast<NumberedObject>());
            allObjects.AddRange(fanZones.Cast<NumberedObject>());
            float scale = ScaleProvider.CalcScale(allObjects);
            foreach (NumberedObject obj in allObjects)
            {
                obj.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }

    private void RemoveObjects()
    {
        foreach (Taxi taxi in taxis)
        {
            Destroy(taxi.gameObject);
        }
        taxis.Clear();
        foreach (Passenger pass in passengers)
        {
            Destroy(pass.gameObject);
        }
        passengers.Clear();
        foreach (FanZone zone in fanZones)
        {
            Destroy(zone.gameObject);
        }
        fanZones.Clear();
        MovingTaxis = new List<Taxi>();
    }

    private void CreateTaxis()
    {
        List<DTO> taxiObjects = Reader.GetTaxis();
        foreach (DTO obj in taxiObjects)
        {
            GameObject newTaxi = Instantiate(TaxiPrefab, transform);
            newTaxi.name = "Taxi" + obj.Number;
            Taxi taxiComp = newTaxi.GetComponent<Taxi>();
            taxiComp.FillFromDTO(obj);
            taxis.Add(taxiComp);
        }
    }

    private void CreatePassengers()
    {
        List<DTO> passObjects = Reader.GetPassengers();
        foreach (DTO obj in passObjects)
        {
            GameObject newPassenger = Instantiate(PassengerPrefab, transform);
            newPassenger.name = "Passenger" + obj.Number;
            Passenger passComp = newPassenger.GetComponent<Passenger>();
            passComp.FillFromDTO(obj);
            passengers.Add(passComp);
        }
    }

    private void CreateFanZones()
    {
        List<DTO> fanZoneObjects = Reader.GetFanZones();
        foreach (DTO obj in fanZoneObjects)
        {
            GameObject newFanZone = Instantiate(FanZonePrefab, transform);
            newFanZone.name = "FanZone" + obj.Number;
            FanZone fanZoneComp = newFanZone.GetComponent<FanZone>();
            fanZoneComp.FillFromDTO(obj);
            fanZones.Add(fanZoneComp);
        }
    }

	void Update()
    {
        if (continued)
        {
            TryStartNewMove();
        }
	}

    private void TryStartNewMove()
    {
        if (moveIndex >= moves.Count)
            return;
        if (Vis.started)
            return;
        TakePassengers();
        StartNewMove();
    }

    private void TakePassengers()
    {
        foreach (Taxi taxi in taxis)
        {
            if (IsOverFanZone(taxi))
            {
                if (taxi.Client != null)
                {
                    taxi.Client.gameObject.SetActive(false);
                    taxi.Client = null;
                }
            }
            else
            {
                if (taxi.Client == null)
                {
                    Passenger newClient = IsOverPassenger(taxi);
                    if (newClient != null)
                    {
                        taxi.Client = newClient;
                    }
                }
            }
        }
    }

    private bool IsOverFanZone(Taxi taxi)
    {
        foreach (FanZone fanZone in fanZones)
        {
            if ((taxi.X == fanZone.X) && (taxi.Y == fanZone.Y))
            {
                return true;
            }
        }
        return false;
    }

    private Passenger IsOverPassenger(Taxi taxi)
    {
        foreach (Passenger passenger in passengers)
        {
            if ((taxi.X == passenger.X) && (taxi.Y == passenger.Y))
            {
                return passenger;
            }
        }
        return null;
    }

    private void StartNewMove()
    {
        int offsetX = moves[moveIndex].OffsetX;
        int offsetY = moves[moveIndex].OffsetY;
        MovingTaxis = taxis.Where(c => moves[moveIndex].TaxiNumbers.Contains(c.Number)).ToList();
        Vis.StartMoving(offsetX, offsetY, MovingTaxis);
        moveIndex++;
    }

    public void StartPlay()
    {
        continued = true;
    }

    public void Pause()
    {
        continued = false;
    }

    public void Next()
    {
        TryStartNewMove();
    }

    public void Restart()
    {
        Vis.StopMoving();
        RemoveObjects();
        Init();
        moveIndex = 0;
        continued = false;
    }

    public List<Taxi> GetMovingTaxis()
    {
        return MovingTaxis;
    }
}
