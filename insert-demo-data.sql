use master
USE EcommerceDB
GO

BEGIN TRY
    INSERT INTO Brands (BrandName)
    VALUES ('Hp'),
           ('ASUS'),
           ('Apple'),
		   ('Dell'),
		   ('Oppo'),
		   ('Vivo'),
		   ('Samsung');

    PRINT 'Brands inserted successfully.';
END TRY
BEGIN CATCH
    PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    INSERT INTO Units (UnitName)
    VALUES 
	('Piece');
	
    IF @@ROWCOUNT > 0
    BEGIN
        PRINT 'Unit inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'No rows inserted.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error inserting unit: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    INSERT INTO ProductCategories (CategoryName, CategoryImage)
    VALUES 
	('Laptop', '~/Images/product-category/Laptop Brands.jpg'),
	('Mobile', '~/Images/product-category/All moblie_20240627020727572.jpeg');
	

    -- Check if any rows were affected (optional)
    IF @@ROWCOUNT > 0
    BEGIN
        PRINT 'Product category inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'No rows inserted.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error inserting product category: ' + ERROR_MESSAGE();
END CATCH


BEGIN TRY
    INSERT INTO ProductSubCategories (SubCategoryName, SubCategoryImg, ProductCategoryId)
    VALUES
	('Hp', '~/Images/product-subcategory/HpCatagory.jpg', 1),
	('Asus', '~/Images/product-subcategory/AsusCatagory.jpg', 1),
	('Apple', '~/Images/product-subcategory/AppleCatagory.jpg', 1),
	('Dell', '~/Images/product-subcategory/DellCatagory.jpg', 1),
	('Oppo', '~/Images/product-subcategory/Oppo_20240627020754088.jpeg', 2),
	('Vivo', '~/Images/product-subcategory/Samsung_20240627015932454.jpeg', 2),
	('Samsung', '~/Images/product-subcategory/Oppo_20240627020754088.jpeg', 2);
	
    IF @@ROWCOUNT > 0
    BEGIN
        PRINT 'Product subcategory inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'No rows inserted.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error inserting product subcategory: ' + ERROR_MESSAGE();
END CATCH


BEGIN TRY
    INSERT INTO ProductSuppliers (SupplierName, ContactEmail, ContactPhone, Address)
    VALUES ('Mredul Suppliers', 'abc@example.com', '123-456-7890', '123 Main Street'),
	('Jamuna Suppliers', 'jamuna@example.com', '123-456-7890', '123 Farmgate, Dhaka'),
	('Keyamy Suppliers', 'kem@example.com', '123-456-7890', '225 Agargoun, Dhaka');

    IF @@ROWCOUNT > 0
    BEGIN
        PRINT 'Product supplier inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'No rows inserted.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error inserting product supplier: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    INSERT INTO Discounts (Name, Percentage)
    VALUES 
	('Eid', 25.0),
	('Year End Sale', 15.0);
	
    IF @@ROWCOUNT > 0
    BEGIN
        PRINT 'Discount inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'No rows inserted.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error inserting discount: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    -- Laptop Category
    INSERT INTO Products(ProductName, Price, Description, ImageUrl, IsActive, ProductCategoryId, ProductSubCategoryId, UnitId, BrandId, ProductSupplierId, DiscountId, Quantity)
    VALUES 
    ('Hp g7', 60000.00, 'Corei-7 fresh condition.', '~/Images/products/hp2.jpg', 1, 1, 1, 1, 1, 1, 1, 100),
    ('Asus G6', 65000.00, 'Fresh new condition.', '~/Images/products/Asus.jpg', 1, 1, 2, 1, 2, 2, 1, 200),
    ('Macbook7', 150050.00, 'Apple Mackbook.', '~/Images/products/apple.jpg', 1, 1, 3, 1, 3, 3, 1, 150),
    ('Dell g7', 60.00, 'Dell new laptop.', '~/Images/products/Dell.jpg', 1, 1, 4, 1, 4, 1, 2, 120);
  
    PRINT 'Vegetables products inserted successfully.';
END TRY
BEGIN CATCH
    PRINT 'Error inserting vegetables products: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    -- Mobile Category
    INSERT INTO Products(ProductName, Price, Description, ImageUrl, IsActive, ProductCategoryId, ProductSubCategoryId, UnitId, BrandId, ProductSupplierId, DiscountId, Quantity)
    VALUES 
    ('Oppo', 40500.00, 'Oppo A53.', '~/Images/products/oppo.jpg', 1, 2, 5, 1, 5, 1, 2, 50),
    ('Vivo', 40400.00, 'Vivo Max pro.', '~/Images/products/vivo.jpg', 1, 2, 6, 1, 5, 2, 2, 60),
    ('Samsung', 40300.00, 'Samsung ultra.', '~/Images/products/samsung3.jpg', 1, 2, 7, 1, 7, 2, 2, 70);

    PRINT 'Fish products inserted successfully.';
END TRY
BEGIN CATCH
    PRINT 'Error inserting fish products: ' + ERROR_MESSAGE();
END CATCH



