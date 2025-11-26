using UnityEngine;
using UnityEngine.Rendering.Universal;   // Light2D

[RequireComponent(typeof(Light2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class SpotLight2DMouseZoom : MonoBehaviour
{
    public Light2D light2D;
    public PolygonCollider2D polyCollider;

    [Header("Zoom")]
    public float scrollSpeed = 1f;
    public float minRadius = 0.5f;
    public float maxRadius = 10f;

    [Header("Forma del cono")]
    [Range(10, 60)]
    public int segments = 20;  

    private void Reset()
    {
        light2D = GetComponent<Light2D>();
        polyCollider = GetComponent<PolygonCollider2D>();
        polyCollider.isTrigger = true;
    }

    private void OnValidate()
    {
        if (!light2D) light2D = GetComponent<Light2D>();
        if (!polyCollider) polyCollider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (Mathf.Abs(scroll) > 0.01f)
        {
            float newRadius = light2D.pointLightOuterRadius + scroll * scrollSpeed;
            newRadius = Mathf.Clamp(newRadius, minRadius, maxRadius);
            light2D.pointLightOuterRadius = newRadius;

            ActualizarConoCollider(newRadius);
        }
    }

    private void ActualizarConoCollider(float radius)
    {
        if (!polyCollider || !light2D) return;

        
        float outerAngle = light2D.pointLightOuterAngle;   
        float halfAngleRad = outerAngle * 0.5f * Mathf.Deg2Rad;

        
        Vector2[] points = new Vector2[segments + 2];
        points[0] = Vector2.zero; 

        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;                   // 0..1
            float angle = -halfAngleRad + (2f * halfAngleRad * t);
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            points[i + 1] = new Vector2(x, y);
        }

        polyCollider.pathCount = 1;
        polyCollider.SetPath(0, points);
    }
}
