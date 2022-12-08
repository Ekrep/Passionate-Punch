using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject cam;

    //iki veriyi de tutan ayrý bir class'in listesi olusturulabilir zaman az oldugundan bu yontemi sectim.
    public List<CharacterSettings> characterDatas;
    public List<GameObject> characterPrefabs;

    public List<Transform> camPositions;

    public List<GameObject> characterSpotLights;


    private int index = 0;


    public float moveSpeed;
    public float rotateSpeed;

    private bool isMoving;
    private bool isRotating;
    private void Start()
    {
        DataManager.Instance.holderData = characterDatas[0];
        DataManager.Instance.holdedCharacter = characterPrefabs[0];
    }



    public void SelectionRight()
    {
        if (!isMoving && !isRotating && index < characterDatas.Count - 1)
        {
            index++;
            DataManager.Instance.holderData = characterDatas[index];
            DataManager.Instance.holdedCharacter = characterPrefabs[index];
            StopCoroutine(MoveCam(0));
            StopCoroutine(RotateCam(0));
            StartCoroutine(MoveCam(index));
            StartCoroutine(RotateCam(index));
        }

    }
    public void SelectionLeft()
    {
        if (!isMoving && !isRotating && index > 0)
        {
            index--;
            DataManager.Instance.holderData = characterDatas[index];
            DataManager.Instance.holdedCharacter = characterPrefabs[index];
            StopCoroutine(MoveCam(0));
            StopCoroutine(RotateCam(0));
            StartCoroutine(MoveCam(index));
            StartCoroutine(RotateCam(index));
        }

    }

    public void PressSelect()
    {
        SceneManager.LoadScene("GameScene");

    }

    IEnumerator MoveCam(int listIndex)
    {
        for (int i = 0; i < characterSpotLights.Count; i++)
        {
            if (listIndex != i)
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
