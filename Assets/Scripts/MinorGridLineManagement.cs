using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorGridLineManagement : MonoBehaviour {

    public GameObject gridLineObject;

    public int numGridLines = 10;
    public float gridLineSpacing = 1.0f;

	// Create our minor grid lines
	void Start () {
        Quaternion horRotation = Quaternion.identity;
        Quaternion verRotation = Quaternion.AngleAxis(90.0f, Vector3.up);
        float extent = numGridLines * gridLineSpacing / 2.0f;

        // Horizontal grid lines along the Z axis
        for (float z = -extent; z < extent; z += gridLineSpacing)
        {
            Vector3 pos = new Vector3(0.0f, 0.0f, z);
            Instantiate(gridLineObject, pos, horRotation, transform);
        }

        // Vertical grid lines along the X axis
        for (float x = -extent; x < extent; x += gridLineSpacing)
        {
            Vector3 pos = new Vector3(x, 0.0f, 0.0f);
            Instantiate(gridLineObject, pos, verRotation, transform);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
