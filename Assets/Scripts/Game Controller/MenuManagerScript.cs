using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour {
    public object LoadingScreenManager { get; private set; }

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("LevelLock2", 10);
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadSceneNum(int num)
    {
        string locktext = "LevelLock" + num.ToString();
        if (PlayerPrefs.GetInt(locktext, 0) == 10)
        {
            if (num < 0 || num >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogError("Can't load scene with index " + num);
                AudioManager.instance.PlaySound("Invalid");
            }
            else
            {

                SceneManager.LoadScene(num);
            }
            
        }
        else
        { AudioManager.instance.PlaySound("Invalid"); }
    }
}
