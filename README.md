# ContactManager
ASP.NET Core Web API that manages contacts

ASP.NET Core Web API project.
Entity Framework as ORM following the Code First approach.
SQL Server DataBase called contacts.

Exposes a RESTful endpoint providing standard CRUD functionality for Contacts.

The API use JWT bearer token as authorization.

DATA MODEL:

User
*	Id: GUID
*	Firstname: string (128 chars)
*	Lastname: string (128 chars)
*	Username: string (60 chars)
*	Password: string (256 chars)


Contact
*	Id: GUID
*	FirstName: string(128 chars)
*	LastName: string(128 chars) [not required]
*	Email: string(128 chars)
*	DateOfBirth: DateTime
*	Phone: string (20 chars)
*	Owner: GUID


ContactController.

*	POST
Url: /api/contacts
Description: Creates a new contact. Should follow the field specification described below 
Output: Should return a 400 Status Code if any validation fails and a 201 in case the contact has been created successfully together with a Location response header containing the newly created contact's URL.

*	GET
Url: /api/contacts
Description: Gets the entire list of contacts.
Output: (200 Status Code) contact list.

Url: /api/contacts/{id}
Description: Gets a contact from the collection.
Output: 200 Status Code with the contact record or a 404 Status Code if no contact matches with the provided Id. 

*	DELETE
Url: /api/contacts/{id}
Description: Remove a contact from the collection.
Output: Error if no contact matches with the provided Id otherwise a success response.

*	PUT
Url: /api/contacts/{id}
Description: Update a contact record.
Output: Error if no contact matches with the provided Id otherwise a success response. 

Fields
Field Name	Data Type	Required	Validation
FirstName	string	true	max length 128
LastName	string	false	max length 128
Email	string	true	must validate to a typical email address.
DateOfBirth	DateTime	true	must evaluate to a valid date
Phone	string	true	Not validation to avoid complexity

*	Every user must have a unique email address.
*	The contact when created must be 18 years or older at the time of the request.
*	When returning one contact or a list of contacts, return back the age of the contact in addition to the date of birth.
*	Any validation error must return the proper error response.
*	The endpoint DELETE /api/contact/{id} should be authorized only for Cuban Administrators.
*	Make sure to validate to return the HTTP status code for invalid URL calls. Ex: POST /api/contacts/{id} or DELETE /api/contacts.
