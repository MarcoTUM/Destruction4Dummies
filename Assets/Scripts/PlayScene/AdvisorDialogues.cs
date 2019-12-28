using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AdvisorDialogues
{
    private static Dictionary<int, string> dialogues = new Dictionary<int, string>()
    {
        {0, "Welcome to the game you are gonna destroy everything. Everything you touch is gonna disintegrate and stuff is gonna happen depending on the block. I actually have no idea what to write so i am jsut tiyping stuff to fill the textbox and test if it works!" }
    };

    /// <summary>
    /// Gets the respective dialogues for the specific level or null if level does not have dialogue
    /// </summary>
    /// <param name="level">Index of level e.g. 0 for Level01 </param>
    /// <returns></returns>
    public static string GetDialogues(int level)
    {
        string result = null;
        dialogues.TryGetValue(level, out result);
        return result;
    }
}
