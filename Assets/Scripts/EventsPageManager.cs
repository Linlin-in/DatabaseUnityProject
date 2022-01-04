using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventsPageManager : MonoBehaviour
{
    
    #region Singleton
    private static EventsPageManager _instance;

    public static EventsPageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EventsPageManager>();
            }
            
            return _instance;
        }
    }
    #endregion

    
    public GameObject[] Items;
    public EventButtonScript[] ButtonScripts;

    public GameObject EventsPageGameObject;

    private int OpenCount;
    
    public void OpenEventsPage(string[] events)
    {

        List<string> stringArray = new List<string>();

        for (int i = 0; i < events.Length-1; i++)
        {
            if (!stringArray.Contains(events[i]))
            {
                stringArray.Add(events[i]);
            }
        }

        UIManager.Instance.CloseAllPages();
        EventsPageGameObject.SetActive(true);

        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].SetActive(false);
        }

        OpenCount = stringArray.Count;
        
        for (int i = 0; i < stringArray.Count; i++)
        {
            Items[i].SetActive(true);
            Items[i].transform.GetComponent<EventButtonScript>().SetEventData(stringArray[i]);
            //Items[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = stringArray[i];
        }
        
        
        StartCoroutine(DBManager.StartGetLikes());
        
    }
    
    public void StartGetLiesMethod()
    {
        StartCoroutine(DBManager.StartGetLikes());
    }
    
    public string GetCategoryName(int index)
    {
        return Items[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }
    
    public void StartOpenCategory(int index)
    {
        WWWForm data = new WWWForm();
        data.AddField("categoryName", GetCategoryName(index));

        StartCoroutine(DBManager.GetLocationWithCategoryData(data));
    }

    public void CalculateLikes(string data)
    {
        List<LikeData> likeData = new List<LikeData>();

        var stringArray = data.Split(':');
        
        for (int i = 0; i < stringArray.Length-1; i++)
        {
            var littleData = stringArray[i].Split('/');
            if(littleData.Length<2)
                continue;
            
            if (likeData.Count == 0)
            {
                LikeData newData = new LikeData();
                newData.EventName = littleData[0];
                newData.LikedUsers.Add(littleData[1]);
                likeData.Add(newData);
            }
            else
            {
                bool boool = false;
                for (int j = 0; j < likeData.Count-1; j++)
                {
                    if (likeData[j].EventName == littleData[0])
                    {
                        likeData[j].LikedUsers.Add(littleData[1]);
                        boool = true;
                    }
                }

                if (!boool)
                {
                    LikeData newData = new LikeData();
                    newData.EventName = littleData[0];
                    newData.LikedUsers.Add(littleData[1]);
                    likeData.Add(newData);
                }
            }
        }
        
        for (int i = 0; i < stringArray.Length; i++)
        {
            Items[i].SetActive(true);
            Items[i].transform.GetComponent<EventButtonScript>().SetEventData(stringArray[i]);
            //Items[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = stringArray[i];
        }

        foreach (var btnScript in ButtonScripts)
        {
            btnScript.SetLikeCount(likeData);
        }
    }
    
    
}

[System.Serializable]
public class LikeData
{
    public string EventName;
    public List<string> LikedUsers;

    public LikeData()
    {
        EventName = "";
        LikedUsers = new List<string>();
    }
    
    public LikeData(string eventName, List<string> likedUsers)
    {
        EventName = eventName;
        LikedUsers = likedUsers;
    }
}