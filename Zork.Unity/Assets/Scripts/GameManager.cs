using UnityEngine;
using Zork.Common;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UnityInputService InputService;
    [SerializeField]
    private UnityOutputService OutputService;

    [SerializeField]
    private TextMeshProUGUI LocationText;

    [SerializeField]
    private TextMeshProUGUI MovesText;

    [SerializeField]
    private TextMeshProUGUI ScoreText;

    private void Start()
    {
        InputService.SetFocus();
        _game.Player.LocationChanged += Player_LocationChanged;
    }
    private void Awake()
    {
        TextAsset gameJson = Resources.Load<TextAsset>("Game Json");
        _game = JsonConvert.DeserializeObject<Game>(gameJson.text);
        _game.Run(InputService,OutputService);
        LocationText.text = _game.Player.CurrentRoom.Name;
    }

    private void Update()
    {
        LocationText.text = _game.Player.CurrentRoom.Name;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InputService.ProcessInput();
            InputService.SetFocus();
        }
    }

    private void Player_LocationChanged(object sender, Room location)
    {
        LocationText.text = location.Name;
    }
    private Game _game;
}
