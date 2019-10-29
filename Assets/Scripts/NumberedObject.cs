using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberedObject : MonoBehaviour
{
    public Text numberText;
    private int _number;

    public int Number
    {
        get
        {
            return _number;
        }
        set
        {
            _number = value;
            if (numberText != null)
            {
                numberText.text = _number.ToString();
            }
        }
    }

    public int X
    {
        get
        {
            return (int)transform.position.x;
        }
        set
        {
            transform.position = new Vector3(value, transform.position.y, transform.position.z);
            MoveConnectedObjects();
        }
    }

    public int Y
    {
        get
        {
            return (int)transform.position.y;
        }
        set
        {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
            MoveConnectedObjects();
        }
    }

    public float Xfloat
    {
        get
        {
            return transform.position.x;
        }
        set
        {
            transform.position = new Vector3(value, transform.position.y, transform.position.z);
            MoveConnectedObjects();
        }
    }

    public float Yfloat
    {
        get
        {
            return transform.position.y;
        }
        set
        {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
            MoveConnectedObjects();
        }
    }

    public void FillFromDTO(DTO source)
    {
        Number = source.Number;
        X = source.X;
        Y = source.Y;
    }

    protected virtual void MoveConnectedObjects()
    { }
}
