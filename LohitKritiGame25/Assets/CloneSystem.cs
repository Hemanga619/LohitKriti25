using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSystem : MonoBehaviour
{
    public GameObject clonePrefab;
    public GameObject recordingUI; // Assign UI in Inspector

    private List<Vector3> recordedPositions = new List<Vector3>();
    private bool isRecording = false;
    private bool isReplaying = false;
    private GameObject currentClone;

    public float recordTime = 3f;
    public float replaySpeed = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isRecording && !isReplaying)
        {
            StartCoroutine(RecordCloneMovement());
        }

        if (Input.GetKeyDown(KeyCode.T) && !isRecording && recordedPositions.Count > 0)
        {
            StartCoroutine(ReplayCloneMovement());
        }
    }

    private IEnumerator RecordCloneMovement()
    {
        isRecording = true;
        recordedPositions.Clear();

        if (recordingUI != null) recordingUI.SetActive(true);

        float timer = 0f;
        while (timer < recordTime)
        {
            recordedPositions.Add(transform.position);
            timer += Time.deltaTime;
            yield return null;
        }

        isRecording = false;
        if (recordingUI != null) recordingUI.SetActive(false);
    }

    private IEnumerator ReplayCloneMovement()
    {
        isReplaying = true;
        currentClone = Instantiate(clonePrefab, recordedPositions[0], Quaternion.identity);

        foreach (Vector3 pos in recordedPositions)
        {
            if (currentClone == null) yield break;
            currentClone.transform.position = pos;
            yield return new WaitForSeconds(Time.deltaTime * replaySpeed);
        }

        isReplaying = false;
    }
}

