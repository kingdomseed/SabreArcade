using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
    public AudioClip mainMenuClip;
    public AudioClip gameClip;
    private AudioSource music;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = mainMenuClip;
            music.loop = true;
            music.Play();
		}
		
	}
    private void OnLevelWasLoaded(int level)
    {
        
        if (level == 0 || level == 1 || level == 4)
        {
            if (music.clip != mainMenuClip)
            {
                music.Stop();
                music.clip = mainMenuClip;
                music.loop = true;
                music.Play();
            }
            
        } else
        {
            if(music.clip != gameClip)
            {
                music.Stop();
                music.clip = gameClip;
                music.loop = true;
                music.Play();
            }
        }
      
    }
}
