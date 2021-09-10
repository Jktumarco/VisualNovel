using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemDB item;

    private static ItemManager instance;

    public List<VideoItem> clonItem = new List<VideoItem>();
    public List<Song> songsItem = new List<Song>();

    public static ItemManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }

        foreach(var item in ItemManager.Instance.item.Items)
        {
            clonItem.Add(item);

        }
        foreach (var item in ItemManager.Instance.item.song)
        {
            songsItem.Add(item);

        }
        
    }
   
    public VideoItem GetItemByName(string Vname)
    {
        VideoItem itemVideo = ItemManager.instance.clonItem.Find(x => x.name == Vname);
        if(itemVideo != null)
        {
            return itemVideo;
        }
        return null; 
    }


}
