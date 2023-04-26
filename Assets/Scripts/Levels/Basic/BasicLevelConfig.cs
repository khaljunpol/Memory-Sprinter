using UnityEngine;

[CreateAssetMenu(fileName = "BasicLevelConfig", menuName = "JaBum/Level/BasicLevelConfig", order = 0)]

public class BasicLevelConfig : BaseLevelConfig
{
    public GridConfig gridConfig;
    public override string SceneName => "";
}
