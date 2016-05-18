using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Channel
{
    public float[] _data;
    public Color _color = Color.white;
    public bool isActive = false;

    public Channel(Color _C, Graph g)
    {
        _color = _C;
        _data = new float[g.MAX_HISTORY];
    }



    public void Feed(float val)
    {
        for (int i = _data.Length - 1; i >= 1; i--)
            _data[i] = _data[i - 1];

        _data[0] = val;
    }
}

public class Graph
{
    public float YMin = -1, YMax = +1;

    public int MAX_HISTORY = 1024;

    public List<Channel> channels;

    public Graph(params Color[] colors)
    {
        channels = new List<Channel>(colors.Length);

        foreach (var color in colors)
        {
            channels.Add(new Channel(color, this));
        }
    }

    public Graph(int channelCount)
    {
        var availableColors = GetColors();
        var colorsToUse = new List<Color>();

        for (int i = 0; i < channelCount; i++)
        {
            colorsToUse.Add(availableColors[i]);

        }

        channels = new List<Channel>();

        foreach (var color in colorsToUse)
        {
            channels.Add(new Channel(color, this));
        }
    }

    private static List<Color> GetColors()
    {
        var colorType = typeof(Color);

        var fields = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);

        var colors = new List<Color>();

        var temp = Color.white;

        foreach (var f in fields)
        {
            colors.Add((Color) f.GetValue(temp, null) );
        }

        return colors;
    }

}