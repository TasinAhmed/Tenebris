using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Messyspace{

    public class SkillDisplay : MonoBehaviour
    {
        //get the Scriptable Object for Skill
        public Skills skill;
        //get the UI components
        public Text skillName;
        public Text skillDescription;
        public Image skillIcon;
        public Text skillLevel;
        public Text skillSkillPointsNeeded;
        public Text skillAttribute;
        public Text skillAttrAmount;

        [SerializeField]
        private PlayerStats m_PlayerHandler;


        // Start is called before the first frame update
        void Start()
        {
            m_PlayerHandler = this.GetComponentInParent<PlayerHandler>().Player;
            //listener for Skill Points exchange
            m_PlayerHandler.onSkillPointsChange += ReactToChange;
            //listener for the Level change
            m_PlayerHandler.onLevelChange += ReactToChange;

            if (skill)
                skill.SetValues(this.gameObject, m_PlayerHandler);

            EnableSkills();
        }

        public void EnableSkills(){
            //if the player has the skill already, then show it as enabled
            if (m_PlayerHandler && skill && skill.EnableSkill(m_PlayerHandler)){
                TurnOnSkillIcon();
            }
            //if the player does not have the skill
            else if (m_PlayerHandler && skill && skill.CheckSkills(m_PlayerHandler)){
                this.GetComponent<Button>().interactable = true;
                this.transform.Find("Image").Find("Disabled").gameObject.SetActive(false);
            this.transform.Find("Image").Find("SkillActive").gameObject.SetActive(false);
            }
            else{
                TurnOffSkillIcon();
            }
        }

        private void onEnable(){
            EnableSkills();
        }

        public void GetSkill(){
            if (skill.GetSkill(m_PlayerHandler)){
                TurnOnSkillIcon();
            }
        }

        private void TurnOnSkillIcon(){
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("Image").Find("Available").gameObject.SetActive(false);
            this.transform.Find("Image").Find("Disabled").gameObject.SetActive(false);
            this.transform.Find("Image").Find("SkillActive").gameObject.SetActive(true);
        }

        private void TurnOffSkillIcon(){
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("Image").Find("Available").gameObject.SetActive(true);
            this.transform.Find("Image").Find("Disabled").gameObject.SetActive(true);
            this.transform.Find("Image").Find("SkillActive").gameObject.SetActive(false);
        }

        void ReactToChange(){
            EnableSkills();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}