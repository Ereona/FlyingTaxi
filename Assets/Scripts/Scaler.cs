using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    public Camera Cam;

    public float CalcScale(List<NumberedObject> objects)
    {
        int max = objects.Max(c => Mathf.Max(Mathf.Abs(c.X), Mathf.Abs(c.Y)));
        float defValue = 5;
        Cam.orthographicSize = max;
        float scale = max / defValue;
        return scale;
    }
}
