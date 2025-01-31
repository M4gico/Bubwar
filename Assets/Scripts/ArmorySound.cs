using UnityEngine;

public class ArmorySound : MonoBehaviour
{
    public FMODUnity.StudioGlobalParameterTrigger tirTriggerON;
    public FMODUnity.StudioGlobalParameterTrigger tirTriggerOFF;

    public FMODUnity.EventReference open;
    public FMODUnity.EventReference close;

    public FMODUnity.EventReference v1;
    public FMODUnity.EventReference v2;
    public FMODUnity.EventReference v3;
    public FMODUnity.EventReference v4;
    public FMODUnity.EventReference v5;

    public FMODUnity.EventReference h1;
    public FMODUnity.EventReference h2;
    public FMODUnity.EventReference h3;
    public FMODUnity.EventReference h4;
    public FMODUnity.EventReference h5;

    public FMODUnity.EventReference s1;
    public FMODUnity.EventReference s2;
    public FMODUnity.EventReference s3;
    public FMODUnity.EventReference s4;
    public FMODUnity.EventReference s5;

    public FMODUnity.EventReference g1;
    public FMODUnity.EventReference g2;
    public FMODUnity.EventReference g3;
    public FMODUnity.EventReference g4;
    public FMODUnity.EventReference g5;

    public void PlayOpenArmory()
    {
        PlayR(open);
        tirTriggerON.TriggerParameters();
    }

    public void PlayCloseArmory()
    {
        PlayR(close);
        tirTriggerOFF.TriggerParameters();
    }

    public void PlayVisionUp(int level = 0)
    {
        switch (level)
        {
            case 1:
                PlayR(v1); 
                break;
            case 2:
                PlayR(v2);
                break;


            case 3:

            case 4:
                PlayR(v5);
                break;
            case 5:
            default:
                break;
        }        
    }

    public void PlayHealthUp(int level = 0)
    {
        switch (level)
        {
            case 1:
                PlayR(h1);
                break;
            case 2:
                PlayR(h2);
                break;


            case 3:

            case 4:
                PlayR(h5);
                break;
            case 5:
            default:
                break;
        }
    }

    public void PlaySpeedUp(int level = 0)
    {
        switch (level)
        {
            case 1:
                PlayR(s1);
                break;
            case 2:
                PlayR(s2);
                break;

            case 3:
            case 4:
                PlayR(s5);
                break;
            default:
                break;
        }
    }

    public void PlayGunUp(int level = 0)
    {
        switch (level)
        {
            case 1:
                PlayR(g1);
                break;
            case 2:
                PlayR(g2);
                break;
            case 3:
                PlayR(g3);
                break;
            case 4:
                PlayR(g4);
                break;

            case 5:
                PlayR(g5);
                break;
            default:
                break;
        }
    }



    private void PlayR(FMODUnity.EventReference r)
    {
        FMODUnity.RuntimeManager.PlayOneShot(r);
    }
}
