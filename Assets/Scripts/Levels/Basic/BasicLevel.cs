using UnityEngine;

public class BasicLevel : BaseLevel
{
    private BasicLevelConfig _config;

    [SerializeField] private Grid _grid;
    
    public override void InitializeLevel(BaseLevelConfig config) // Before starting the level. (Usually behind start panel)
    {
        base.InitializeLevel(config);
        _config = (BasicLevelConfig)config;
        InstantiateLevelObjects();
    }

    private void InstantiateLevelObjects()
    {
        _grid.InitializeGrid(this._config.gridConfig);
    }

    protected internal override void StartLevel() // When click on start
    {
        base.StartLevel();
    }

    protected override void FillVariables()
    {
        base.FillVariables();
    }

    protected internal override void FinishLevel()
    {
        base.FinishLevel();
    }

    protected override void SubscribeEvents() // For example, listen to what player does
    {
    }

    protected override void UnSubscribeEvents()
    {
    }

    protected internal override int CalculateEarnedMoney()
    {
        return 10; // Your estimate
    }

    protected internal override float CalculateSatisfaction()
    {
        return .5f; // Your estimate
    }

    protected internal override int CalculateScore()
    {
        return 100; // Your estimate
    }

    protected internal override bool IsWon()
    {
        return CalculateSatisfaction() > .6f; // Your rules
    }

    protected internal override void PrepareUIRequirementsabstract()
    {
        // Ui can implement your level UI here
    }
}
