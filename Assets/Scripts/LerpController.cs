using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpController : MonoBehaviour
{
    public float speed = 1;

	public void StartMoving(int offsetX, int offsetY, List<Taxi> movingTaxis)
    {
        started = true;
        currentLerp = 0;
        StartPoints = new List<Vector2>();
        EndPoints = new List<Vector2>();
        MovingTaxis = new List<Taxi>(movingTaxis);
        foreach (Taxi taxi in MovingTaxis)
        {
            Vector2 start = new Vector2(taxi.X, taxi.Y);
            StartPoints.Add(start);
            Vector2 end = new Vector2(taxi.X + offsetX, taxi.Y + offsetY);
            EndPoints.Add(end);
        }
    }

    private void Update()
    {
        if (started)
        {
            currentLerp += Time.deltaTime * speed;
            if (currentLerp > 1)
                currentLerp = 1;
            for (int i = 0; i < MovingTaxis.Count; i++)
            {
                Vector2 current = Vector2.Lerp(StartPoints[i], EndPoints[i], currentLerp);
                MovingTaxis[i].Xfloat = current.x;
                MovingTaxis[i].Yfloat = current.y;
            }
            if (currentLerp >= 1)
            {
                started = false;
            }
        }
    }

    public void StopMoving()
    {
        started = false;
    }

    private List<Vector2> StartPoints;
    private List<Vector2> EndPoints;
    private List<Taxi> MovingTaxis;
    public bool started = false;
    private float currentLerp;
}
