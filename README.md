1. This WEB API application is built in VS2019 and .Net core 5.0.
2. Web Application Project is - PhotoAlbumExp and test project is PhotoAlbumExp.Tests. Loosly coupled architecture is maintained and dependency is injected as and when it is required.
3. Web API application contains Service Controller, Business and Data layer. Data Model classes are stored under Model folder.
4. Error handling: appropriate try-catch block is implemented to catch exception.
5. Logging: Application is using Serilog to keep logging of information, warning, error. From each piece of code it's logging correct information. Logged file rolling interval is set as 1 day. Log file is saved into Log folder which is kept just one level up of solution directory.
6. Authentication : 
a. JWT(JSON Web Token) authentication is useed to authenticate. User credential is saved as hard coded value in JWTAuthenticationManager class.
b. Token encryption key is saved in startup.cs.
7. Data Access: In Data folder 2 end points (http://jsonplaceholder.typicode.com/photos, http://jsonplaceholder.typicode.com/albums) are accessed by HttpWebRequest. URL is saved in appsettings file.
8. Unit Testing : Unit test project is of type xUnit. Moq is used to mock different dependency or configure arrange part of unit testing. 
9. Production troubleshooting: In each level of code logging is present for audit trail purpose. To identify any error/ exception we can find the same in log file.
10. Run in postman: 
a. Authenticate : first call a post method with url - https://localhost:44362/PhotoAlbum/Authenticate providing Content type "application/json" and in body - {"username":"test1","password":"password1"}. Wrong user credential will provide Unauthorized message.  
b. Authenticate method will generate JWT encrypted token. Collect and copy the token.
c. Service Call: call a get method with URL: https://localhost:44362/PhotoAlbum/GetAlbumPhotos/-99 (for all album user id) by providing authorization attribute: Bearer [JWTToken]. Service will return json data set. To specify album user id it needs to pass correct albumUserID.(example - https://localhost:44362/PhotoAlbum/GetAlbumPhotos/2).
11. Proposed Improvement :
a. We can extend other data source by adding other data access functions in data access interface. From handler class we can inject the dependency of that data access function, based on requirement. 
b. For logging also we can save log information in other destination like database.
c. User credential for authentiaton should save into database as encrpted data. 
