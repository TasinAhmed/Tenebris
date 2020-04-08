using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messyspace {

    public class PlayerStats : MonoBehaviour
    {

        [Header("Main Player Stats")]
        public string PlayerName;
        public int PlayerHP = 50; //baseline = 50 +20% each upgrade

        [SerializeField]
        private int m_PlayerSkillPoints = 0;
        public int PlayerSkillPoints{
            get { return m_PlayerSkillPoints; }
            set {
                m_PlayerSkillPoints = value;

                //SP changed
                if (onSkillPointsChange != null)
                    onSkillPointsChange();
            }
        }

        //Set Listener for Player Level
        [SerializeField]
        private int m_PlayerLevel = 1;
        public int PlayerLevel{
            get { return m_PlayerLevel; }
            set { 
                m_PlayerLevel = value;

                //Level change
                if (onLevelChange != null)
                    onLevelChange();
            }
        }

        [Header("Player Attributes")]
        public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

        [Header("Player Skills Enabled")]
        public List<Skills> PlayerSkills = new List<Skills>();

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //Delegates for listeners
        public delegate void OnSPChange();
        public event OnSPChange onSkillPointsChange;

        public delegate void OnLevelChange();
        public event OnLevelChange onLevelChange;

        //Just some temp methods to test
        public void UpdateLevel(int amount){
            PlayerLevel += amount;
        }

        public void UpdateSkillPoints(int amount){
            PlayerSkillPoints += amount;
        }
    }

}