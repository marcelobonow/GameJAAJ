using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class flicker : MonoBehaviour
{
    public bool checkAssigned;
    public bool displayScriptErrors = true;

    public enum UseType { Color, Intensity, Range, Off }
    public UseType useType;

    public enum WaveFunction { Noise, Sin, Tri, Sqr, Saw, SawInv }
    public WaveFunction waveFunction;

    public bool noiseUpdatePF = true;
    public bool noiseSmoothFade;
    public float noiseSmoothSpeed = 4;

    public float timescaleMin = 0.1f;

    public float coreBase = 0.8f;
    public float amplitude = 0.4f;
    public float phase;
    public float frequency = 0.5f;

    public bool useOutsideLight;
    public List<Light> outsideLight;

    private Color mainColor;
    private float mainValueI;
    private float mainValueR;

    [HideInInspector]
    public List<Color> mainOutColor;
    [HideInInspector]
    public List<float> mainOutValueI;
    [HideInInspector]
    public List<float> mainOutValueR;

    private Light lightS;

    private bool error1;
    private bool error2;
    private bool assigned;

    private float x;
    private float y;
    private float z;

    private float noiseD;
    private bool noiseGo;

    void Update()
    {
        phase = Mathf.Clamp01(phase);
        if (waveFunction == WaveFunction.Noise && !noiseUpdatePF)
        {
            if (noiseD > frequency)
            {
                noiseD = 0;
                noiseGo = true;
            }
            else
            {
                noiseD += Time.deltaTime;
                noiseGo = false;
            }
        }

        if (lightS == null && !useOutsideLight)
        {
            lightS = gameObject.GetComponent<Light>();
        }
        if (checkAssigned || !assigned)
        {
            CheckAssigned();
        }

        if (((!error1 && !useOutsideLight) || (!error2 && useOutsideLight)) && assigned && useType != UseType.Off && Time.timeScale > timescaleMin)
        {
            CalWaveFunc();
            if (useOutsideLight)
            {
                for (int i = 0; i < outsideLight.Count; i++)
                {
                    if (useType == UseType.Color)
                    {
                        outsideLight[i].color = mainOutColor[i] * (z);
                    }
                    else if (useType == UseType.Intensity)
                    {
                        outsideLight[i].intensity = mainOutValueI[i] * (z);
                    }
                    else if (useType == UseType.Range)
                    {
                        outsideLight[i].range = mainOutValueR[i] * (z);
                    }
                    if (mainOutValueI[i] != outsideLight[i].intensity && (useType != UseType.Intensity))
                    {
                        outsideLight[i].intensity = mainOutValueI[i];
                    }
                    if (mainOutValueR[i] != outsideLight[i].range && (useType != UseType.Range))
                    {
                        outsideLight[i].range = mainOutValueR[i];
                    }
                    if (mainOutColor[i] != outsideLight[i].color && (useType != UseType.Color))
                    {
                        outsideLight[i].color = mainOutColor[i];
                    }
                }
            }
            else
            {
                if (useType == UseType.Color)
                {
                    lightS.color = mainColor * (z);
                }
                else if (useType == UseType.Intensity)
                {
                    lightS.intensity = mainValueI * (z);
                }
                else if (useType == UseType.Range)
                {
                    lightS.range = mainValueR * (z);
                }
            }
        }
        if (!useOutsideLight && !error1)
        {
            if (mainValueI != lightS.intensity && (useType != UseType.Intensity))
            {
                lightS.intensity = mainValueI;
            }
            if (mainValueR != lightS.range && (useType != UseType.Range))
            {
                lightS.range = mainValueR;
            }
            if (mainColor != lightS.color && (useType != UseType.Color))
            {
                lightS.color = mainColor;
            }
        }
    }

    void CheckAssigned()
    {
        if (!GetComponent<Light>() && displayScriptErrors && !error1 && !useOutsideLight)
        {
            Debug.LogError("ERROR MISSING COMPONENT: The gameObject " + "'" + gameObject.name + "'" + " does not have a Light component attached to it. If you wish to use a Light from another gameObject, please select the boolean variable 'Use Outside Light' for this feature.", gameObject);
            error1 = true;
        }
        else if (!useOutsideLight && (!error1 || GetComponent<Light>()))
        {
            mainColor = GetComponent<Light>().color;
            mainValueI = GetComponent<Light>().intensity;
            mainValueR = GetComponent<Light>().range;
            if (error1)
            {
                Debug.Log("FIXED MISSING COMPONENT: The missing Light component on the gameObject " + "'" + gameObject.name + "'" + " has been successfully re-established!", gameObject);
                error1 = false;
            }
            assigned = true;
        }

        if (useOutsideLight)
        {
            if (mainOutColor.Count > 0)
            {
                mainOutColor.RemoveRange(0, mainOutColor.Count);
                mainOutValueI.RemoveRange(0, mainOutValueI.Count);
                mainOutValueR.RemoveRange(0, mainOutValueR.Count);
            }
            if (OutsideLight() && error2)
            {
                error2 = false;
                Debug.Log("FIXED EMPTY VARIABLE:  The gameObject " + "'" + gameObject.name + "'" + " has re-established an index with missing Light component!", gameObject);
            }
            if (!error2)
            {
                assigned = true;
            }
        }
    }

    bool OutsideLight()
    {
        for (int i = 0; i < outsideLight.Count; i++)
        {
            if (!outsideLight[i] && displayScriptErrors && !error2)
            {
                error2 = true;
                Debug.LogError("ERROR EMPTY VARIABLE: The gameObject " + "'" + gameObject.name + "'" + " does not have a Light component attached to the variable 'Outside Light' index number " + "[ " + i + " ], to fix this either, assign a gameObject with a Light component in this space or, lower the size amount to fit your assigned lights. If you do not wish to use this feature, un-tick the boolean variable 'Use Outside Light.'", gameObject);
                return false;
            }
            else if (outsideLight[i] || !error2)
            {
                mainOutColor.Add(outsideLight[i].color);
                mainOutValueI.Add(outsideLight[i].intensity);
                mainOutValueR.Add(outsideLight[i].range);
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    void CalWaveFunc()
    {
        x = (Time.time + phase) * frequency;
        x = x - Mathf.Floor(x);

        if (waveFunction == WaveFunction.Sin)
        {
            y = Mathf.Sin(x * 2 * Mathf.PI);
        }
        else if (waveFunction == WaveFunction.Tri)
        {
            if (x < 0.5f)
            {
                y = 4.0f * x - 1.0f;
            }
            else
            {
                y = -4.0f * x - 3.0f;
            }
        }
        else if (waveFunction == WaveFunction.Sqr)
        {
            if (x < 0.5f)
            {
                y = 1.0f;
            }
            else
            {
                y = -1.0f;
            }
        }
        else if (waveFunction == WaveFunction.Saw)
        {
            y = x;
        }
        else if (waveFunction == WaveFunction.SawInv)
        {
            y = 1.0f - x;
        }
        else if (waveFunction == WaveFunction.Noise)
        {
            if (noiseGo || noiseUpdatePF)
            {
                y = 1 - (Random.value * 2);
            }
        }
        if (noiseSmoothFade && waveFunction == WaveFunction.Noise)
        {
            z = Mathf.Lerp(z, (y * amplitude) + coreBase, Time.deltaTime * noiseSmoothSpeed);
        }
        else
        {
            z = (y * amplitude) + coreBase;
        }
    }
}