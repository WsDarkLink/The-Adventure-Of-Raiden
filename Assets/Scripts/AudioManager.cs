using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public AudioSource[] bgm; //BackGroundMusic

    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(int soundToPlay) {
        if (soundToPlay < sfx.Length){
            sfx[soundToPlay].Play();
        }
    }

    public void PlayBGM(int musicToPlay) {
        if (!bgm[musicToPlay].isPlaying)
        {
            StopBGM();
            if (musicToPlay < bgm.Length)
            {
                bgm[musicToPlay].Play();
                
            }
        }
    }

    public void StopBGM() {
        for (int i = 0; i < bgm.Length; i++) {
            bgm[i].Stop();
        }
    }

}
