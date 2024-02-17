#nullable enable
using Game.Common.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scene.Sample
{
    public class HealthGage : MonoBehaviour
    {
        [SerializeField] Slider? gage;
        [SerializeField] UIText? text;

        int maxHealthValue;

        public int Health { get; set; }

        public void Setup(int health)
        {
            Health = health;
            maxHealthValue = health;
        }

        public void SetGage()
        {
            if (gage == null) return;

            var progress = (float)Health / maxHealthValue;
            gage.value = progress;
        }

        public void SetHealthText()
        {
            text.SetTextSafe($"{Health}");
        }
    }
}