using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockChecker : MonoBehaviour {

    [SerializeField]
    private int level;

    private string locktext;
	void Start () {
        locktext = "LevelLock" + level.ToString();
        if (!(PlayerPrefs.GetInt(locktext, 0) == 10))
        {
            this.gameObject.SetActive(true);
        }
        else
            this.gameObject.SetActive(false);

    }
	
}
