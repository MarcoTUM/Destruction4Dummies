using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    private static Dictionary<int, DialogueLine[]> dialogues;
    private const string playerIndicator = "Balagan";
    private const string advisorIndicator = "Da'at";

    private void Awake()
    {
        if (dialogues == null)
        {
            dialogues = new Dictionary<int, DialogueLine[]>();
            Parse();
        }
    }

    public static void Parse()
    {
        string fullText = Resources.Load<TextAsset>(FilePaths.TextResourceFolder + "StoryDialogues").text;
        string[] textLines = fullText.Split('\n');
        int currentLevel = -1;

        List<DialogueLine> lineList = null;
        foreach (string line in textLines)
        {
            string trimmedLine = line.Trim();
            if (trimmedLine == "")
                continue;
            string[] splitLine = trimmedLine.Split(':');
            if (splitLine.Length != 2)
                throw new InvalidOperationException("Could not parse fileLine: " + trimmedLine + " - it contains not exactly 1 :");
            string lineIndicator = splitLine[0].Trim();
            string lineText = splitLine[1].Trim();
            int level;
            if (int.TryParse(lineIndicator, out level))
            {
                if (lineList != null)
                {
                    dialogues.Add(currentLevel, lineList.ToArray());
                }

                currentLevel = level;
                
                lineList = new List<DialogueLine>();
                continue;
            }
            else
            {
                Speaker speaker;
                switch (lineIndicator)
                {
                    case playerIndicator: speaker = Speaker.Player; break;
                    case advisorIndicator: speaker = Speaker.Advisor; break;
                    default: throw new InvalidOperationException("Could not parse LineIndicator: " + lineIndicator);
                }
                lineList.Add(new DialogueLine(speaker, lineText));
            }
        }

        dialogues.Add(currentLevel, lineList.ToArray());
        Debug.Log(currentLevel + " - " + lineList.Count);
    }

    public static DialogueLine[] GetDialogueLines(int id)
    {
        DialogueLine[] lines;
        if (dialogues.TryGetValue(id, out lines))
        {
            return lines;
        }
        else
        {
            return null;
        }
    }
}
