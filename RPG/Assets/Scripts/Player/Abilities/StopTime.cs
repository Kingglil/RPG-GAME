using System.Collections;
using UnityEngine;

public class StopTime : MonoBehaviour
{

    private bool stopTimeOffCooldown = true;
    private float stopTimeDuration = 0.01f;
    private float stopTimeCooldown = 20f;

    void Update()
    {
        if (Input.GetButtonDown("LB"))
        {
            if (!AbilitiesManager.stopTime && stopTimeOffCooldown)
            {
                AbilitiesManager.stopTime = true;
                StartCoroutine(StopTimeDuration());
            }
        }
    }

    IEnumerator StopTimeDuration()
    {
        Time.timeScale = 0.01f;
        Time.fixedDeltaTime = 0.016f * 0.01f;

        for (float i = 0; i < stopTimeDuration * 60; i += 0.01f)
        {
            yield return new WaitForSeconds(0.01f / (stopTimeDuration * 6000));
            UIManager.Duration(i / (stopTimeDuration * 60));
        }

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.016f;
        AbilitiesManager.stopTime = false;
        stopTimeOffCooldown = false;
        StartCoroutine(StopTimeCooldown());
        StopCoroutine(StopTimeDuration());
    }

    IEnumerator StopTimeCooldown()
    {
        for (int i = 0; i < stopTimeCooldown * 60; i++)
        {
            yield return new WaitForSeconds(1 / 60);
            UIManager.Duration(1 - (i / (stopTimeCooldown * 60)));
        }
        stopTimeOffCooldown = true;
        StopCoroutine(StopTimeCooldown());
    }
}
