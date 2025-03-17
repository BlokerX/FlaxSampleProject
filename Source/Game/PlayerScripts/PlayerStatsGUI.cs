using FlaxEngine;
using FlaxEngine.GUI;
using System;

namespace Game
{
    public class PlayerStatsGUI : Script
    {
        public PlayerStats PlayerStats { get; set; }
        public float HealthCriticPoint { get; set; } = 30;
        public UIControl HealthBar;
        private ProgressBar _healthBar;
        public UIControl HealthLabel;
        private Label _healthLabel;

        public override void OnStart()
        {
            if (HealthBar != null && HealthBar.Is<ProgressBar>())
                _healthBar = HealthBar.Get<ProgressBar>();
            else
                throw new Exception("HealthBar is not ProgressBar");

            if (HealthLabel != null && HealthLabel.Is<Label>())
                _healthLabel = HealthLabel.Get<Label>();
            else
                throw new Exception("HealthLabel is not ProgressBar");
        }

        public override void OnUpdate()
        {
            _healthLabel.Text = "HP: " + PlayerStats.Health + "%";

            _healthBar.Value = PlayerStats.Health;
            _healthBar.BarColor = PlayerStats.Health <= HealthCriticPoint ? Color.Red.AlphaMultiplied(0.5f) : Color.SpringGreen.AlphaMultiplied(0.5f);
        }
    }
}