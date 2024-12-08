using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            Ray ray = _camera.ScreenPointToRay(screenCenter);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicatorCoroutine(hit.point, new Vector3(0.2f, 0.2f, 0.2f))); 
                    Debug.DrawLine(this.transform.position, hit.point, Color.green, 6);
                }
            }
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 12; 
        style.normal.textColor = Color.white; 

        string text = "*";
        Vector2 textSize = style.CalcSize(new GUIContent(text));

        float posX = _camera.pixelWidth / 2 - textSize.x / 2; 
        float posY = _camera.pixelHeight / 2 - textSize.y / 2; 

        GUI.Label(new Rect(posX, posY, textSize.x, textSize.y), text, style); 
    }

    private IEnumerator SphereIndicatorCoroutine(Vector3 pos, Vector3 size)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        sphere.transform.localScale = size; 

        yield return new WaitForSeconds(6);
        Destroy(sphere);
    }
}