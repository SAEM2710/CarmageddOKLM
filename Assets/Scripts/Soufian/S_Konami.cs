using UnityEngine;
using System.Collections;

public class S_Konami : MonoBehaviour
{
    /*private string[] konamiCode = ["UpArrow", "UpArrow", "DownArrow", "DownArrow", "LeftArrow", "RightArrow", "LeftArrow", "RightArrow", "B", "A", "Return"];
    private int currentPos = 0;
    private bool inKonami = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (inKonami)
        {
            if (!GetComponent<CameraFilterPack_FX_Glitch1> ().enabled)
            {
                GetComponent<CameraFilterPack_FX_Glitch1> ().enabled = true;
            }
            else
            {
                GetComponent<CameraFilterPack_FX_Glitch1> ().enabled = false;
            }
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && Input.anyKeyDown && !inKonami && e.keyCode.ToString() != "None")
        {
            konamiFunction(e.keyCode);
        }
    }

    void konamiFunction(string incomingKey)
    {

        string incomingKeyString = incomingKey.ToString();
        if (incomingKeyString == konamiCode[currentPos])
        {
            print("Unlocked part " + (currentPos + 1) + "/" + konamiCode.Length + " with " + incomingKeyString);
            currentPos++;

            if ((currentPos + 1) > konamiCode.Length)
            {
                print("You master Konami.");
                inKonami = true;
                currentPos = 0;
            }
        }
        else
        {
            print("You fail Konami at position " + (currentPos + 1) + ", find the ninja in you.");
            currentPos = 0;
        }

    }
    */

}
