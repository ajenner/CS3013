﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public GUISkin skin;

	private float gldepth = -0.5f;
	private float startTime = 0.1f;

	public Material mat;

	private long tris = 0;
	private long verts = 0;
	private float savedTimeScale;

	private bool showfps;
	private bool showtris;
	private bool showvtx;
	private bool showfpsgraph;

	public Color lowFPSColor = Color.red;
	public Color highFPSColor = Color.green;

	public int lowFPS = 30;
	public int highFPS = 50;

	public GameObject start;

	public string url = "unity.html";

	public Color statColor = Color.yellow;

	public string[] credits= {
		"Group 32",
		"CS2013 Software Engineering Project",
		"Trinity College, Dublin"} ;
	public Texture[] crediticons;

	public enum Page {
		None,Main,Options,Credits,ReturnToLobby,Restart
	}

	private Page currentPage;

	private float[] fpsarray;
	private float fps;

	private int toolbarInt = 0;
	private string[]  toolbarstrings =  {"Audio","Graphics","Stats","System"};


	void Start() {
		fpsarray = new float[Screen.width];
		Time.timeScale = 1;
		//Comment the next line out if you don't want it to pause right at the beginning
		//PauseGame();
	}

	void OnPostRender() {
		if (showfpsgraph && mat != null) {
			GL.PushMatrix ();
			GL.LoadPixelMatrix();
			for (var i = 0; i < mat.passCount; ++i)
			{
				mat.SetPass(i);
				GL.Begin( GL.LINES );
				for (int x=0; x < fpsarray.Length; ++x) {
					GL.Vertex3(x, fpsarray[x], gldepth);
				}
				GL.End();
			}
			GL.PopMatrix();
			ScrollFPS();
		}
	}

	void ScrollFPS() {
		for (int x = 1; x < fpsarray.Length; ++x) {
			fpsarray[x-1]=fpsarray[x];
		}
		if (fps < 1000) {
			fpsarray[fpsarray.Length - 1]=fps;
		}
	}

	static bool IsDashboard() {
		return Application.platform == RuntimePlatform.OSXDashboardPlayer;
	}

	static bool IsBrowser() {
		return (Application.platform == RuntimePlatform.WindowsWebPlayer ||
			Application.platform == RuntimePlatform.OSXWebPlayer);
	}

	void LateUpdate () {
		if (showfps || showfpsgraph) {
			FPSUpdate();
		}

		if (Input.GetKeyDown("escape")) 
		{
			switch (currentPage) 
			{
			case Page.None: 
				PauseGame(); 
				break;

			case Page.Main: 
				if (!IsBeginning()) 
					UnPauseGame(); 
				break;

			default: 
				currentPage = Page.Main;
				break;
			}
		}
	}

	void OnGUI () {
		if (skin != null) {
			GUI.skin = skin;
		}
		ShowStatNums();
		ShowLegal();
		if (IsGamePaused()) {
			GUI.color = statColor;
			switch (currentPage) {
			case Page.Main: 
				MainPauseMenu(); 
				break;
			case Page.Options: 
				ShowToolbar(); 
				break;
			case Page.Credits:
				ShowCredits(); 
				break;
			case Page.ReturnToLobby:
				ReturnToLobby ();
				break;
			case Page.Restart:
				Restart();
				break;
			}
		}   
	}

	void ShowLegal() {
		if (!IsLegal()) {
			GUI.Label(new Rect(Screen.width-100,Screen.height-20,90,20),
				"jdonavan.com");
		}
	}

	bool IsLegal() {
		return !IsBrowser() || 
			Application.absoluteURL.StartsWith("http://www.jdonavan.com/") ||
			Application.absoluteURL.StartsWith("http://jdonavan.com/");

	}

	void ShowToolbar() {
		BeginPage(300,300);
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);
		switch (toolbarInt) {
		case 0: VolumeControl(); break;
		case 3: ShowDevice(); break;
		case 1: Qualities(); QualityControl(); break;
		case 2: StatControl(); break;
		}
		EndPage();
	}

	void ShowCredits() {
		BeginPage(300,300);
		foreach(string credit in credits) {
			GUILayout.Label(credit);
		}
		foreach( Texture credit in crediticons) {
			GUILayout.Label(credit);
		}
		EndPage();
	}

	void ShowBackButton() {
		if (GUI.Button(new Rect(20, Screen.height - 50, 50, 20),"Back")) {
			currentPage = Page.Main;
		}
	}

	void ShowDevice() {
		GUILayout.Label("Unity player version "+Application.unityVersion);
		GUILayout.Label("Graphics: "+SystemInfo.graphicsDeviceName+" "+
			SystemInfo.graphicsMemorySize+"MB\n"+
			SystemInfo.graphicsDeviceVersion+"\n"+
			SystemInfo.graphicsDeviceVendor);
		GUILayout.Label("Shadows: "+SystemInfo.supportsShadows);
		GUILayout.Label("Image Effects: "+SystemInfo.supportsImageEffects);
		GUILayout.Label("Render Textures: "+SystemInfo.supportsRenderTextures);
	}

	void ReturnToLobby(){
		BeginPage(300,300);
		SceneManager.LoadScene ("3dMenu");
	}

	void Restart(){
		BeginPage(300,300);
		Scene current = SceneManager.GetActiveScene ();
		string currentScene = current.name;
		//SceneManager.UnloadScene (currentScene);
		SceneManager.LoadScene (currentScene);
	}

	void Qualities() {
		switch (QualitySettings.GetQualityLevel()) 
		{
		case 0:
			GUILayout.Label("Fastest");
			break;
		case 1:
			GUILayout.Label("Fast");
			break;
		case 2:
			GUILayout.Label("Simple");
			break;
		case 3:
			GUILayout.Label("Good");
			break;
		case 4:
			GUILayout.Label("Beautiful");
			break;
		default:
			GUILayout.Label("Fantastic");
			break;
		}
	}

	void QualityControl() {
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Decrease")) {
			QualitySettings.DecreaseLevel();
		}
		if (GUILayout.Button("Increase")) {
			QualitySettings.IncreaseLevel();
		}
		GUILayout.EndHorizontal();
	}

	void VolumeControl() {
		GUILayout.Label("Volume");
		AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
	}

	void StatControl() {
		GUILayout.BeginHorizontal();
		showfps = GUILayout.Toggle(showfps,"FPS");
		showtris = GUILayout.Toggle(showtris,"Triangles");
		showvtx = GUILayout.Toggle(showvtx,"Vertices");
		showfpsgraph = GUILayout.Toggle(showfpsgraph,"FPS Graph");
		GUILayout.EndHorizontal();
	}

	void FPSUpdate() {
		float delta = Time.smoothDeltaTime;
		if (!IsGamePaused() && delta !=0.0) {
			fps = 1 / delta;
		}
	}

	void ShowStatNums() {
		GUILayout.BeginArea( new Rect(Screen.width - 100, 10, 100, 200));
		if (showfps) {
			string fpsstring= fps.ToString ("#,##0 fps");
			GUI.color = Color.Lerp(lowFPSColor, highFPSColor,(fps-lowFPS)/(highFPS-lowFPS));
			GUILayout.Label (fpsstring);
		}
		if (showtris || showvtx) {
			GetObjectStats();
			GUI.color = statColor;
		}
		if (showtris) {
			GUILayout.Label (tris+"tri");
		}
		if (showvtx) {
			GUILayout.Label (verts+"vtx");
		}
		GUILayout.EndArea();
	}

	void BeginPage(int width, int height) {
		GUILayout.BeginArea( new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}

	void EndPage() {
		GUILayout.EndArea();
		if (currentPage != Page.Main) {
			ShowBackButton();
		}
	}

	bool IsBeginning() {
		return (Time.time < startTime);
	}


	void MainPauseMenu() {
		BeginPage(200,200);
		if (GUILayout.Button (IsBeginning() ? "Play" : "Continue")) {
			UnPauseGame();

		}
		if (GUILayout.Button ("Options")) {
			currentPage = Page.Options;
		}
		if (GUILayout.Button ("Credits")) {
			currentPage = Page.Credits;
		}
		if (GUILayout.Button ("Return To Lobby")) {
			ReturnToLobby ();
		}
		if (GUILayout.Button ("Restart")) {
			Restart ();
		}
		if (IsBrowser() && !IsBeginning() && GUILayout.Button ("Restart")) {
			Application.OpenURL(url);
		}
		EndPage();
	}

	void GetObjectStats() {
		verts = 0;
		tris = 0;
		GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach (GameObject obj in ob) {
			GetObjectStats(obj);
		}
	}

	void GetObjectStats(GameObject obj) {
		Component[] filters;
		filters = obj.GetComponentsInChildren<MeshFilter>();
		foreach( MeshFilter f  in filters )
		{
			tris += f.sharedMesh.triangles.Length/3;
			verts += f.sharedMesh.vertexCount;
		}
	}

	void PauseGame() {
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;

		currentPage = Page.Main;
	}

	void UnPauseGame() {
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
		currentPage = Page.None;

		if (IsBeginning() && start != null) {
			start.SetActive(true);
		}
	}

	bool IsGamePaused() {
		return (Time.timeScale == 0);
	}

	void OnApplicationPause(bool pause) {
		if (IsGamePaused()) {
			AudioListener.pause = true;
		}
	}
}