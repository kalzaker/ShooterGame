using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource aplayer;

    [SerializeField] AudioClip[] clips;

    void Start(){
        EventManager.soundPlayed.AddListener(PlaySound);
    }

    void PlaySound(Clip clip, Vector3 pos){
        switch(clip){
            case Clip.Hurt:{
                AudioSource.PlayClipAtPoint(clips[0], pos);
                break;
            }
            case Clip.Attack:{
                AudioSource.PlayClipAtPoint(clips[1], pos);
                break;
            }
            case Clip.ChangeElement:{
                AudioSource.PlayClipAtPoint(clips[2], pos);
                break;
            }
            case Clip.Die:{
                AudioSource.PlayClipAtPoint(clips[3], pos);
                break;
            }
            case Clip.EnemyDie:{
                AudioSource.PlayClipAtPoint(clips[4], pos);
                break;
            }
            case Clip.WaveCleared:{
                AudioSource.PlayClipAtPoint(clips[5], pos);
                break;
            }
            case Clip.BlackHole:{
                AudioSource.PlayClipAtPoint(clips[6], pos);
                break;
            }
            case Clip.FireExplosion:{
                AudioSource.PlayClipAtPoint(clips[7], pos);
                break;
            }
            case Clip.AirExplosion:{
                AudioSource.PlayClipAtPoint(clips[8], pos);
                break;
            }
            case Clip.OrbCollision:{
                AudioSource.PlayClipAtPoint(clips[9], pos);
                break;
            }
        }
    }
}

public enum Clip{
    Hurt,
    Attack,
    ChangeElement,
    Die,
    EnemyDie,
    WaveCleared,
    BlackHole,
    FireExplosion,
    AirExplosion,
    OrbCollision
}