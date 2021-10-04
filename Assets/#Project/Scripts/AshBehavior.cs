using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AshBehavior : MonoBehaviour
{
    public Material[] materials;
    private NavMeshAgent agent;
    private CubeBehavior cubeDestination;
    public int colorIndex;
    public bool loaded = false;

    // Start is called before the first frame update
    public void Initialize() {
        if (materials == null || materials.Length < 4) {
            Debug.LogError("This component need 4 materials.", gameObject);
        }
        else {
            if (!loaded) {
                colorIndex = Random.Range(0, 3);
            }
            

            GetComponent<Renderer>().material = materials[colorIndex];
        }

        agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(CubeBehavior cube) {
        agent.SetDestination(cube.transform.position);
        cubeDestination = cube;
    }
    // Update is called once per frame

    private void ChangeColor() {
        int exchange = colorIndex;
        colorIndex = cubeDestination.colorIndex;
        cubeDestination.colorIndex = exchange;

        GetComponent<Renderer>().material = materials[colorIndex];
        cubeDestination.GetComponent<Renderer>().material = 
            materials[cubeDestination.colorIndex];
    }

    void Update() {
        if (cubeDestination != null) {
            if (Vector3.Distance(
                cubeDestination.transform.position, transform.position) < 0.5f) {
                ChangeColor();
                cubeDestination = null;
            }
        }
    }
}
