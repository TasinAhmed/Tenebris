using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static int level;
    Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        level = Mathf.FloorToInt(SoulsManager.souls / 3);
        text.text = "LEVEL " + level;
    }
}
