using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    public MoveController Controller;
    public LerpController Vis;
    public Text movingObjectsText;
    public Text mouseObjectText;
    public InputField VisSpeedInput;

    void Start()
    {
        if (Vis != null)
        {
            VisSpeedInput.text = Vis.speed.ToString();
        }
    }

	void Update()
    {
        List<Taxi> moving = Controller.GetMovingTaxis();
        string[] lines = new string[moving.Count];
        for (int i = 0; i < moving.Count; i++)
        {
            lines[i] = ObjToString(moving[i]);
        }
        string concated = string.Join(System.Environment.NewLine, lines);
        movingObjectsText.text = concated;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            NumberedObject mouseObj = hit.collider.GetComponentInParent<NumberedObject>();
            if (mouseObj != null)
            {
                mouseObjectText.text = ObjToString(mouseObj);
            }
            else
            {
                mouseObjectText.text = string.Empty;
            }
        }
        else
        {
            mouseObjectText.text = string.Empty;
        }

        if (Vis != null)
        {
            float speed;
            if (float.TryParse(VisSpeedInput.text, out speed))
            {
                Vis.speed = speed;
            }
        }
    }

    private string ObjToString(NumberedObject obj)
    {
        return string.Format("{0} {1:0.00} {2:0.00}", obj.Number, obj.Xfloat, obj.Yfloat);
    }
}
