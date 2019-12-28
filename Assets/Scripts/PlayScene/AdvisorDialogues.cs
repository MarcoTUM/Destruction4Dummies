using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AdvisorDialogues
{
    private static Dictionary<int, string> dialogues = new Dictionary<int, string>()
    {
        {0, "Welcome to the game you are gonna destroyfffWW everyrhint angd dnothing. asdfi ellai neai." }
    };

    /// <summary>
    /// Gets the respective dialogues for the specific level or null if level does not have dialogue
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static string GetDialogues(int level)
    {
        string result = null;
        dialogues.TryGetValue(level, out result);
        return result;
    }
}
