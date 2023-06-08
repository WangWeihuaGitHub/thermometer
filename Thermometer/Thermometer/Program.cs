using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thermometer
{
    public class Program
    {
        public static void Main()
        {
            Thermometer thermometer = new Thermometer();
            thermometer.PropertyChanged += new Thermometer.ThermometerEventHandler(onTempChange); // Subscribe to the event

            var boiling = new Threshold()
            {
                isListenToRising = true,
                isListenToDropping = false,
                CelsiusThreshold = 100,
            };
            var freezing = new Threshold()
            {
                isListenToRising = false,
                isListenToDropping = true,
                CelsiusThreshold = 0,
            };
            thermometer.SetThreshold(boiling);
            thermometer.SetThreshold(freezing);

            //There will be only one event
            thermometer.Celsius = 90;
            thermometer.Celsius = 99.7;
            thermometer.Celsius = 99.9;

            Console.ReadKey();
        }

        // Event handler
        public static void onTempChange(Object sender, TemperatureChangeEventArgs e)
        {
            var threshold = e.threshold as Threshold;
            if (threshold != null)
            {
                Console.WriteLine("{0} to {1} Celsius.", e.eventName, threshold.CelsiusThreshold);
            }
        }
    }
}