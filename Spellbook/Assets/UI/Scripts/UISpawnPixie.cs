using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISpawnPixie : MonoBehaviour {

	// Public Fields
	[Tooltip("This is the object that will spawn and move towards the desired point.")]
	public GameObject pixie;
	[Tooltip("This is the position that the spawned object will move towards")]
	public Transform origin;
	[Tooltip("This is the linear move speed of the spawned objects.")]
	public float moveSpeed = 0.8F;
	[Tooltip("This is the angular move speed of the spawned objects.")]
	public float angularSpeed = 8.5F;
	[Tooltip("This is the axis the spawned objects will rotate around")]
	public Vector3 rotationAxis = new Vector3(0.0F, 0.0F, 1.0F);
	[Tooltip("This is the maximum radius that the object will be spawned within")]
	public float maxSpawnRadius = 20.0F;
	[Tooltip("This is the number of objects that will be spawned at once")]
	public int spawnQuantity = 3;

	// Internal Objects
	private List<GameObject> _managedObjects;

	void Start() {
		_managedObjects = new List<GameObject>();
	}

	void Update() {
		for (int index = _managedObjects.Count - 1; index >= 0; index -= 1) {
			GameObject iteratedObject = _managedObjects[index];
			iteratedObject.transform.RotateAround(origin.position, rotationAxis, angularSpeed);
			iteratedObject.transform.position = Vector3.MoveTowards(iteratedObject.transform.position, origin.position, moveSpeed);
			if (Vector3.Distance(iteratedObject.transform.position, origin.position) <= moveSpeed) {
				Destroy(iteratedObject, 2.0F);
				_managedObjects.RemoveAt(index);
			}
		}
	}

	public void SpawnPixies() {
		for (int count = 0; count < spawnQuantity; count += 1) {
			SpawnPixie(pixie);
		}
	}

	public void ClearPixies() {
		foreach (GameObject iteratedObject in _managedObjects) {
			Destroy(iteratedObject);
		}
		_managedObjects.Clear();
	}

	private void SpawnPixie(GameObject instance) {
		GameObject newInstance = Object.Instantiate(instance, origin);
		newInstance.transform.position = GetSpawnPosition();
		newInstance.transform.rotation = origin.rotation;
		_managedObjects.Add(newInstance);
	}

	private Vector3 GetSpawnPosition() {
		return new Vector3(Random.Range(-maxSpawnRadius, maxSpawnRadius), Random.Range(-maxSpawnRadius, maxSpawnRadius), origin.position.z);
	}
}
