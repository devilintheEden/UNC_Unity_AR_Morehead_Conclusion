using MiniGames;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGames
{
    public class SaveAndShare : MonoBehaviour
    {
        private Button saveBtn;
        private Button shareBtn;
        private GameObject blankMoney;

        void Start()
        {
            saveBtn = transform.GetChild(2).gameObject.GetComponent<Button>();
            shareBtn = transform.GetChild(3).gameObject.GetComponent<Button>();
            blankMoney = transform.GetChild(1).gameObject;
            saveBtn.onClick.AddListener(SaveImage);
            shareBtn.onClick.AddListener(ShareImage);
        }

        private void SaveImage()
        {
            StartCoroutine(SaveImageCoro());
        }

        private IEnumerator SaveImageCoro()
        {
            yield return new WaitForEndOfFrame();
            Texture2D save = HelpGetImage();
            save.Apply();
            NativeGallery.SaveImageToGallery(save, "GalleryTest", "save_face_img.png");
            Destroy(save);
        }

        private void ShareImage()
        {
            StartCoroutine(ShareImageCoro());
        }

        private IEnumerator ShareImageCoro()
        {
            yield return new WaitForEndOfFrame();
            Texture2D share = HelpGetImage();
            share.Apply();
            string filePath = Path.Combine(Application.temporaryCachePath, "shared_img.png");
            File.WriteAllBytes(filePath, share.EncodeToPNG());
            Destroy(share);
            new NativeShare().AddFile(filePath)
                .SetSubject("").SetText("")
                .Share();
        }

        private Texture2D HelpGetImage()
        {
            Texture2D screenCap = ScreenCapture.CaptureScreenshotAsTexture();
            float ratio = screenCap.height / GetComponent<CanvasScaler>().referenceResolution.y;
            int w = Mathf.FloorToInt(blankMoney.GetComponent<RectTransform>().rect.width * ratio);
            int h = Mathf.FloorToInt(blankMoney.GetComponent<RectTransform>().rect.height * ratio);
            int left_x = Mathf.FloorToInt(screenCap.width / 2 + blankMoney.GetComponent<RectTransform>().anchoredPosition.x * ratio - w / 2);
            int up_y = Mathf.FloorToInt(screenCap.height / 2 + blankMoney.GetComponent<RectTransform>().anchoredPosition.y * ratio - h / 2);
            Texture2D result = BasicTextureEdit.CropTexture(screenCap, new Vector2(w, h), new Vector2(left_x, up_y));
            Destroy(screenCap);
            return result;
        }

    }
}
