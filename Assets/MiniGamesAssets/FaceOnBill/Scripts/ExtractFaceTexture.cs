using MiniGames;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGames
{
    public class ExtractFaceTexture : MonoBehaviour
    {
        public Camera ARCamera;
        public RenderTexture renderTex;
        public GameObject canvas;
        public Texture2D basePattern;
        private GameObject rawImage;
        private GameObject ARface;
        private bool UISetup;
        private Vector2 ini_raw_size;

        void OnEnable()
        {
            ARface = GameObject.FindGameObjectWithTag("ARFace");
            rawImage = canvas.transform.GetChild(0).gameObject;
            canvas = GameObject.FindGameObjectWithTag("rootPrefab").transform.GetChild(1).gameObject;
            ini_raw_size = rawImage.GetComponent<RectTransform>().sizeDelta;
            InvokeRepeating("GCEveryTwoSeconds", 2f, 2f);
            canvas.SetActive(false);
            UISetup = false;
        }

        void OnDisable()
        {
            CancelInvoke();
        }

        void Update()
        {
            if (ARface == null)
            {
                ARface = GameObject.FindGameObjectWithTag("ARFace");
                if (UISetup)
                {
                    canvas.SetActive(false);
                    UISetup = !UISetup;
                }
            }
            else
            {
                if (!UISetup)
                {
                    canvas.SetActive(true);
                    UISetup = !UISetup;
                }
                //calculate crop face image
                Vector3 screenPos1 = ARCamera.WorldToScreenPoint(ARface.transform.position + new Vector3(-0.1f, -0.1f, 0f));
                Vector3 screenPos2 = ARCamera.WorldToScreenPoint(ARface.transform.position + new Vector3(0.1f, 0.1f, 0f));
                float ratio = (float)renderTex.height / Screen.height;
                int w = Mathf.FloorToInt(1.8f * Mathf.Abs(screenPos1.x - screenPos2.x) * ratio);
                int h = Mathf.FloorToInt(2 * Mathf.Abs(screenPos1.y - screenPos2.y) * ratio);
                int left_x = Mathf.FloorToInt((screenPos1.x + screenPos2.x) * ratio / 2 - w / 2);
                int up_y = Mathf.FloorToInt(renderTex.height - (screenPos1.y + screenPos2.y) * ratio / 2 - h / 2);
                left_x = left_x >= 0 ? left_x : 0;
                up_y = up_y >= 0 ? up_y : 0;
                w = left_x + w < renderTex.width ? w : renderTex.width - left_x;
                h = up_y + h < renderTex.height ? h : renderTex.height - up_y;
                Texture2D result = new Texture2D(w, h);
                RenderTexture.active = renderTex;
                result.ReadPixels(new Rect(left_x, up_y, w, h), 0, 0);
                //scale the texture
                int scale_w = Mathf.FloorToInt(w * ini_raw_size.y / h);
                scale_w = scale_w > (int)ini_raw_size.x ? scale_w : (int)ini_raw_size.x;
                scale_w = scale_w < (int)ini_raw_size.y ? scale_w : (int)ini_raw_size.y;
                Color[] temp = BasicTextureEdit.ScaleTexture(result, scale_w, (int)ini_raw_size.y);
                //image Effect
                result.SetPixels(BasicTextureEdit.BrightenContrastOverlayBlendColorizeTexture(temp, new Color(0.76f, 0.78f, 0.77f), basePattern));
                result.Apply();
                rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(scale_w, ini_raw_size.y);
                rawImage.GetComponent<RawImage>().texture = result;
            }
        }

        private void GCEveryTwoSeconds()
        {
            Resources.UnloadUnusedAssets();
        }

        private void ForceUpdateARface()
        {
            if (ARface != null)
            {
                Destroy(ARface);
            }
        }
    }
}