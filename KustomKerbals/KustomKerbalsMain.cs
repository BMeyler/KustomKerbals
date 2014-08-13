//Copyright (c) 2014, Blake Meyler.
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
//(the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, 
//distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
//subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
//DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.IO;
using KustomKerbals.Extensions;
using UnityEngine;

[KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
public class KK : MonoBehaviour
{
    private static Rect windowPosition = new Rect(0, 0, 250, 160);
    private static GUIStyle windowStyle = null;
    private static bool buttonState = false;
    private static bool windowState = false;
    public string stringToEdit = "Jebediah Kerman Jr.";
    public float sliderValue = 0.0f;
    public float sliderValue2 = 0.0f;
    public static string Path = (KSPUtil.ApplicationRootPath + HighLogic.CurrentGame + "peristent.sfs");

    //Tells the plugin to draw the window.
    public void ShowWindow()
    {
        windowState = true;
    }

    //Tells the plugin to stop drawing the window.
    public void HideWindow()
    {
        windowState = false;
    }

    public void Awake()
    {
        RenderingManager.AddToPostDrawQueue(0, OnDraw);
    }

    public void Start()
    {
        //Sets window style to KSP default.
        windowStyle = new GUIStyle(HighLogic.Skin.window);
        Debug.Log("Kustom Kerbals loaded.");
    }

    private void OnDraw()
    {
        //Checks if player has toggled the window on or off.
        if (windowState == true)
        {
            windowPosition = GUI.Window(1234, windowPosition, OnWindow, "Kustom Kerbals", windowStyle);

            //Resets window position to middle of screen.
            if (windowPosition.x == 0f && windowPosition.y == 0f)
            {
                windowPosition = windowPosition.CenterScreen();
            }
        }
    }
    private void SpawnKerbal(int count)
    {
         ProtoCrewMember kerbal = HighLogic.CurrentGame.CrewRoster.GetNewKerbal();
    }
        
    private void SpawnKerbal(ProtoCrewMember kerbal)
    {
         kerbal.name = stringToEdit;
         kerbal.courage = sliderValue;
         kerbal.stupidity = sliderValue2;
         kerbal.isBadass = buttonState;
         kerbal.SetTimeForRespawn(0);
         kerbal.Spawn();
         kerbal.rosterStatus = ProtoCrewMember.RosterStatus.Available;
    }

    public void OnGUI()
    {
        //Button to open window.
        if (GUI.Button(new Rect(10, 20, 115, 20), "Open Kustomizer"))
        {
            ShowWindow();
            Debug.Log("KK window shown.");
        }

        //Button to close window.
        if (GUI.Button(new Rect(10, 60, 115, 20), "Hide Kustomizer"))
        {
            HideWindow();
            Debug.Log("KK window hidden.");
        }
    }

    private void OnWindow(int windowID)
    {
        //Field to type in kerbal's name.
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        stringToEdit = GUILayout.TextField(stringToEdit, 50);
        GUILayout.EndHorizontal();

        //Sets kerbal's courage. 
        GUILayout.BeginHorizontal();
        GUILayout.Label("Courage:");
        sliderValue = GUILayout.HorizontalSlider(sliderValue, 0.0f, 100.0f);
        GUILayout.EndHorizontal();

        //Sets kerbal's stupidity.
        GUILayout.BeginHorizontal();
        GUILayout.Label("Stupidity:");
        sliderValue2 = GUILayout.HorizontalSlider(sliderValue2, 0.0f, 100.0f);
        GUILayout.EndHorizontal();

        //Toggles badass state.
        GUILayout.BeginHorizontal();
        buttonState = GUILayout.Toggle(buttonState, "Badass: " + buttonState);
        GUILayout.EndHorizontal();

        //Button to create the kerbal using above paramaters.
        if (GUI.Button(new Rect(7, 125, 150, 30),  "Kreate Kustom Kerbal"))
        {
            Debug.Log("Kustom Kerbal Kreated.");
            Debug.Log("Name: " + stringToEdit);
            Debug.Log("Courage: " + sliderValue);
            Debug.Log("Stupidity: " + sliderValue2);
            Debug.Log("Badass: " + buttonState);
            SpawnKerbal(kerbal);
        }

        GUI.DragWindow();
    }

    public int kerbal { get; set; }
}