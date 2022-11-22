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
        ParseAndWriteLine(message.ToString());
    }

    public void WriteLine(string message)
    {
        ParseAndWriteLine(message);
    }

    private void ParseAndWriteLine(string message)
    {
        var textLine = Instantiate(TextLinePrefab,gameObject.transform);
        textLine.text = message;
        _entries.Add(textLine.gameObject);
        
    }

    private List<GameObject> _entries = new List<GameObject> ();
}
