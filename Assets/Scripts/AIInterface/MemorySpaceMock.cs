using UnityEngine;

// Placeholder "reconstructed memory" destination - a simple warmly lit table
// scene built from primitives. Swap this out once real memory reconstruction
// (actual room geometry / photogrammetry) is available.
public class MemorySpaceMock : MonoBehaviour
{
    public Color warmLightColor = new Color(1f, 0.75f, 0.45f);
    public Color tableColor = new Color(0.35f, 0.22f, 0.12f);
    public Color cakeColor = new Color(0.95f, 0.9f, 0.85f);

    void Awake()
    {
        Build();
    }

    void Build()
    {
        var floor = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        floor.name = "Room Floor";
        floor.transform.SetParent(transform, false);
        floor.transform.localPosition = new Vector3(0f, -0.05f, 0f);
        floor.transform.localScale = new Vector3(6f, 0.05f, 6f);
        SetColor(floor, new Color(0.08f, 0.06f, 0.05f));

        var table = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        table.name = "Table";
        table.transform.SetParent(transform, false);
        table.transform.localPosition = new Vector3(0f, 0.4f, 1.2f);
        table.transform.localScale = new Vector3(0.8f, 0.4f, 0.8f);
        SetColor(table, tableColor);

        var cake = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cake.name = "Cake";
        cake.transform.SetParent(transform, false);
        cake.transform.localPosition = new Vector3(0f, 0.9f, 1.2f);
        cake.transform.localScale = new Vector3(0.25f, 0.08f, 0.25f);
        SetColor(cake, cakeColor);

        for (int i = 0; i < 5; i++)
        {
            float angle = i / 5f * Mathf.PI * 2f;
            var pos = new Vector3(Mathf.Cos(angle) * 0.12f, 0.98f, 1.2f + Mathf.Sin(angle) * 0.12f);

            var candle = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            candle.name = "Candle " + i;
            candle.transform.SetParent(transform, false);
            candle.transform.localPosition = pos;
            candle.transform.localScale = new Vector3(0.01f, 0.03f, 0.01f);
            SetColor(candle, new Color(0.9f, 0.85f, 0.7f));

            var lightObj = new GameObject("Candle Light " + i);
            lightObj.transform.SetParent(transform, false);
            lightObj.transform.localPosition = pos + Vector3.up * 0.03f;
            var light = lightObj.AddComponent<Light>();
            light.type = LightType.Point;
            light.color = warmLightColor;
            light.range = 1.5f;
            light.intensity = 1.2f;
        }

        var roomLightObj = new GameObject("Room Light");
        roomLightObj.transform.SetParent(transform, false);
        roomLightObj.transform.localPosition = new Vector3(0f, 2.5f, 1.2f);
        var roomLight = roomLightObj.AddComponent<Light>();
        roomLight.type = LightType.Point;
        roomLight.color = warmLightColor;
        roomLight.range = 8f;
        roomLight.intensity = 1.5f;
    }

    static void SetColor(GameObject go, Color color)
    {
        var renderer = go.GetComponent<Renderer>();
        var mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mat.SetColor("_BaseColor", color);
        renderer.material = mat;

        var collider = go.GetComponent<Collider>();
        if (collider != null) Destroy(collider);
    }
}
