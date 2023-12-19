using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundManagerSO soundManagerSO;
    private const string SOUND_EFFECT_CHANGE = "soundEffect";
    public static SoundManager Instance { get; private set; }
    private float volume = .5f;
    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(SOUND_EFFECT_CHANGE, 1f);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnDeliverSuccess += DeliveryManager_OnDeliverSuccess;
        DeliveryManager.Instance.OnDeliverFailure += DeliveryManager_OnDeliverFailure;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickUp += Player_OnPickUp;
        Player.Instance.OnFootStep += Player_OnFootStep;
        BaseCounter.OnDropObject += BaseCounter_OnDropObject;
        PlateKitchenObject.OnPickUpIngredient += PlateKitchenObject_OnPickUpIngredient;
        TrashCounter.OnTrash += TrashCounter_OnTrash;
    }

    private void Player_OnFootStep(object sender, System.EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(soundManagerSO.footstep, player.transform.position);
    }

    private void TrashCounter_OnTrash(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(soundManagerSO.trash, trashCounter.transform.position);
    }

    private void PlateKitchenObject_OnPickUpIngredient(object sender, System.EventArgs e)
    {
        PlateKitchenObject plateKitchenObject = sender as PlateKitchenObject;
        PlaySound(soundManagerSO.pickUpObject, plateKitchenObject.transform.position);
    }

    private void BaseCounter_OnDropObject(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySound(soundManagerSO.dropObject, baseCounter.transform.position);
    }

    private void Player_OnPickUp(object sender, System.EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(soundManagerSO.pickUpObject, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySound(soundManagerSO.Chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliverFailure(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryManager = DeliveryManager.Instance;
        PlaySound(soundManagerSO.DeliveryFail, deliveryManager.transform.position);
    }

    private void DeliveryManager_OnDeliverSuccess(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryManager = DeliveryManager.Instance;
        PlaySound(soundManagerSO.DeliverySuccess, deliveryManager.transform.position);
    }

    private void PlaySound(List<AudioClip> audioClipList, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipList[Random.Range(0, audioClipList.Count)], position);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplyer = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplyer * volume);
    }
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(soundManagerSO.warning, position);
    }
    public void ChangeSoundEffect()
    {
        volume += .1f;
        if(volume > 1f) volume = 0f;
        PlayerPrefs.SetFloat(SOUND_EFFECT_CHANGE, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}
