# G1ANT.Addon.SMS

Simple solution for intercept SMS from Android smartphone.
You smartphone should have installed G1Ant SMS Intercept application and working in this same network.

Example scenario:
You writing robot for system where MFA via SMS is required. After typing credential, you must confirm your identity via code from SMS.

Example usage in G1ANT Robot:

```
smsintercept.init hostname ‴192.168.1.100‴ tcpport 8080
smsintercept.readsms waittimeout 60 result ♥sms
dialog ♥sms
smsintercept.close

```
Explanation:
1. Bind listener for 8080 tcp port on 192.168.1.100  network interface
2. Wait for incoming SMS for max. 60 seconds
3. Show incoming SMS, if SMS is not received you will have empty result
4. Unbind listener

