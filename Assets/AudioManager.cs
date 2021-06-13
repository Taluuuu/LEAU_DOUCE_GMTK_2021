using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip attackmode, botkitapdenmure, bruitrandominsecte, cordequipete, coretensionmaximale, crachat, fshiou, fuwuwuwu, grab, retractioncorde, rikikiki, schlotchelotche, stalactitetombe;
    [SerializeField] public AudioClip[] sluurpe;
    [SerializeField] public AudioClip[] cling;
    static private AudioSource audioSrc;
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    public void PlaySound (string clip)
    {
        if(clip == "attackmode") audioSrc.PlayOneShot(attackmode);
        if (clip == "botkitapdenmure") audioSrc.PlayOneShot(botkitapdenmure);
        if (clip == "bruitrandominsecte") audioSrc.PlayOneShot(bruitrandominsecte);
        if (clip == "cling") audioSrc.PlayOneShot(cling[Random.Range(0,2)]);
        if (clip == "cordequipete") audioSrc.PlayOneShot(cordequipete);
        if (clip == "coretensionmaximale") audioSrc.PlayOneShot(coretensionmaximale);
        if (clip == "crachat") audioSrc.PlayOneShot(crachat);
        if (clip == "fshiou") audioSrc.PlayOneShot(fshiou);
        if (clip == "fuwuwuwu") audioSrc.PlayOneShot(fuwuwuwu);
        if (clip == "grab") audioSrc.PlayOneShot(grab);
        if (clip == "retractioncorde") audioSrc.PlayOneShot(retractioncorde);
        if (clip == "rikikiki") audioSrc.PlayOneShot(rikikiki);
        if (clip == "schlotchelotche") audioSrc.PlayOneShot(schlotchelotche);
        if (clip == "stalactitetombe") audioSrc.PlayOneShot(stalactitetombe);
        if (clip == "sluurpe") audioSrc.PlayOneShot(sluurpe[Random.Range(0,1)]);
    }
}
