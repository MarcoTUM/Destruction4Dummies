using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AdvisorDialogues
{
    private static Dictionary<int, string> dialogues = new Dictionary<int, string>()
    {
        {1, "Welcome to the game you are gonna destroy everything. Everything you touch is gonna disintegrate and stuff is gonna happen depending on the block."
            +" I actually have no idea what to write so i am jsut tiyping stuff to fill the textbox and test if it works!" },
        {-1, "This is an XboxSpecific text meaning you only see this if you are using an xbox device or any device because i am lazy and dont aknowldege controllers that are not an xbox one on the pc." },
        {3, "Well I also need to test if the textboxes are shown in the correct levels. Therefore, I need to come up with another useless text and fill the speech bubble so "
            +"I can test everything. To be honest I am running out of ideas what to write about, luckily only a couple of words left until 2+ pages are filled with text and I can test this s" }

    };

    /// <summary>
    /// Gets the respective dialogues for the specific level or null if level does not have dialogue
    /// </summary>
    /// <param name="level">Index of level e.g. 0 for Level01 </param>
    /// <returns></returns>
    public static string GetDialogues(int level)
    {
        string result = null;
        if (!dialogues.TryGetValue(level, out result) && level < 0)
        {
            dialogues.TryGetValue(-level, out result);
        }
        return result;
    }
}
