using UnityEngine;

public class FramerateController : MonoBehaviour
{
	float deltaTime = 0.0f;
    public Color32 textColor = new Color32(255, 255, 255, 255);
	[Range(0.01f, 0.10f)] public float heightRatio = 0.03f;
	//public int width = 1920;
	//public int height = 1080;
	private int targetFramerate;

    private void Start()
    {
		targetFramerate = GameController.framerate;
    }

    void Update()
	{
		Application.targetFrameRate = targetFramerate;
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		//Screen.SetResolution(width, height, true, targetFramerate);
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * heightRatio);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = (int) (h * heightRatio);
		style.normal.textColor = textColor;
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
}
