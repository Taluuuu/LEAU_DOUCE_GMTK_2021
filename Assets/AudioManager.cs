using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip attackmode, botkitapdenmure, cordequipete, coretensionmaximale, crachat, fshiou, grab, retractioncorde, schlotchelotche, stalactitetombe;
    [SerializeField] public AudioClip[] sluurpe;
    [SerializeField] public AudioClip[] cling;
    [SerializeField] public AudioClip[] environnement;
    static private AudioSource audioSrc;
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (Random.Range(0, 1200) == 1) audioSrc.PlayOneShot(environnement[Random.Range(0,3)]);
    }
    public void PlaySound (string clip)
    {
        if(clip == "attackmode") audioSrc.PlayOneShot(attackmode);
        if (clip == "botkitapdenmure") audioSrc.PlayOneShot(botkitapdenmure);
        if (clip == "cling") audioSrc.PlayOneShot(cling[Random.Range(0,3)]);
        if (clip == "cordequipete") audioSrc.PlayOneShot(cordequipete);
        if (clip == "coretensionmaximale") audioSrc.PlayOneShot(coretensionmaximale);
        if (clip == "crachat") audioSrc.PlayOneShot(crachat);
        if (clip == "fshiou") audioSrc.PlayOneShot(fshiou);
        if (clip == "grab") audioSrc.PlayOneShot(grab);
        if (clip == "retractioncorde") audioSrc.PlayOneShot(retractioncorde);
        if (clip == "schlotchelotche") audioSrc.PlayOneShot(schlotchelotche);
        if (clip == "stalactitetombe") audioSrc.PlayOneShot(stalactitetombe);
        if (clip == "sluurpe") audioSrc.PlayOneShot(sluurpe[Random.Range(0,2)]);
    }
}
