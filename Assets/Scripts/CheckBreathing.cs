using UnityEngine;
using System.Collections;

public class CheckBreathing : MonoBehaviour {
	private Animator anim;
	private AudioClip clip;
	private float currTime;

	public int framesPerSample = 24;
	public int sampleLength = 1; //length of sample (in seconds?)

	//Start is called at the beginning
	void Start ()
    {
        anim = GetComponent<Animator>();
        //Record
        currTime = 0f;
        clip = Microphone.Start("", false, sampleLength, 44100);
    }

	// Update is called once per frame
	void Update () {
		/*
		if (Input.anyKey) {
			anim.SetBool("isBreathing", true);
		} else {
			anim.SetBool("isBreathing", false);
		}
		*/
		//if ()
	}

	// FixedUpdate is called once per physics step
	void FixedUpdate () {
		/*
		//Start recording
		if (framesSoFar == 0) {
			clip = Microphone.Start();
		}
		//Reset after 24 frames
		if (framesSoFar >= framesPerSample) {
			//framesSoFar = 0;
			Microphone.End();
			Debug.Log("Recorded.");
		}
		*/
	}

	//Algorithm from http://stackoverflow.com/questions/3881256/can-you-programmatically-detect-white-noise
	//Assumes breath out sounds like white noise
}
