using System;
using Xamarin.Forms;

namespace MatoIndustry.Control
{

    public class StepSlider : Slider
    {
        /// <summary>
        /// 步长
        /// </summary>
        public static readonly BindableProperty CurrentStepValueProperty = BindableProperty.Create(nameof(StepValue), typeof(double), typeof(StepSlider),  1.0);

        /// <summary>
        /// 获取或设置步长
        /// </summary>
        /// <value>The step value.</value>
        public double StepValue
        {
            get { return (double)GetValue(CurrentStepValueProperty); }

            set { SetValue(CurrentStepValueProperty, value); }
        }

        public StepSlider()
        {
            ValueChanged += OnSliderValueChanged;
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / StepValue);

            Value = newStep * StepValue;
        }
    }

}
