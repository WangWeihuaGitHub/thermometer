# thermometer

The design of a thermometer that can read the temperature of some external source.

The thermometer is able to provide temperature in both Fahrenheit and Celsius. Callers of the thermometer system can define arbitrary thresholds 
such as freezing and boiling at which the thermometer will infom the callers that a specific threshold has been reached. 
Note: Callers of the thermometer may not want to be repeatedly informed that a given threshold has been reached if the temperature is fluctuating 
around the threshold point. For example, consider the following temperature readings from the external source:

1.5 C

1.0 C

0.5 C

0.0 C

-0.5 C

0.0 C

-0.5 C

0.0 C

0.5 C

0.0 C

Some callers may only want to be informed that the temperature has reached 0° C once because they consider fluctuations of +/- 0.5° C insignificant.

It may also be important for some callers to be informed that a threshold has been reached only if the threshold was reached from a certain direction. 
For example, some callers may only care about a freezing point threshold if the previous temperature was above freezing 
(i.e. they only care about the threshold if it occurred while the temperature was dropping).
