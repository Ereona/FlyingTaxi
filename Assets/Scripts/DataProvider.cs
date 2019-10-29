using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataProvider : MonoBehaviour
{
    public abstract List<DTO> GetTaxis();

    public abstract List<DTO> GetPassengers();

    public abstract List<DTO> GetFanZones();

}
