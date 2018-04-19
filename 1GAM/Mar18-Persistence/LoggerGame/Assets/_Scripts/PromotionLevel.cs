using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromotionLevel : MonoBehaviour {

    public SkillType skillType;

    private TMP_Text text;
    public enum SkillType { ChopSpeed, LumberjackJumpSpeed, LumberjackStamina, PlantSpeed, WaterSpeed, PlanterJumpSpeed, PlanterStamina};

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Use this for initialization
    void Start () {
		
	}


    /// <summary>
    /// Used when the promotion level button is pressed. Updates the level counter for each category.
    /// </summary>
    public void UpdatePromotionLevel()
    {
        //switch (skillType)
        //{
        //    case SkillType.ChopSpeed:
        //        GameManager.instance.skillLevels[skillType.ToString()] += 1;
        //        break;
        //    case SkillType.LumberjackJumpSpeed:
        //        break;
        //    case SkillType.LumberjackStamina:
        //        break;
        //    case SkillType.PlanterJumpSpeed:
        //        break;
        //    case SkillType.PlanterStamina:
        //        break;
        //    case SkillType.PlantSpeed:
        //        break;
        //    case SkillType.WaterSpeed:
        //        break;

        //}

        GameManager.instance.skillLevels[skillType.ToString()] += 1;
        text.text = GameManager.instance.skillLevels[skillType.ToString()].ToString();
    }
}
