using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
using MPUIKIT;

namespace MMC
{
    public class ContentScript : MonoBehaviour
    {
        public ContentObject content;
        public Side side;
        public int placeInQueue;
        public bool chosen;

        public TextMeshProUGUI titleText;
        public TextMeshProUGUI descriptionText;
        public MPImage graphic;
        public TextMeshProUGUI physicalText;
        public TextMeshProUGUI mentalText;
        public TextMeshProUGUI spiritualText;

        private void OnEnable()
        {
            titleText.text = content.title;
            descriptionText.text = content.description;
            graphic.sprite = content.graphic;
            physicalText.text = $"{content.physical}";
            mentalText.text = $"{content.mental}";
            spiritualText.text = $"{content.spiritual}";
        }
    }
}
