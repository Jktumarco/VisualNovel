
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemDB")]
public class ItemDB : ScriptableObject
{
    public List<VideoItem> Items;

    public List<Song> song;

    public static ItemDB s;
   
}


