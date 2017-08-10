using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour {

    [SerializeField]
    private float splashTime;
	
	
	// Update is called once per frame
	void Update () {
        splashTime -= Time.deltaTime;
        if(splashTime<=0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
