using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume=0.7f;
     [Range(0.5f, 1.5f)]
    public float pitch=1f;

    [Range(0f, .5f)]
     public float randomVolume = 0.1f;
     [Range(0f, .5f)]
     public float randomPitch = 0.1f;

     public bool loop = false;


    private AudioSource source;

    public void SetSource (AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
    }

    public void Play()
    {
        source.volume = volume*(1+Random.Range(-randomVolume/2f , randomVolume/2f));
        source.pitch = pitch*(1+Random.Range(-randomVolume/2f , randomVolume/2f));
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }
}
public class AudioManager : MonoBehaviour {

    public static AudioManager instance;


    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
        else
        { 
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        for(int i =0;i< sounds.Length;i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
        if (PlayerPrefs.GetInt("MusicToggle", 0) == 0)
        PlaySound("Music");
    }

	public void VolumeControl(float soundControl){
		foreach (Sound sound in sounds)
			sound.volume = soundControl;
	}

	public void MusicToggle(bool play){

		if (play) 
			foreach (Sound sound in sounds)
				sound.Play();
		else
			foreach (Sound sound in sounds)
				sound.Stop();
	}

    public void PlaySound(string _name)
    {
        for(int i=0;i<sounds.Length;i++)
        {
            if(sounds[i].name==_name)
            {
                sounds[i].Play();
                return;
            }
        }

        //no sound
        Debug.Log("WARNING: Audiomanager: sound nai :@" + _name);
    }
    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        //no sound
        Debug.Log("WARNING: Audiomanager: sound nai :@" + _name);
    }
}
