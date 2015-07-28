using UnityEngine;
using System.Collections;

public class CheckBreathing : MonoBehaviour {
	private Animator anim;
	private AudioClip clip;
	private float currTime;
	private int physicsStepCount;

	public int stepsPerSample = 24; //# of physics steps before we check the microphone again
	public int sampleLength = 1; //length of sample in seconds
	public int sampleFreq = 44100;

	//Start is called at the beginning
	void Start ()
    {
        anim = GetComponent<Animator>();
        //Record
        currTime = 0f;
        clip = Microphone.Start("", false, sampleLength, sampleFreq);

        //Currently on the 0th physics step
        physicsStepCount = 0;
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
		physicsStepCount++;
		//Every stepsPerSample steps, chekc the microphone for breathing
		if (physicsStepCount > stepsPerSample) {
			float[] samples = new float[clip.samples * clip.channels];
			clip.GetData(samples, 0);

			int position = Microphone.getPosition();

			/*string[] stringSamples = new string[samples.Length];
			for (int i = 0; i < samples.Length; i++)
				stringSamples[i] = samples[i].ToString();
			Debug.Log(string.Join(" ", stringSamples));*/
		}
	}

	//Algorithm from http://stackoverflow.com/questions/3881256/can-you-programmatically-detect-white-noise
	//Assumes breath out sounds like white noise
}
