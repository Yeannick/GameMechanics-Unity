using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static AudioClip DeathSound , GrapplingSound , CherrySound , DashSound , JumpSound , HeartSound , SpinDashSound , FinishSound;

    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        DeathSound = Resources.Load<AudioClip>("Death");
        GrapplingSound = Resources.Load<AudioClip>("Grappling");
        CherrySound = Resources.Load<AudioClip>("Cherry");
        DashSound = Resources.Load<AudioClip>("Dash");
        JumpSound = Resources.Load<AudioClip>("Jump");
        HeartSound = Resources.Load<AudioClip>("Heart");
        SpinDashSound = Resources.Load<AudioClip>("SpinDash");
        FinishSound = Resources.Load<AudioClip>("Finish");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Death":
                audioSrc.PlayOneShot(DeathSound);
                break;
            case "Grappling":
                audioSrc.PlayOneShot(GrapplingSound);
                break;
            case "Cherry":
                audioSrc.PlayOneShot(CherrySound);
                break;
            case "Dash":
                audioSrc.PlayOneShot(DashSound);
                break;
            case "Jump":
                audioSrc.PlayOneShot(JumpSound,0.01f);
                break;
            case "Heart":
                audioSrc.PlayOneShot(HeartSound);
                break;
            case "SpinDash":
                audioSrc.PlayOneShot(SpinDashSound,0.1f);
                break;
            case "Finish":
                audioSrc.PlayOneShot(FinishSound);
                break;
        }
    }
}
