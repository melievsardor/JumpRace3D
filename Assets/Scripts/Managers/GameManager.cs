using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameStates gameState;
    public GameStates GetGameStates { get { return gameState; } }

    private List<IGameState> gameStates = new List<IGameState>();
    private List<PlayerController> players = new List<PlayerController>();
    public List<PlayerController> GetPlayers { get { return players; } }

    private LevelController levelController;

    protected override void Awake()
    {
        base.Awake();

        levelController = FindObjectOfType<LevelController>();
        levelController.Init();
    }

    public void AddState(IGameState state)
    {
        gameStates.Add(state);
    }

    public void SetPlay()
    {
        foreach (var state in gameStates)
            state.Play();
    }

    public void SetFailed()
    {
        foreach (var state in gameStates)
            state.Failed();
    }

    public void SetFinish()
    {
        foreach (var state in gameStates)
            state.Finish();

        gameState.LevelCompleted();
    }

    public void SetLeadrboard()
    {
        players.Sort((a, b) => a.Index.CompareTo(b.Index));

        foreach (var state in gameStates)
            state.Leadrboard();
    }

    public void AddPlayer(PlayerController player)
    {
        players.Add(player);
    }
  

}
