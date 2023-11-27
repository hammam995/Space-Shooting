using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "AudioDB", menuName = "DB/Audio", order = 0)]
public class AudioDB : ScriptableObject
{

    [Space(20)]
    [Header("Audio Clips")]
    [SerializeField] private List<EachAudio> M_Audio_Datas = new List<EachAudio>();

    public void Initialize()
    {
        ChangeToID();
    }

    private readonly Dictionary<EAudioTag, EachAudio> M_Unique_ID = new();


    public void ChangeToID()
    {
        foreach (var soundData in M_Audio_Datas)
        {
            M_Unique_ID.TryAdd(soundData.M_Audio_Tag, soundData);
        }
    }

    public EachAudio GetSoundDataByID(EAudioTag audioTag)
    {
        return M_Unique_ID[audioTag];
    }

    private void OnValidate()
    {
        foreach(var soundData in M_Audio_Datas)
        {
            soundData.Validate(name);
        }
    }
}

[Serializable]
public class EachAudio
{
    [field: SerializeField] public AudioClip M_Audio_Clip { private set; get; }
    [field: SerializeField] public float M_Pitch_Minimum { private set; get; }
    [field: SerializeField] public float M_Pitch_Maximum { private set; get; }
    [field: SerializeField] public EAudioTag M_Audio_Tag { private set; get; }

    public void Validate(string name)
    {
        Assert.IsNotNull(M_Audio_Clip,$"{nameof(M_Audio_Clip)} cannot be null in {name}");
    }
}
public enum EAudioTag
{
    Attack,
    DestroyFallingItem,
}
