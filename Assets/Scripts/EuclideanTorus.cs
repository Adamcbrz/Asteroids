using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EuclideanTorus
{
    public static EuclideanTorus current
    {
        get
        {
            if (m_current == null)
                m_current = new EuclideanTorus();

            return m_current;
        }
        set { m_current = value; }
    }
    private static EuclideanTorus m_current;

    public EuclideanTorus() { }
    private Edges edges;

    public void Setup(float width, float height)
    {
        float right = width / 2;
        float left = right * -1;
        float top = height / 2;
        float bottom = top * -1;
        Range xRange = new Range { min = left, max = right };
        Range yRange = new Range { min = bottom, max = top };

        edges = new Edges { x = xRange, y = yRange };
    }

    public Range xRange { get { return edges.x; }}
    public Range yRange { get { return edges.y; }}

    public Vector3 Wrap(Vector3 pos)
    {
        if (pos.x > edges.x.max)
        {
            pos.x = edges.x.min;
        }
        else if (pos.x < edges.x.min)
        {
            pos.x = edges.x.max;
        }
        else if (pos.y > edges.y.max)
        {
            pos.y = edges.y.min;
        }
        else if (pos.y < edges.y.min)
        {
            pos.y = edges.y.max;
        }

        return pos;
    }
}
