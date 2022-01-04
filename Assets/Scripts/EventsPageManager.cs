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

    public GameObject EventsPageGameObject;

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

        for (int i = 0; i < stringArray.Count; i++)
        {
            Items[i].SetActive(true);
            Items[i].transform.GetComponent<EventButtonScript>().SetEventData(stringArray[i]);
            //Items[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = stringArray[i];
        }
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
    
}
