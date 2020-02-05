using UnityEngine;
using System.Collections;

public class DrawFPS : MonoBehaviour
{
	public  float updateInterval = 0.5F;
	private float accum = 0; // FPS accumulated over the interval
	private int   frames = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	
	UITextInstance fpsText;


	void Awake()
	{
		useGUILayout = false;
	}

	void Start()
	{
		timeleft = updateInterval;
		
		var text = UIHelper.getText();
		fpsText = text.addTextInstance("FPS", 135, 16);
		fpsText.setColorForAllLetters(Colors.HighlightText);
	}

	void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;
    
		// Interval ended - update GUI text and start new interval
		if (timeleft <= 0.0) {
			// display two fractional digits (f2 format)
			float fps = accum / frames;
			string format = System.String.Format("{0:F2}", fps);
			fpsText.text = format;

			if (fps < 30) {
				
				fpsText.setColorForAllLetters(Color.yellow);
			} else {
				if (fps < 10)
					fpsText.setColorForAllLetters(Color.red);
				else
					fpsText.setColorForAllLetters(Colors.HighlightText);
			}
	
			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
		}
	}

}
