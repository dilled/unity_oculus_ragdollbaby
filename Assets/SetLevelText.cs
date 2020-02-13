using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLevelText : MonoBehaviour
{
    public Text text;
    
    public void SetText(int level)
    {
        text.text = "DAY " + level.ToString();
    }
    
}
