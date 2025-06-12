using UnityEngine;
using UnityEngine.Rendering;

public class SoundManagerScript : MonoBehaviour
{
    [SerializeField] private SoundRefsSO soundRefs;
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += PlaySuccessSound;
        DeliveryManager.Instance.OnRecipeFailure += PlayFailureSound;
        CuttingCounter.OnAnyCut += PlayCuttingSound;
        PlayerController.Instance.OnPickup += PlayPickupSound;
        BaseCounter.OnAnyObjectPlacedOnCounter += PlayObjectPlacedOnCounterSound;
        TrashCounterScript.OnAnyObjectDestroyed += PlayTrashSound;
    }

    private void PlaySuccessSound(object sender, System.EventArgs e)
    {

        DeliveryCounterVisual DeliveryCounter = DeliveryCounterVisual.Instance;
        PlaySoundArrays(soundRefs.deliverySuccess, DeliveryCounter.transform.position);
    }

    private void PlayFailureSound(object sender, System.EventArgs e)
    {

        DeliveryCounterVisual DeliveryCounter = DeliveryCounterVisual.Instance;
        PlaySoundArrays(soundRefs.deliveryFail, DeliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySoundArrays(AudioClip[] audioClips, Vector3 position, float volume = 80f)
    {
        if (audioClips.Length == 0) return;
        int randomIndex = Random.Range(0, audioClips.Length);
        AudioSource.PlayClipAtPoint(audioClips[randomIndex], position, volume);
    }

    private void PlayCuttingSound(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        if (cuttingCounter != null)
        {
            PlaySoundArrays(soundRefs.chop, cuttingCounter.transform.position);
        }
    }

    private void PlayPickupSound(object sender, System.EventArgs e)
    {
        PlayerController player = sender as PlayerController;
        if (player != null)
        {
            PlaySoundArrays(soundRefs.objectPickup, player.transform.position);
        }
    }

    private void PlayObjectPlacedOnCounterSound(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        if (baseCounter != null)
        {
            PlaySoundArrays(soundRefs.objectDrop, baseCounter.transform.position);
        }
    }

    private void PlayTrashSound(object sender, System.EventArgs e)
    {
        TrashCounterScript trashCounter = sender as TrashCounterScript;
        if (trashCounter != null)
        {
            PlaySoundArrays(soundRefs.trash, trashCounter.transform.position);
        }
    }

}
