using UnityEngine;
using System.Collections.Generic;

public class GrassManager : MonoBehaviour
{
    [Range(1f, 100f)]
    public float GrassBendingIntensity = 7.3f;
    [Range(0f, 1f)]
    public float GrassSpecularIntensity = 0f;
    [Range(0f, 100f)]
    public float GrassSwaySpeed = 1f;
    [Range(0f, 100f)]
    public float GrassSwayBendIntensity = 1f;
    [Range(0f, 100f)]
    public float GrassSwayBendTime = 1f;
    [Range(0f, 100f)]
    public float GrassSwayDisplacement = 1f;

    public List<GrassCollider> TerrainGrassCollider = new List<GrassCollider>();

    private void Update()
    {
        List<Vector4> Positions = new List<Vector4>();
        List<float> Radiuses = new List<float>();
        int i = 0;

        foreach (GrassCollider g in TerrainGrassCollider.ToArray())
        {
            if (g.Collider != null)
            {
                Positions.Add(new Vector4(g.Collider.position.x, g.Collider.position.y, g.Collider.position.z, 0));
                Radiuses.Add(g.Radius);
                i++;
            }
        }

        if (Positions.Count > 0)
        {
            Shader.SetGlobalVectorArray("_Obstacles", Positions);
            Shader.SetGlobalFloatArray("_ObstacleRadius", Radiuses);
            Shader.SetGlobalFloat("_ObstacleMax", (float)i);
            Shader.SetGlobalFloat("_BendIntensity", GrassBendingIntensity);
        }
        else
            Shader.SetGlobalFloat("_BendIntensity", 0f);

        //Shader.SetGlobalFloat("_GrassSpecular", GrassSpecularIntensity);

        Shader.SetGlobalFloat("_ShakeWindspeed", GrassSwaySpeed);
        Shader.SetGlobalFloat("_ShakeBending", GrassSwayBendIntensity);
        Shader.SetGlobalFloat("_ShakeTime", GrassSwayBendTime);
        Shader.SetGlobalFloat("_ShakeDisplacement", GrassSwayDisplacement);

    }
}