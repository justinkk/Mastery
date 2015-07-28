using UnityEngine;
using System.Collections;
using System; //For Math.Max

public class CheckBreathing : MonoBehaviour {
	private Animator anim;
	private AudioClip clip;
	//private float currTime;
	//private int physicsStepCount;

	//public int stepsPerSample = 24; //# of physics steps before we check the microphone again
	public int sampleLength = 1; //length of sample in seconds
	public int sampleFreq = 44100;
	public float whiteNoiseRatio = 0.6f;
	public float integralThreshold = 1.0f;

	//Start is called at the beginning
	void Start ()
    {
        anim = GetComponent<Animator>();
        //Record, looping
        clip = Microphone.Start("", true, sampleLength, sampleFreq);
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
		//Calculate how many samples to  get
		float timestep = Time.deltaTime;
		int numSamples = (int) (timestep * sampleFreq);

		//Get all samples
		float[] samples = new float[clip.samples * clip.channels];
		clip.GetData(samples, 0);

		//Get only the samples since the last frame
		int position = Microphone.GetPosition("") % sampleFreq; //GetPosition increases forever
		int startPosition = Math.Max(0, position - numSamples);
		int recentLength = position - startPosition + 1;
		float[] recentSamples = new float[recentLength];
		Array.Copy(samples, startPosition, recentSamples, 0, recentLength);

		//Test copying
		/*string[] stringSamples = new string[recentLength];
		for (int i = 0; i < recentLength; i++)
			stringSamples[i] = recentSamples[i].ToString();
		Debug.Log(string.Join(" ", stringSamples));*/

		//Test if the sound since the last frame sounds like white noise (breathing out)
		//Algorithm from http://stackoverflow.com/questions/3881256/can-you-programmatically-detect-white-noise
		//White noise has higher ratio of absolute integral of derivative
		//to absolute integral, compared to other sounds
		//Breathing out, in addition, is loud
		float integral = 0f;
		float derivIntegral = 0f;
		for (int i = 1; i < recentLength; i++) {
			integral += Math.Abs(recentSamples[i]);
			derivIntegral += Math.Abs(recentSamples[i] - recentSamples[i-1]);
		}

		Debug.Log("ratio: " + derivIntegral / integral);
		Debug.Log("integral: " + integral);

		//You are breathing out
			//Noise-ish enough                          Loud enough            
		if (derivIntegral / integral > whiteNoiseRatio && integral > integralThreshold) {
			anim.SetBool("isBreathing", true);
		} //You are not breathing out
		else {
			anim.SetBool("isBreathing", false);
		}
		//wNoiseRatio = ?0.8; //quite easily discovered, bit tricky to calculate.

		/*if((sumd1/sumd0)<wNoiseRatio)
		{ / }*/
	}

	// FixedUpdate is called once per physics step
	/*void FixedUpdate () {
		physicsStepCount++;
		//Every stepsPerSample steps, chekc the microphone for breathing
		if (physicsStepCount > stepsPerSample) {
			float[] samples = new float[clip.samples * clip.channels];
			clip.GetData(samples, 0);

			int position = Microphone.getPosition();

			//string[] stringSamples = new string[samples.Length];
			for (int i = 0; i < samples.Length; i++)
				stringSamples[i] = samples[i].ToString();
			Debug.Log(string.Join(" ", stringSamples));
		}
	}*/

	//Algorithm from http://stackoverflow.com/questions/3881256/can-you-programmatically-detect-white-noise
	//Assumes breath out sounds like white noise
}
