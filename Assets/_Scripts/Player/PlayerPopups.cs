using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class PlayerPopups
{
    private static TextMeshProUGUI popupText;
    private static bool popupSet = false;

    public static void InitPopup(TextMeshProUGUI text)
    {
        if (popupSet) return;
        popupText = text;
        popupSet = true;
    }

    public static void SetPopupText(string text)
    {
        popupText.text = text;
    }

    public static void ClearPopupText()
    {
        popupText.text = "";
    }

    public static bool ComparePopupText(string text)
    {
        return text == popupText.text;
    }
}
