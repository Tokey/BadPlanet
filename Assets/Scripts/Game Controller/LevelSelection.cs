using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelSelection : MonoBehaviour {

    // Use this for initialization

    int highScoreText;

    [SerializeField]
    private int level;
	void Start () {
        string temp = "LevelHS" + level;
        highScoreText=PlayerPrefs.GetInt(temp, 0);

        this.GetComponent<Text>().text = highScoreText.ToString(); ;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
