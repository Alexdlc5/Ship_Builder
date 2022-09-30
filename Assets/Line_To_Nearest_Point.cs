using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_To_Nearest_Point : MonoBehaviour
{
    private LineRenderer line_renderer;
    public Texture2D texture;
    private Transform[] points;
    // Start is called before the first frame update
    private void Start()
    {
        SetUpLine(points);
    }
    void Awake()
    {
        line_renderer = GetComponent<LineRenderer>();
        points = new Transform[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_Selection.current_placable == null || Submarine_Core.submode == true)
        {
            //make line invisible
            line_renderer.startColor = new Color(line_renderer.startColor.r, line_renderer.startColor.g, line_renderer.startColor.b, 0);
            line_renderer.endColor = new Color(line_renderer.endColor.r, line_renderer.endColor.g, line_renderer.endColor.b, 0);
        }
        else
        {
            //make line visible
            line_renderer.startColor = new Color(line_renderer.startColor.r, line_renderer.startColor.g, line_renderer.startColor.b, 1);
            line_renderer.endColor = new Color(line_renderer.endColor.r, line_renderer.endColor.g, line_renderer.endColor.b, 1);
        }
        points[0] = transform;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (points[1] != null)
        {
            for (int i = 0; i < points.Length; i++)
            {
                line_renderer.SetPosition(i, points[i].position);
            }
        }
    }
    public void SetUpLine(Transform[] points)
    {
        line_renderer.positionCount = points.Length;
        this.points = points;
    }
    public void setSecondPoint(Transform transform)
    {
        points[1] = transform;
    }
}
