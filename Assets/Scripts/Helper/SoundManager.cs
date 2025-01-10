using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField]
    private AudioSource GunSound,EnemySound;
    public AudioClip[] GunClips;
    public AudioClip[] AxeClips;
    public AudioClip CannibalScreem;
    public AudioClip CannibalAttack;
    public AudioClip BoarAttack;
    public AudioClip BoarDead;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        GunSound = GetComponent<AudioSource>();
    }

    public void AxeSound()
    {
        GunSound.PlayOneShot(AxeClips[Random.Range(0, AxeClips.Length)]);
    }
    public void AssaultSound()
    {
        GunSound.PlayOneShot(GunClips[0]);
    } 
    public void RevolverSound()
    {
        GunSound.PlayOneShot(GunClips[1]);
    }
    public void ShotGunSound()
    {
        GunSound.PlayOneShot(GunClips[2]);
        StartCoroutine(ShotGunReload());
    }
    IEnumerator ShotGunReload()
    {
        yield return new WaitForSeconds(.8f);
        GunSound.PlayOneShot(GunClips[3]);
    }

    public void CannibalAttackSound()
    {
        EnemySound.PlayOneShot(CannibalAttack);
    }
    public void CannibalScreemSound()
    {
        EnemySound.PlayOneShot(CannibalScreem);
    }
    public void BoarAttackSound()
    {
        EnemySound.PlayOneShot(BoarAttack);
    }
    public void BoarDeadSound()
    {
        EnemySound.PlayOneShot(BoarDead);
    }
}
