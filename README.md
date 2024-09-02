# ASP.NET MVC Ecommerce Online Shop
## Overview
#### This Online Shop  Management System is a robust and user-friendly web application developed using ASP.NET MVC 5 and Entity Framework 6, leveraging the Database First approach. 
The system provides a seamless interface for managing Ecommerce (mobile and Laptop) -related data, including Order, User, Brands, Products, Products Category, and ProductsSuppliers, Discount , Unit. 
It also incorporates image handling capabilities for user profiles and other relevant records.

## Features
### Online shop  Management: Create, update, delete, and view Product. Assign Products to Admin and manage Shop details efficiently.
Department Management: Manage departments, assign instructors as department heads, and link courses to departments.
Admin : Add, update, delete, and view Product profiles.
User :When a user visits this site to purchase a product, they must log in first. After logging in, they can add any product to the cart and then complete the order
Image Handling: Upload and manage images for Products to enhance user profiles and other records.
Sorting and Filtering: Implement sorting and filtering functionalities across various tables for improved data navigation.
Validation and Error Handling: Comprehensive validation and error handling to ensure data integrity and a smooth user experience.
Technologies Used
ASP.NET MVC 5: For creating a scalable and maintainable web application.
Entity Framework 6: For data access and management using the Database First approach.
Bootstrap: For responsive and modern UI design.
jQuery and DataTables: For enhanced user interactions and data handling.
SQL Server: As the database solution to store and manage application data.

## System Overview in Images
## Below are some screenshots of the system:
# Login
![Screenshot 2024-09-03 030754](https://github.com/user-attachments/assets/0e2b6f7a-263d-4d9c-ac0c-cd54e384445c)
![Screenshot 2024-09-03 030932](https://github.com/user-attachments/assets/e22ff352-cffa-4d34-8c6e-d952a9f6e0e2)
![Screenshot 2024-09-03 031022](https://github.com/user-attachments/assets/54aa073c-5cb0-42d0-867f-bdcd2d6e9ecf)
![Screenshot 2024-09-03 031100](https://github.com/user-attachments/assets/f9b150c9-98d6-4018-830b-0a46a906dd63)
![Screenshot 2024-09-03 031147](https://github.com/user-attachments/assets/4748f91b-29d4-4b45-8415-9a9531dfbcec)
![Screenshot 2024-09-03 031253](https://github.com/user-attachments/assets/94bbf63b-d68f-4946-88be-03e3e7f9e21b)
![Screenshot 2024-09-03 031321](https://github.com/user-attachments/assets/949eca9d-5d90-4bfa-b10f-234feade572d)
![Screenshot 2024-09-03 031354](https://github.com/user-attachments/assets/9bcc620c-57f7-4f76-83f2-94dd69201afe)
![Screenshot 2024-09-03 031552](https://github.com/user-attachments/assets/041fcc67-4e57-4cff-bf6b-6862dbff181c)
![Screenshot 2024-09-03 031437](https://github.com/user-attachments/assets/819cc100-4b7f-4e8f-8fb9-ac87deb13c79)

# Master-Detail CRUD Application
Using Entity Framework Code First for managing  Brands, Products, and Products Suppliers with image support.

### How to Use This Application
## Initial Setup:
Open Microsoft SQL Server Management Studio.
Copy the server name.
Navigate to the root folder of the Asp.Net-MVC-Ecommerce application.
Open Web.config.
Update the data source with your server name (other settings remain unchanged).
Database Setup:
Open Visual Studio.
Navigate to Tools -> NuGet Package Manager -> Package Manager Console.
Run the command:
<b>update-database</b>
The database will be created, and data will be seeded.
If update-database does not work, delete the Migrations folder and run the following commands:
Enable-Migrations
Add-Migration initData
Update-Database
If issues persist, ensure the database catalog name in Web.config is correct.
Note: The update-database command should work fine Insha Allah.

Rebuild the project
Run the Application:
Open HomeController and run the project.

