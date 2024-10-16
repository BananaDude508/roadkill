using System;
using TMPro;

public static class PlayerPopups
{
    private static TextMeshProUGUI popupText;
    public static bool popupVisible = true;


    public static void AssignPopup(TextMeshProUGUI text)
    {
        popupText = text;
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
