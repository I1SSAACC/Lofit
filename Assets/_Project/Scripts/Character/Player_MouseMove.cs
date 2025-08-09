using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MouseMove : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2.0f; // Чувствительность мыши
    [SerializeField] private float maxYAngle = 80.0f; // Максимальный угол вращения по вертикали
    [SerializeField] private float fovSensitivity = 5f; // Чувствительность для изменения поля зрения
    [SerializeField] private Camera playerCamera; // Ссылка на камеру игрока
    [SerializeField] private float sideTiltAmount = 2.0f; // Наклон камеры при боковом движении
    [SerializeField] private float runTiltAmount = 4.0f; // Увеличенный наклон при беге
    [SerializeField] public GameObject Camera; // Ссылка на камеру игрока

    float xRot;
    float yRot;
    float xRotCurrent;
    float yRotCurrent;
    float currentVelosityX;
    float currentVelosityY;
    public GameObject rig;
    public float smoothTime = 0.1f;
    float rotationX = 0.0f;
    public GameObject ray;
    public GameObject playerGameObject;



    private void Update()
    {
        /*

        xRot += Input.GetAxis("Mouse X") * sensitivity;
        yRot += Input.GetAxis("Mouse Y") * sensitivity;
        yRot = Mathf.Clamp(yRot, -90, 90);


        xRotCurrent = Mathf.SmoothDamp(xRotCurrent, xRot, ref currentVelosityX, smoothTime);
        yRotCurrent = Mathf.SmoothDamp(yRotCurrent, yRot, ref currentVelosityY, smoothTime);
        ray.transform.rotation = Quaternion.Euler(-yRotCurrent, xRotCurrent, 0f).normalized;
        rig.transform.rotation = Quaternion.Euler(-yRotCurrent, xRotCurrent, 0f).normalized;
        Camera.transform.rotation = rig.transform.rotation;
        playerGameObject.transform.rotation = Quaternion.Euler(0f, xRotCurrent, 0f).normalized;

        // Изменяем поле зрения в зависимости от движения
        UpdateFieldOfView();

        // Динамика наклона камеры при движении
        UpdateCameraTilt();
        */

    }

    private void UpdateFieldOfView()
    {
        if (Input.GetKey(KeyCode.LeftShift)) // Если игрок бежит
        {
        //    playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 70, Time.deltaTime * fovSensitivity); // Увеличиваем FOV
        }
        else
        {
     //       playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 60, Time.deltaTime * fovSensitivity); // Возвращаем FOV к норме
     //       playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 60, Time.deltaTime * fovSensitivity); // Возвращаем FOV к норме
        }
    }

    private void UpdateCameraTilt()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    }
}
