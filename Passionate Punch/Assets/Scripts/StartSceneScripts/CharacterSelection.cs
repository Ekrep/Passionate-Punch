using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject cam;

    public List<Transform> camPositions;

    public List<GameObject> characterSpotLights;

    public float moveSpeed;
    public float rotateSpeed;

    private bool isMoving;
    private bool isRotating;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)&&!isMoving&&!isRotating)
        {
            StopCoroutine(MoveCam(0));
            StopCoroutine(RotateCam(0));
            StartCoroutine(MoveCam(0));
            StartCoroutine(RotateCam(0));
        }
        if (Input.GetKeyDown(KeyCode.L) && !isMoving && !isRotating)
        {
            StopCoroutine(MoveCam(0));
            StopCoroutine(RotateCam(0));
            StartCoroutine(MoveCam(1));
            StartCoroutine(RotateCam(1));
        }
    }


    IEnumerator MoveCam(int listIndex)
    {
        for (int i = 0; i < characterSpotLights.Count; i++)
        {
            if (listIndex!=i)
            {
                characterSpotLights[i].SetActive(false);
            }

        }
        isMoving = true;
        while (Vector3.Distance(cam.transform.position, camPositions[listIndex].position) > 0.1f)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPositions[listIndex].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        characterSpotLights[listIndex].SetActive(true);
        isMoving = false;
    }
    IEnumerator RotateCam(int listIndex)
    {
        isRotating = true;
        while (Vector3.Distance(cam.transform.rotation.eulerAngles, camPositions[listIndex].rotation.eulerAngles) > 0.1f)
        {
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, camPositions[listIndex].rotation, rotateSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isRotating = false;
    }
}
