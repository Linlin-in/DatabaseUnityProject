using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventButtonScript : MonoBehaviour
{

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI CategoryText;
    public TextMeshProUGUI LengthText;
    public TextMeshProUGUI DateText;
    public TextMeshProUGUI LocationText;
    public TextMeshProUGUI LikeCount;
    public GameObject DidyouLiked;

    public string id;
    
    public void SetEventData(string data)
    {
        //print(data);
        var finalData = data.Split('/');
        
        if(finalData.Length<4)
            return;
        
        NameText.text = finalData[0];
        CategoryText.text = finalData[1];
        LengthText.text = finalData[2];
        DateText.text = finalData[3];
        LocationText.text = finalData[4];
        id = finalData[5];
        //data.Split()
    }
    
    public void CategoryButton(int index)
    {
        EventsPageManager.Instance.StartOpenCategory(index);
    }

    public void SetLikeCount(List<LikeData> likeData)
    {
        for (int i = 0; i < likeData.Count; i++)
        {
            if (likeData[i].EventName == NameText.text)
            {
                LikeCount.text = likeData[i].LikedUsers.Count + "";
                DidyouLiked.SetActive(likeData[i].LikedUsers.Contains(DBManager.Username));
            }
        }
    }
    
    public void AddLike()
    {
        WWWForm data = new WWWForm();
        data.AddField("username", DBManager.Username);
        data.AddField("pswd", DBManager.Pswd);
        data.AddField("id", id);
        
        StartCoroutine(DBManager.AddLike(data));
    }

    
}
