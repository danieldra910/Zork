using TMPro;
using UnityEngine;
using Zork.Common;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnityOutputService : MonoBehaviour, IOutputService
{
    [SerializeField]
    private TextMeshProUGUI TextLinePrefab;

    [SerializeField]
    private Image NewLinePrefab;

    [SerializeField]
    private Transform ContentTransform;

    [SerializeField]
    [Range(0, 1000)]
    private int MaxEntries;

    public void Write (object message)
    {
        ParseAndWriteLine(message.ToString());
    }

    public void Write (string message)
    {
        ParseAndWriteLine (message);
    }

    public void WriteLine(object message)
    {
        char separator = '\n';
        string[] strings = message.ToString().Split(separator);
        foreach (string s in strings)
        {
            if (!s.Equals(separator))
                ParseAndWriteLine(s);
        }

    }

    public void WriteLine(string message)
    {
        char separator = '\n';
        string[] strings = message.Split (separator);
        foreach (string s in strings) 
        {
            if (!s.Equals(separator))
                ParseAndWriteLine(s);
        }
        //ParseAndWriteLine(message);
    }

    private void ParseAndWriteLine(string message)
    {
        if(_entries.Count >= MaxEntries)
        {
            _entries.Clear();
        }
        var textLine = Instantiate(TextLinePrefab, ContentTransform);
        textLine.text = message;
        _entries.Enqueue(textLine.gameObject);

    }

    private Queue<GameObject> _entries = new Queue<GameObject> ();
}
