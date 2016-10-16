using UnityEngine;
using System.Collections;

public class EnvironmentManager : MonoBehaviour {
    public float backgroundStaticSpeed = 0.005f;
    private Renderer backgroundRenderer;
    private float movingVerticalMultiplier;
    private float movingHorizontalMultiplier;

    void Start()
    {
        backgroundRenderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        backgroundRenderer.material.SetTextureOffset("_MainTex", new Vector2(backgroundRenderer.material.mainTextureOffset.x,
            backgroundRenderer.material.mainTextureOffset.y - backgroundStaticSpeed));

        // Left and Right
        if (movingHorizontalMultiplier != 0)
        {
            backgroundRenderer.material.SetTextureOffset("_MainTex", new Vector2(backgroundRenderer.material.mainTextureOffset.x + movingHorizontalMultiplier,
            backgroundRenderer.material.mainTextureOffset.y));
        }

        // Up and Down
        if (movingVerticalMultiplier != 0)
        {
            backgroundRenderer.material.SetTextureOffset("_MainTex", new Vector2(backgroundRenderer.material.mainTextureOffset.x,
            backgroundRenderer.material.mainTextureOffset.y + movingVerticalMultiplier));
        }
    }

    public void SetMovingVerticallyMultiplier(float multiplier)
    {
        movingVerticalMultiplier = multiplier;
    }

    public void SetMovingHorizontalMultiplier(float multiplier)
    {
        movingHorizontalMultiplier = multiplier;
    }
}
