using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;
using UnityEngine.UI;

public enum playerEnum { IsPlay, Stop }

public class Player : MonoBehaviour
{
    public VideoPlayer player;
    public bool isPlay = false;
    public static Player s;
    [Header("Buttons in Canvas")]
    public List<GameObject> listButtons = new List<GameObject>();
   

    public int timeLineIndex = 0;
    

    [Header("Time line")]
    public List<VideoItem> timeLine;

    public Canvas canvas;
  

    private void Awake()
    {
        if (s == null)
        {
            s = this;
        }
        timeLine = new List<VideoItem>();
    }
   
    private void Update()
    {
        if (isPlay == false) { return; }
        if (player.clip != null && ((float)player.clockTime) == ((float)player.length) && isPlay == true) { isPlay = false; scr.s.TimeForEach(); }
    }

    
    public void Playing(string namesVideo)
    {
        VideoItem getting = timeLine.Find(x => x.name == namesVideo);
        player.clip = getting.video;
        player.Play();
    }


    public void ButtonIsActive(GameObject currButton, bool rrr) {
        currButton.SetActive(rrr);
    }


    public void ButtonsOffActive(List<GameObject> currButton, bool rrr)
    {
        foreach (var item in currButton)
        {
            item.SetActive(rrr);
        }
    }  
    

    public VideoItem CurrVideo()
    {
        if (timeLine != null) { return timeLine[timeLineIndex]; }
        else return null;
    }


    public void CanvasOffActive()
    {
        canvas.gameObject.SetActive(false);
    }


    public void CanvasIsActive()
    {
        canvas.gameObject.SetActive(true);
    }
}
