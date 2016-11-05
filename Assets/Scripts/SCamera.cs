using UnityEngine;
using System.Collections;

public class SCamera : MonoBehaviour
{
    #region Visible Variables

    [SerializeField] private float m_fCameraHeight;

    #endregion

    private GameObject m_goPlayer;

	// Use this for initialization
	void Start ()
    {
        m_goPlayer = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlacementCamera();
    }

    void PlacementCamera()
    {
        Vector3 v3CameraPosition = new Vector3();
        v3CameraPosition = m_goPlayer.transform.position;
        v3CameraPosition.y = m_fCameraHeight;
        transform.position = v3CameraPosition;
    }
}
