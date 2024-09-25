using System;
using TMPro;

public static class PlayerPopups
{
    private static TextMeshProUGUI popupText;
    private static bool popupSet = false;
    public static bool popupVisible = true;


    public static void InitPopup(TextMeshProUGUI text)
    {
        if (popupSet)
            throw new Exception("Cannot init popup text twice");
        popupText = text;
        popupSet = true;
    }

    public static void SetPopupText(string text)
    {
        popupText.text = text;

        popupText.gameObject.SetActive(popupVisible);
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
