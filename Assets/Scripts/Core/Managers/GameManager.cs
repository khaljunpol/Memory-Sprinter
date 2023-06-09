using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private bool startLevelOnAwake;
    public static int TotalScore => GameManagementPlayerPrefs.PlayerTotalScore;

    protected override void Awake()
    {
        FillComponents();
    }

    private void Start()
    {
        InitializeLevelManager();
    }

    private void OnEnable()
    {
        SubscribeToLevelManager();
    }

    private void OnDisable()
    {
        UnSubscribeFromLevel();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.S))
            StartLevel();
        if (UnityEngine.Input.GetKeyDown(KeyCode.W))
            ForceWin();
        if (UnityEngine.Input.GetKeyDown(KeyCode.L))
            ForceLose();
        if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            Retry();
        if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
            Skip();
        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
            GoToPreviousLevel();
        if (UnityEngine.Input.GetKeyDown(KeyCode.F))
            ForceFinish();
    }
#endif

    private static void FillComponents()
    {
    }

    public static void InitializeLevelManager()
    {
        LevelManager.Instance.Initialize();
    }

    public static void StartLevel()
    {
        LevelManager.Instance.StartLevelWheneverReady();
    }

    private void SubscribeToLevelManager()
    {
        LevelManager.OnLevelReady += LevelReady;
        LevelManager.OnLevelFinish += FinishLevel;
    }

    private void FinishLevel(LevelData levelData)
    {
        FinishLevelBehaviour(levelData.WinStatus);
    }

    private void FinishLevelBehaviour(bool successful)
    {
    }

    private void UnSubscribeFromLevel()
    {
        LevelManager.OnLevelReady -= LevelReady;
        LevelManager.OnLevelFinish -= FinishLevel;
    }

    private void LevelReady()
    {
        if (startLevelOnAwake)
            StartLevel();
    }

    public static void Skip()
    {
        LevelManager.Instance.Skip();
    }

    private void GoToPreviousLevel()
    {
        LevelManager.Instance.InitializePreviousLevel();
    }

    public static void Retry()
    {
        LevelManager.Instance.Retry();
    }

    public static void ForceWin()
    {
        LevelManager.Instance.ForceWin();
    }

    public static void ForceLose()
    {
        LevelManager.Instance.ForceLose();
    }

    public static void ForceFinish()
    {
        LevelManager.Instance.ForceFinish();
    }
}
