using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainClick : MonoBehaviour
{
    public static MainClick s;

    private void Awake()
    {
        if (s == null)
        {
            s = this;
        }
    }
    public void ClickButton()
    {
        var item = ItemManager.Instance.GetItemByName("Warlock");
        Player.s.timeLine.Add(item);
        Player.s.Playing("Warlock");
        Player.s.ButtonIsActive(Player.s.listButtons[3], false);
        Player.s.isPlay = true;
    }

    public void ButtonTextScreen()
    {
        int T = 0;
        foreach (var item in Player.s.CurrVideo().refVideo)
        {
            //print(item);
            Player.s.ButtonIsActive(Player.s.listButtons[T], true);
            ButtonTextOnSceen(T);
            T++;
        } 
    }

    public void ButtonTextOnSceen(int c)
    {
        Player.s.listButtons[c].GetComponentInChildren<Text>().text = Player.s.CurrVideo().choiceText[c];
        Player.s.listButtons[c].GetComponent<ButtonB>().refVideo = Player.s.CurrVideo().refVideo[c];
    }
}