using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicLevelConfig", menuName = "JaBum/Level/BasicLevelConfig", order = 0)]

public class BasicLevelConfig : BaseLevelConfig
{
    public GameObject Dummy;
    public GameObject SecondDummy;
    public override string SceneName => "";
}
