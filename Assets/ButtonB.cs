using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonB : MonoBehaviour
{
    public Text text;
    public string refVideo = "";
    public void OnIsClick()
    {
        
        Player.s.isPlay = true;
        var item = ItemManager.Instance?.GetItemByName(refVideo);
        Player.s.timeLine.Add(item);
        Player.s.timeLineIndex++;
        //Player.s.player.clip = null;
        Player.s.Playing(refVideo);
        Player.s.ButtonsOffActive(Player.s.listButtons, false);
    }
}
