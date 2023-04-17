using System;
using UnityEngine;

internal static class GameManagementPlayerPrefs
{
    public static int PlayerLevel
    {
        set => PlayerPrefs.SetInt("CurrentLevel", value);
        get => PlayerPrefs.GetInt("CurrentLevel", 0);
    }

    public static int PlayerTotalScore
    {
        set => PlayerPrefs.SetInt("TotalScore", value);
        get => PlayerPrefs.GetInt("TotalScore", 0);
    }

    public static int GetLevelAttempts(int levelNumber)
    {
        return PlayerPrefs.GetInt($"Level_{levelNumber}_Attempts", 0);
    }

    public static void AttemptLevel(int levelNumber)
    {
        var newAttempt = GetLevelAttempts(levelNumber) + 1;
        PlayerPrefs.SetInt($"Level_{levelNumber}_Attempts", newAttempt);
    }
}
