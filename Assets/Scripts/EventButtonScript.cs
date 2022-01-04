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


    public void SetEventData(string data)
    {
        var finalData = data.Split('/');

        NameText.text = finalData[0];
        CategoryText.text = finalData[1];
        LengthText.text = finalData[2];
        DateText.text = finalData[3];
        LocationText.text = finalData[4];
        //data.Split()
    }
    
    public void CategoryButton(int index)
    {
        EventsPageManager.Instance.StartOpenCategory(index);
    }
    
}
