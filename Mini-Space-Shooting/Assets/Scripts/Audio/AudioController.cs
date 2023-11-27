using UnityEngine;

public class AudioController
{
    public AudioDB M_Audio_DB { get; private set; }
    public AudioController(AudioDB audioDB)
    {
        M_Audio_DB = audioDB;
        audioDB.Initialize();
    }

    public void PlayAudio(AudioSource source, EAudioTag audioTag)
    {
        var audioData = M_Audio_DB.GetSoundDataByID(audioTag);
        source.pitch = Random.Range(audioData.M_Pitch_Minimum, audioData.M_Pitch_Maximum);
        source.PlayOneShot(audioData.M_Audio_Clip);
    }
}
