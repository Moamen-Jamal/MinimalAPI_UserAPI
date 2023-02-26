# MinimalAPI_UserAPI
 Simple Minimal  APIs for registering and fetching a user from a database using  .NET 7 with Entity Framework Core 7
 
Notes:
1- The “id” should be a programmatically generated SHA1 hash of the email address, salted with the following “450d0b0db2bcf4adde5032eca1a7c416e560cf44” string. 
2- The “accessToken” should be a programmatically generated unique JWT Token.
3- The GET endpoint should use “id” to return the user.
4- The GET endpoint should omit the user “email” property if “marketingConsent” is false.
5- Save the user into a local database.
