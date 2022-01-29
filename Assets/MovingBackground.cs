using UnityEngine;

public class MovingBackground : MonoBehaviour
{

	public Rigidbody2D firstLoop;
	public Rigidbody2D secondLoop;
	public Rigidbody2D thirdLoop;

	public float yPosToDissapear = -48.2f;
	[SerializeField] float distance = 86.42f;
	[SerializeField] float distanceToDissapear = 180;
	[SerializeField] float distanceToDissapearStart = 90;
	bool start;
	[SerializeField] float ySize;

	public float movementSpeed = 0.5f;
	Vector2[] startPositions;

	[SerializeField] Transform lastAppearedMap;

	private void Awake()
	{
		start = true;
		startPositions = new Vector2[3];
		lastAppearedMap = thirdLoop.transform;
		startPositions[0] = firstLoop.transform.position;
		startPositions[1] = secondLoop.transform.position;
		startPositions[2] = thirdLoop.transform.position;
	}
	// Update is called once per frame
	void Update()
	{
		if (firstLoop.gameObject.activeInHierarchy)
		{
			firstLoop.velocity = new Vector2(0, (-movementSpeed));
		}
		if (secondLoop.gameObject.activeInHierarchy)
		{
			secondLoop.velocity = new Vector2(0, (-movementSpeed));
		}
		if (thirdLoop.gameObject.activeInHierarchy)
		{
			thirdLoop.velocity = new Vector2(0, (-movementSpeed));
		}
		ManageMaps();
	}


	public void ManageMaps()
	{

		if (firstLoop.gameObject.activeInHierarchy && firstLoop.transform.position.y <= yPosToDissapear)
		{
			InitNewMap(0);
			//firstLoop.gameObject.SetActive(false);
		}
		if (secondLoop.gameObject.activeInHierarchy && secondLoop.transform.position.y <= yPosToDissapear)
		{
			InitNewMap(1);
			//secondLoop.gameObject.SetActive(false);
		}
		if (thirdLoop.gameObject.activeInHierarchy && thirdLoop.transform.position.y <= yPosToDissapear)
		{
			InitNewMap(2);
			//thirdLoop.gameObject.SetActive(false);
		}
	}
	void InitNewMap(int _index)
	{
		if (_index == 0)
		{
			firstLoop.position = new Vector2(0, thirdLoop.transform.position.y + distance);
			lastAppearedMap = firstLoop.gameObject.transform;

		}
		else if (_index == 1)
		{
			secondLoop.position = new Vector2(0, firstLoop.transform.position.y + distance);
			lastAppearedMap = secondLoop.gameObject.transform;
		}
		else if (_index == 2)
		{
			thirdLoop.position = new Vector2(0, secondLoop.transform.position.y + distance);
			lastAppearedMap = thirdLoop.gameObject.transform;
		}
	}
}
