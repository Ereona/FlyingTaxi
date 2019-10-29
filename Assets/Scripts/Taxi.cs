using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi : NumberedObject
{
    public Passenger Client { get; set; }

    protected override void MoveConnectedObjects()
    {
        base.MoveConnectedObjects();
        if (Client != null)
        {
            Client.Xfloat = this.Xfloat;
            Client.Yfloat = this.Yfloat;
        }
    }
}
