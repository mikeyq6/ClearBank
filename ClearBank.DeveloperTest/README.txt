Changes:

* Changed AllowedPaymentSchemes to be a list to simplify the code, also so that there is only one version of the enum. If you ever needed to add a scheme, you would need to add it in both places otherwise
* Added interfaces for DataStores, then set the interface up as a property in the PaymentService class, ready for dependency injection
* Added other interfaces for Account and PaymentRequest
* Added unit tests, mocking the account, paymentRequest and dataStore
* Changed clause for whether status should be false or true, such that it presumes false until is set true. This will prevent false positives