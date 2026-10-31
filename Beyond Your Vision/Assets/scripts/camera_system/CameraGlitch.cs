using UnityEngine;
using UnityEngine.UI;

public class CameraGlitch : MonoBehaviour
{
    public float baseStatic = 0.1f;
    public float burstStatic = 0.7f;
    public float minTimeBetweenBursts = 3f;
    public float maxTimeBetweenBursts = 10f;
    public float burstLength = 0.25f;
    public float switchLength = 0.35f;

    private RawImage staticImage;
    private Texture2D noise;
    private Color32[] pixels;

    private float noiseTimer;
    private float nextBurst;
    private float burstEnd;
    private float switchEnd;

    void Awake()
    {
        staticImage = GetComponent<RawImage>();

        noise = new Texture2D(128, 128);
        noise.filterMode = FilterMode.Point;
        pixels = new Color32[128 * 128];

        staticImage.texture = noise;
        staticImage.raycastTarget = false;
    }

    void OnEnable()
    {
        nextBurst = Time.time + Random.Range(minTimeBetweenBursts, maxTimeBetweenBursts);
        burstEnd = 0f;
    }

    void Update()
    {
        if (Time.time >= noiseTimer)
        {
            MakeNoise();
            noiseTimer = Time.time + 0.05f;
        }

        float alpha = baseStatic * Random.Range(0.6f, 1.4f);

        if (Time.time < switchEnd)
        {
            alpha = burstStatic;
        }
        else if (Time.time < burstEnd)
        {
            alpha = burstStatic * Random.Range(0.4f, 1f);
        }
        else if (Time.time >= nextBurst)
        {
            burstEnd = Time.time + burstLength;
            nextBurst = burstEnd + Random.Range(minTimeBetweenBursts, maxTimeBetweenBursts);
        }

        staticImage.color = new Color(1f, 1f, 1f, alpha);
    }

    public void Pulse()
    {
        switchEnd = Time.time + switchLength;
    }

    void MakeNoise()
    {
        for (int i = 0; i < pixels.Length; i++)
        {
            byte gray = (byte)Random.Range(0, 256);
            pixels[i] = new Color32(gray, gray, gray, 255);
        }

        noise.SetPixels32(pixels);
        noise.Apply();
    }
}
