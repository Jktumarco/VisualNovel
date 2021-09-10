using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class VideoItem
{

    public VideoClip video;

    public string name;


    [Header("Choicen")]
    public List<string> choiceText;

    [Header("Ref On the Video")]
    public List<string> refVideo;

    

}    

