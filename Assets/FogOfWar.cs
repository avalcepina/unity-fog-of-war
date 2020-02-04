using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{

    public GameObject fogOfWarPlane;
    //public Transform player;
    public LayerMask fogLayer;
    public float radius = 5f;
    private float radiusSqr { get { return radius * radius; } }

    private Mesh mesh;
    private Vector3[] vertices;
    private Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        Ray r = new Ray(transform.position, player.position - transform.position);

        RaycastHit hit;

        Debug.Log("Casting ray from " + transform.position + " to " + (player.position - transform.position));

        if (Physics.Raycast(r, out hit, Mathf.Infinity, fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = fogOfWarPlane.transform.TransformPoint(vertices[i]);
                //float dist = Mathf.Abs(Vector3.Distance(v, hit.point));

                float dist = Vector3.SqrMagnitude(v - hit.point);

                if (dist < radiusSqr)
                {

                    float alpha = Mathf.Min(colors[i].a, dist / radiusSqr);
                    colors[i].a = alpha;
                }

            }

            UpdateColor();
        }
    }


    void Initialize()
    {

        mesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }

        UpdateColor();
    }


    void UpdateColor()
    {
        mesh.colors = colors;
    }
}
