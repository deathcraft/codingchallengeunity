using UnityEngine;
using UnityEngine.EventSystems;

public class MitosisCell : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private float velocityCoeff = 0.5f;

	[SerializeField]
	private float mitosisScaleCoeff = 0.4f;
	
	void Update ()
	{
		var velocity = GetRandomVelocity();
		var transformPosition = transform.position;
		transform.position = transformPosition + velocity;
	}

	private Vector3 GetRandomVelocity()
	{
		var velocity = Random.insideUnitSphere * velocityCoeff;
		return velocity;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		SplitCell();
	}

	private void SplitCell()
	{
		InstantiateChild();
		InstantiateChild();
		Destroy(gameObject);
	}

	private void InstantiateChild()
	{
		var instance = Instantiate(gameObject);
		instance.name = gameObject.name;
		instance.transform.localScale *= Random.value + mitosisScaleCoeff;
		instance.transform.position += GetRandomVelocity();
	}
}
