using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowLineRenderer : MonoBehaviour
{
	LineRenderer lineRendrer;
	private float counter;
	private float dist;

	public Transform origin;
	public Transform destination;

	public Transform pointA;
	public Transform pointB;

	public float point2YPosition = 2;
	public float vertexCount = 10;

	public float lineDrawSpeed = 6f;
	private void Awake()
	{
		lineRendrer = GetComponent<LineRenderer>();
		lineRendrer.SetPosition(0, origin.position);
		lineRendrer.SetWidth(0.5f, 0.45f);

		dist = Vector2.Distance(origin.position, destination.position);
	}
	private void Update()
	{
		//pointB.transform.position = new Vector3(origin.transform.position.x + destination.transform.position.x, point2YPosition, (origin.transform.position.z + destination.transform.position.z) / 2);

		//var pointList = new List<Vector3>();

		//for (float ratio = 0; ratio <= 1; ratio += (1/vertexCount))
		//{
		//	var tangent1 = Vector3.Lerp(origin.transform.position, pointB.transform.position,ratio);
		//	var tangent2 = Vector3.Lerp(pointB.transform.position, destination.transform.position,ratio);
		//	var curve = Vector3.Lerp(tangent1, tangent2, ratio);

		//	pointList.Add(curve);
		//}


		lineRendrer.SetPosition(0, new Vector3(origin.position.x, origin.position.y, -10));
		lineRendrer.SetPosition(1, destination.position);
	}
}
