using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PromotePanel : MonoBehaviour {

    public TMP_Text[] skillLevels;
    public PromotionPoints promotionPoints;

    public void ResetSkillLevels()
    {
        foreach(TMP_Text t in skillLevels)
        {
            t.text = 1.ToString();
        }
        //promotionPoints.ResetPoints();
    }
}
