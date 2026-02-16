using System;
using UnityEngine;


public enum SoundType
{
    ButtonHover,
    ButtonPress,
    ButtonImpossible,
    CharacterSwordHit,
    CharacterJump,
    CharacterDash,
    CharacterHeavyAttack,
    CharacterSpell,
    CharacterSpellElectric,
    CharacterSpellBats,
    CharacterHurt,
    CharacterDeath,
    CharacterGetRelic,
    CharacterRiseFromTomb,
    CharacterWalk,
    RatAttack,
    RatDeath,
    SkullAttack,
    SkullDeath,
    DoorOpen,
    TrapSpikes,
    PlatformDown,
    PlatformUp,
    MerchantBuy,
    MerchantBlood,
    MusicMainMenu,
    MusicCombat,
    MusicWin
}


    
    
    

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    [Range(0f,1f)] public float volume = 1f;
    [Range(0f,1f)] public float pitch = 1f;
    private AudioSource OldTrack, CurrentTrack; //Faire le fade in fade out depuis le old track jusqu'au current
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        //Randomize pitch
        instance.audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        instance.audioSource.PlayOneShot(randomClip, volume);
        //instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    public static void PlayMusic(SoundType sound, float volume = 1)
    {
        
    }
    
#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
    #endif

    [Serializable]
    public struct SoundList
    {
        public AudioClip[] Sounds
        {
            get => sounds;
        }
        [HideInInspector] public string name;
        [SerializeField] private AudioClip[] sounds;
    }
}
