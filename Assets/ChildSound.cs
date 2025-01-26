using UnityEngine;

public class ChildSound : MonoBehaviour
{
    public FMODUnity.EventReference takeDamageSound; 
    public FMODUnity.EventReference deathSound;

    public void PlayTakeDamageSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(takeDamageSound, gameObject);
    }

    public void PlayDeathSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(deathSound, gameObject);
    }
}
