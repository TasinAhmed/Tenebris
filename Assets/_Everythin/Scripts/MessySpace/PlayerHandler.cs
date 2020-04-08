using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messyspace {
    
    public class PlayerHandler : MonoBehaviour
    {

        public PlayerStats Player;

        [SerializeField]
        private Canvas m_Canvas;
        private bool m_seeCanvas;

        // Called once per frame
        void Update()
        {
            // if you press the tab key
            if (Input.GetKeyDown("tab")) {
                if (m_Canvas) {
                    m_seeCanvas = !m_seeCanvas;
                    m_Canvas.gameObject.SetActive(m_seeCanvas); // display or not the canvas
                }
            }
            
        }
    }
}
