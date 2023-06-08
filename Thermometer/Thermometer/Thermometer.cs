using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermometer
{
    public class Threshold
    {
        public bool isListenToRising;
        public bool isListenToDropping;

        protected double _celsiusThreshold;
        public double CelsiusThreshold
        {
            get
            {
                return _celsiusThreshold;
            }
            set
            {
                _celsiusThreshold = value;
                _fahrenheitThreshold = (value * 1.8) + 32;
            }
        }

        protected double _fahrenheitThreshold;
        public double FahrenheitThreshold
        {
            get
            {
                return _fahrenheitThreshold;
            }
            set
            {
                _fahrenheitThreshold = value;
                _celsiusThreshold = (value - 32) / 1.8;
            }
        }
    }

    public class TemperatureChangeEventArgs : EventArgs
    {
        public string eventName { get; internal set; }
        public object threshold { get; internal set; }

        public TemperatureChangeEventArgs(string eventName, object threshold)
        {
            this.eventName = eventName;
            this.threshold = threshold;
        }
    }

    public class Thermometer
    {
        protected double _previousCelsius;
        protected IList<Threshold> Thresholds;

        public Thermometer()
        {
            Thresholds = new List<Threshold>();
        }

        public void SetThreshold(Threshold threshold)
        {
            if (threshold != null)
            {
                Thresholds.Add(threshold);
            }
        }

        public delegate void ThermometerEventHandler(object sender, TemperatureChangeEventArgs data);
        public event ThermometerEventHandler PropertyChanged; // Associate the event with the delegate

        // Raise the event
        protected void ChangeTemperature(double celsiusTemperature)
        {
            foreach (var threshold in Thresholds)
            {
                if ((PropertyChanged != null) &&
                    (threshold.isListenToDropping == true) &&
                    (celsiusTemperature - threshold.CelsiusThreshold <= 0.5) &&
                    (_previousCelsius - threshold.CelsiusThreshold > 0.5))
                {
                    TemperatureChangeEventArgs e = new TemperatureChangeEventArgs("Temperature is dropping", threshold);
                    PropertyChanged(this, e);
                }

                if ((PropertyChanged != null) &&
                    (threshold.isListenToRising == true) &&
                    (threshold.CelsiusThreshold - celsiusTemperature <= 0.5) &&
                    (threshold.CelsiusThreshold - _previousCelsius > 0.5))
                {
                    TemperatureChangeEventArgs e = new TemperatureChangeEventArgs("Temperature is rising", threshold);
                    PropertyChanged(this, e);
                }
            }
        }

        protected double _celsiusValue;
        public double Celsius
        {
            get
            {
                return _celsiusValue;
            }
            set
            {
                _previousCelsius = this._celsiusValue;
                _celsiusValue = value;
                _fahrenheitValue = (value * 1.8) + 32;
                ChangeTemperature(_celsiusValue);
            }
        }

        protected double _fahrenheitValue;
        public double Fahrenheit
        {
            get
            {
                return _fahrenheitValue;
            }
            set
            {
                _previousCelsius = this._celsiusValue;
                _fahrenheitValue = value;
                _celsiusValue = (value - 32) / 1.8;
                ChangeTemperature(_celsiusValue);
            }
        }
    }
}
