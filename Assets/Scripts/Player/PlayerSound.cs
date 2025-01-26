using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public FMODUnity.EventReference regenLife;
    public FMODUnity.EventReference regenAmmo;
    public FMODUnity.EventReference takeDamage; 
    public FMODUnity.EventReference die;
    public FMODUnity.EventReference noAmmo;


    public void PlayRegenLifeSound()
    {
        PlayReference(regenLife);
    }

    public void PlayRegenAmmoSound()
    {
        PlayReference(regenAmmo);
        Debug.LogWarning("Weshh");
    }

    public void PlayTakeDamageSound()
    {
        PlayReference(takeDamage);
    }

    public void PlayDieSound()
    {
        PlayReference(die);
    }

    public void PlayNoAmmoSound()
    {
        PlayReference(noAmmo);
    }

    private void PlayReference(FMODUnity.EventReference r)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(r,gameObject);
    }
}
 
