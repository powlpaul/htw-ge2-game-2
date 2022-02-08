using UnityEngine;

public class Waypoints : MonoBehaviour {

	public static Transform[] points;
	public static float[] distancesFromEnd;
	public static Transform startPosition;
	public static float maxDistance;
	void Awake ()
	{
		points = new Transform[transform.childCount];
		for (int i = 0; i < points.Length; i++)
		{
			points[i] = transform.GetChild(i);
		}
		startPosition = GameObject.Find("START").transform;
		distancesFromEnd = new float[points.Length];
		for (int i = points.Length-2; i >= 0; i--)
        {
			distancesFromEnd[i] = Vector3.Distance(points[i].position, points[i + 1].position) + distancesFromEnd[i+1];
        }
		maxDistance = Vector3.Distance(startPosition.position, points[0].position) + distancesFromEnd[0];
	}
	public static float GetDistanceToEnd(Transform currPos, int posIndex)
    {
		if (posIndex < 0) posIndex = 0;
		return (Vector3.Distance(currPos.position, points[posIndex].position) + distancesFromEnd[posIndex]) / maxDistance;
    }
}
