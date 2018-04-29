using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromotionLevel : MonoBehaviour {

    public SkillType skillType;
    public enum SkillType { ChopSpeed, DigSpeed, LumberjackJumpSpeed, LumberjackStamina, PlantSpeed, WaterSpeed, PlanterJumpSpeed, PlanterStamina };
    private TMP_Text text;
    public PromotionPoints promotionPoints;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Used when the promotion level button is pressed. Updates the level counter for each category.
    /// </summary>
    public void UpdatePromotionLevel()
    {
        if(promotionPoints.numPoints > 0)
        {
            promotionPoints.UsePoints();
            GameManager.instance.skillLevels[skillType.ToString()] += 1;
            text.text = GameManager.instance.skillLevels[skillType.ToString()].ToString();
        }
    }
}
