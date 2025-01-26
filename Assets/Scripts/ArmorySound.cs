using UnityEngine;

public class ArmorySound : MonoBehaviour
{
    //public FMODUnity.EventReference open;
    //public FMODUnity.EventReference close;

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

    public void PlayVisionUp(int level = 0)
    {

    }

    public void PlayHealthUp(int level = 0)
    {

    }

    public void PlaySpeedUp(int level = 0)
    {

    }

    public void PlayGunUp(int level = 0)
    {

    }



    private void PlayR(FMODUnity.EventReference r)
    {
        FMODUnity.RuntimeManager.PlayOneShot(r);
    }
}
