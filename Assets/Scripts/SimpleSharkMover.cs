using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/*
 * Hacked-together script that loads in tmp-data and tmp-eas, then moves it
 * at 24 FPS. Data is loaded and integrated at startup, so expect a small delay at Start.
 */
public class SimpleSharkMover : MonoBehaviour {

    /*
     * Two CSV files, the first containing accelerometer data in the first
     * three columns, the second containing orientation data.
     */
    public TextAsset movementData;
    public TextAsset orientationData;

    public float inputSampleRate = 20.0f;
    public float startTime = 20.0f;

    private List<Vector3> position;
    private List<Vector3> orientation;

    private float currentTime;

	// Use this for initialization
	void Start () {
        currentTime = startTime;

        List<Vector3> acceleration = ParseFloatingCSV(movementData.text, 1);
        position = AccelToPos(acceleration);
        orientation = ParseFloatingCSV(orientationData.text, 2);

        Assert.AreEqual(position.Count, orientation.Count,
            "Position and orientation lists do not have the same number of elements." +
            " (" + position.Count + " != " + orientation.Count + ")");
        Assert.AreNotEqual(position.Count, 0,
            "Lists are empty or could not be read.");
    }
	
	void Update () {
        if (position.Count == 0)
            return;

        currentTime %= (position.Count / inputSampleRate);
        float currentFrame = currentTime * inputSampleRate;

        int frame1 = (int) currentFrame;
        int frame2 = frame1 + 1;
        float lerpAmount = currentFrame - frame1;
        
        if (lerpAmount < 0.0001f || frame2 >= position.Count)
        {
            transform.localPosition = position[frame1];
            transform.localEulerAngles = orientation[frame1];
        } else
        {
            transform.localPosition = Vector3.Lerp(position[frame1], position[frame2], lerpAmount);
            transform.localEulerAngles = Vector3.Lerp(orientation[frame1], orientation[frame2], lerpAmount);
        }

        currentTime += Time.deltaTime;
	}

    private List<Vector3> ParseFloatingCSV(string csv, int rowsToSkip)
    {
        string[] lines = csv.Split('\n');
        List<Vector3> parsed = new List<Vector3>(lines.Length);
        for (int i=rowsToSkip; i<lines.Length; i++)
        {
            string line = lines[i];
            string[] floats = line.Split(',');
            if (floats[0] == "")
            {
                continue;
            }
            Assert.IsTrue(floats.Length >= 3, "Line doesn't have enough elements");
            parsed.Add(new Vector3(
                float.Parse(floats[0]),
                float.Parse(floats[1]),
                float.Parse(floats[2])
            ));

        }
        return parsed;
    }

    /*
     * Double-integrate accelerometer data into position data,
     * taking into account sample rate and whatnot
     */
    private List<Vector3> AccelToPos(List<Vector3> accel)
    {
        return accel;
    }

    
}
