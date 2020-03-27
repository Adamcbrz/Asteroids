using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// EuclideanTorus defines a static instance for all system to access for wrapping visually over the screen
/// </summary>
public class EuclideanTorus
{

    #region Static 
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
    #endregion

    #region Private Variables
    private Edges edges;
    #endregion

    #region Public Methods

    public EuclideanTorus() { }

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

    public bool Contains(Vector3 pos)
    {
        return (pos.x >= edges.x.max && pos.x >= edges.x.min && pos.y <= edges.y.max && pos.y >= edges.y.min);        
    }

    public Vector3 Wrap(Vector3 pos, float radius = 0)
    {
        if (pos.x > edges.x.max + radius)
        {
            pos.x = edges.x.min - radius;
        }
        else if (pos.x < edges.x.min - radius)
        {
            pos.x = edges.x.max + radius;
        }
        else if (pos.y > edges.y.max + radius)
        {
            pos.y = edges.y.min - radius;
        }
        else if (pos.y < edges.y.min - radius)
        {
            pos.y = edges.y.max + radius;
        }

        return pos;
    }

    #endregion
}
