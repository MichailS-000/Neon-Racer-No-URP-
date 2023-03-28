using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] music;

    int currentPos = 0;
    bool isPaused = false;

	private void Start()
	{
        audioSource.clip = music[0];
	}

	public void Next()
	{
        currentPos++;
        if (currentPos >= music.Length)
		{
            currentPos = 0;
		}

        PlayCurrent();
	}

    public void Previos()
	{
        currentPos--;
        if (currentPos <= music.Length)
        {
            currentPos = music.Length - 1;
        }

        PlayCurrent();
    }

    void PlayCurrent()
	{
        audioSource.clip = music[currentPos];
        audioSource.Play();
	}

    public void Pause()
	{
        audioSource.Pause();
        isPaused = true;
	}

    public void Play()
	{
        if (isPaused)
        {
            audioSource.UnPause();
			isPaused = false;
        }
		else
		{
            audioSource.UnPause();
            audioSource.Play();
        }
	}
}