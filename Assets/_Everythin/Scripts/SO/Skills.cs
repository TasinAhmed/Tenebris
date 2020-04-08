using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messyspace{

    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Skill")]
    public class Skills : ScriptableObject
    {
        public string Description;
        public Sprite Icon;
        public int LevelNeeded;
        public int SkillPointsNeeded;

        public List<PlayerAttributes> AffectedAttributes = new List<PlayerAttributes>();

        public void SetValues(GameObject SkillDisplayObject, PlayerStats Player)
        {
            if (Player){
                CheckSkills(Player);
            }

            //bit of error handling
            if (SkillDisplayObject){
                SkillDisplay SD = SkillDisplayObject.GetComponent<SkillDisplay>();
                SD.skillName.text = name;
                if (SD.skillDescription)
                    SD.skillDescription.text = Description;

                if (SD.skillIcon)
                    SD.skillIcon.sprite = Icon;
                
                if (SD.skillLevel)
                    SD.skillLevel.text = LevelNeeded.ToString();

                if (SD.skillSkillPointsNeeded)
                    SD.skillSkillPointsNeeded.text = SkillPointsNeeded.ToString() + " SP";

                if (SD.skillAttribute)
                    SD.skillAttribute.text = AffectedAttributes[0].attribute.ToString();

                if (SD.skillAttrAmount)
                    SD.skillAttrAmount.text = "+" + AffectedAttributes[0].amount.ToString() + "%";
            }
        }

        //check if the player is able to get the skill
        public bool CheckSkills(PlayerStats Player){

            //check if player is the right level
            if (Player.PlayerLevel < LevelNeeded)
                return false;

            if (Player.PlayerSkillPoints < SkillPointsNeeded)
                return false;

            //otherwise they can enable this skill
            return true;
        }

        //check if player already has the skill
        public bool EnableSkill(PlayerStats Player){
            //go thru all skills the player has
            List<Skills>.Enumerator skills = Player.PlayerSkills.GetEnumerator();
            while (skills.MoveNext()) {
                var CurrSkill = skills.Current;
                if (CurrSkill.name == this.name){
                    return true;
                }
            }
            return false;
        }

        //get new skill
        public bool GetSkill(PlayerStats Player){
            int i = 0;
            //List through the Skill's Attributes
            List<PlayerAttributes>.Enumerator attributes = AffectedAttributes.GetEnumerator();
            while (attributes.MoveNext())
            {
                
                //List thru the Players Attribs and match with the skill attribute
                List<PlayerAttributes>.Enumerator PlayerAttr = Player.Attributes.GetEnumerator();
                while (PlayerAttr.MoveNext()) 
                {
                    if (attributes.Current.attribute.name.ToString() == PlayerAttr.Current.attribute.name.ToString()) 
                    {
                        //update the players attributes
                        PlayerAttr.Current.amount += attributes.Current.amount;
                        //mark that an attribute was updated
                        i++;
                    }
                }
                if (i > 0) {
                    //reduce te Skill Points from the Player?
                    Player.PlayerSkillPoints -= this.SkillPointsNeeded;
                    //add to the list of skills
                    Player.PlayerSkills.Add(this);
                    return true;
                }
            }
            return false;
        }
    }

}
