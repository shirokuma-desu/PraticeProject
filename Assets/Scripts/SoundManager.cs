using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "soundEffectsVolume";

    [SerializeField] private AudioClipSO audioClipSO;

    public static SoundManager Instance { get; private set; }

    private float volume = 1.0f;

    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyCounterPlaceHere += BaseCounter_OnAnyCounterPlaceHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyCounterPlaceHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipSO.objectPickUp, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipSO.deliverySuccess, Camera.main.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 positoin, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, positoin, volumeMultiplier*volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 positoin, float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], positoin, volume);
    }

    public void PlayFootstepsSound(Vector3 position,float volume)
    {
        PlaySound(audioClipSO.footstep, position, volume);
    }

    public void PlayCountDownSound()
    {
        PlaySound(audioClipSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 posistion)
    {
        PlaySound(audioClipSO.warning, posistion);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if(volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
