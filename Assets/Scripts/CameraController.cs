using UnityEngine;
using System.Collections;

public enum CameraFocus{ZOOMIN, ZOOMOUT, RESET}
public class CameraController : MonoBehaviour {

	private float initMouse;
	private float currentMouse;
	private CameraFocus focus;

	const float MINBOUNDARY = 0.5f;
	const float MAXBOUNDARY = 3.5f;

	public float cameraZoomCoefficient;	//default = 0.2f

	// Use this for initialization
	void Start () {
		initMouse = Input.GetAxis ("Mouse ScrollWheel");
		currentMouse = initMouse;	
		focus = CameraFocus.RESET;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentMouse = initMouse;
		initMouse = Input.GetAxis ("Mouse ScrollWheel");
		if(initMouse != 0)
		{
			if (currentMouse + initMouse < 0) 
				focus = CameraFocus.ZOOMOUT;
			else if(currentMouse + initMouse > 0)
				focus = CameraFocus.ZOOMIN;
			CameraZoom ();
		}
	}

	private void CameraZoom()
	{
		if (focus == CameraFocus.ZOOMOUT) 
		{
			if(Camera.main.orthographicSize >= MAXBOUNDARY)
				Camera.main.orthographicSize = MAXBOUNDARY;
			else
				Camera.main.orthographicSize += cameraZoomCoefficient;
		}
		else if(focus == CameraFocus.ZOOMIN)
		{
			if(Camera.main.orthographicSize <= MINBOUNDARY)
				Camera.main.orthographicSize = MINBOUNDARY;
			else
				Camera.main.orthographicSize -= cameraZoomCoefficient;
		}
		else if(focus == CameraFocus.RESET)
		{
			Camera.main.orthographicSize = 3;
		}

	}

}
