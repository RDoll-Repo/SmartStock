-- --------------------------------------------------------------------------------
-- Name: SmartStock DataBase
-- Class: Capstone
-- Abstract: The database for smartstack
-- --------------------------------------------------------------------------------

-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE sqlDB1;     -- Get out of the master database
SET NOCOUNT ON; -- Report only errors

-- --------------------------------------------------------------------------------
--	Step #1 : Drop VIEWS
-- --------------------------------------------------------------------------------
IF OBJECT_ID ('Inventory_View')			IS NOT NULL DROP VIEW Inventory_View
IF OBJECT_ID ('User_View')				IS NOT NULL DROP VIEW User_View
IF OBJECT_ID ('Supplier_View')			IS NOT NULL DROP VIEW Supplier_View
-- --------------------------------------------------------------------------------
--	Step #1 : Drop tables
-- --------------------------------------------------------------------------------
IF OBJECT_ID ('TOrderProducts')			IS NOT NULL DROP TABLE TOrderProducts
IF OBJECT_ID ('TOrder')					IS NOT NULL DROP TABLE TOrder
IF OBJECT_ID ('TSupplier')				IS NOT NULL DROP TABLE TSupplier
IF OBJECT_ID ('TInventoryAdjustment')	IS NOT NULL DROP TABLE TInventoryAdjustment
IF OBJECT_ID ('TInventory')				IS NOT NULL DROP TABLE TInventory
IF OBJECT_ID ('TProduct')				IS NOT NULL DROP TABLE TProduct
IF OBJECT_ID ('TUser')					IS NOT NULL DROP TABLE TUser

IF OBJECT_ID ('TAdjustment')			IS NOT NULL DROP TABLE TAdjustment
IF OBJECT_ID ('TStatus')				IS NOT NULL DROP TABLE TStatus
IF OBJECT_ID ('TRole')					IS NOT NULL DROP TABLE TRole
IF OBJECT_ID ('TPayment')				IS NOT NULL DROP TABLE TPayment
IF OBJECT_ID ('TCategory')				IS NOT NULL DROP TABLE TCategory
IF OBJECT_ID ('TProductLocation')		IS NOT NULL DROP TABLE TProductLocation
IF OBJECT_ID ('TState')					IS NOT NULL DROP TABLE TState

-- --------------------------------------------------------------------------------
--	Step #1.1 : Create tables
-- --------------------------------------------------------------------------------

CREATE TABLE TState
(
	 intStateID					INTEGER			NOT NULL
	,strStateName				VARCHAR(50)		NOT NULL
	,CONSTRAINT TState_PK PRIMARY KEY (intStateID)
)

CREATE TABLE TRole
(
	 intRoleID					INTEGER			NOT NULL
	,strRoleName				VARCHAR(50)		NOT NULL
	,strRoleDesc				VARCHAR(255)	NOT NULL
	,CONSTRAINT TRole_PK PRIMARY KEY (intRoleID)
)

CREATE TABLE TUser
(
	 intUserID					INTEGER			NOT NULL
	,strFirstName				VARCHAR(50)		NOT NULL
	,strLastName				VARCHAR(50)		NOT NULL
	,strPhoneNumber				VARCHAR(50)		NOT NULL
	,strEmail					VARCHAR(255)	NOT NULL
	,strAddress1				VARCHAR(50)		NOT NULL
	,strAddress2				VARCHAR(50)		NOT NULL
	,strZip						VARCHAR(50)		NOT NULL
	,strUserName				VARCHAR(50)		NOT NULL
	,userPassword				VARCHAR(50)		NOT NULL
	,intStateID					INTEGER			NOT NULL
	,intRoleID					INTEGER			NOT NULL
	,CONSTRAINT TUser_PK PRIMARY KEY (intUserID)
)

CREATE TABLE TInventoryAdjustment
(
	 intInventoryAdjustmentID	INTEGER			NOT NULL
	,intInventoryID				INTEGER			NOT NULL
	,intAdjustmentID			INTEGER			NOT NULL
	,intUserID					INTEGER			NOT NULL
	,intProductID				INTEGER			NOT NULL
	,CONSTRAINT TInventoryAdjustment_PK PRIMARY KEY (intInventoryAdjustmentID)
)

CREATE TABLE TAdjustment
(
	 intAdjustmentID			INTEGER			NOT NULL
	,strAdjustmentType			VARCHAR(50)		NOT NULL
	,strAdjustmentDesc			VARCHAR(255)	NOT NULL
	,CONSTRAINT TAdjustment_PK PRIMARY KEY (intAdjustmentID)
)

CREATE TABLE TInventory
(
	 intInventoryID				INTEGER			NOT NULL
	,intProductID				INTEGER			NOT NULL
	,intStatusID				INTEGER			NOT NULL
	,intCurrentInventory		INTEGER			NOT NULL
	,CONSTRAINT TInventory_PK PRIMARY KEY (intInventoryID)
)

CREATE TABLE TStatus
(
	 intStatusID				INTEGER			NOT NULL
	,strStatusType				VARCHAR(50)		NOT NULL
	,CONSTRAINT TStatus_PK PRIMARY KEY (intStatusID)
)

CREATE TABLE TCategory
(
	 intCategoryID				INTEGER			NOT NULL
	,strCategory				VARCHAR(50)		NOT NULL
	,CONSTRAINT TCategory_PK PRIMARY KEY (intCategoryID)
)

CREATE TABLE TProductLocation
(
	 intProductLocationID		INTEGER			NOT NULL
	,strLocation				VARCHAR(50)		NOT NULL
	,CONSTRAINT TProductLocation_PK PRIMARY KEY (intProductLocationID)
)

CREATE TABLE TProduct
(
	 intProductID				INTEGER			NOT NULL
	,strProductName				VARCHAR(50)		NOT NULL
	,strProductDesc				VARCHAR(255)	NOT NULL
	,intUnitSize				INTEGER			NOT NULL
	,intUnitType				INTEGER			NOT NULL
	,monUnitPrice				MONEY			NOT NULL
	,intCategoryID				INTEGER			NOT NULL
	,intProductLocationID		INTEGER			NOT NULL
	,CONSTRAINT TProduct_PK PRIMARY KEY (intProductID)
)

CREATE TABLE TOrderProducts
(
	 intOrderProductsID			INTEGER			NOT NULL
	,intProductQuantity			INTEGER			NOT NULL
	,intOrderID					INTEGER			NOT NULL
	,intProductID				INTEGER			NOT NULL
	,CONSTRAINT TOrderProducts_PK PRIMARY KEY (intOrderProductsID)
)

CREATE TABLE TPayment
(
	 intPaymentID				INTEGER			NOT NULL
	,strPaymentType				VARCHAR(50)		NOT NULL
	,CONSTRAINT TPayment_PK PRIMARY KEY (intPaymentID)
)

CREATE TABLE TOrder
(
	 intOrderID					INTEGER			NOT NULL
	,dtmOrderDate				DATETIME		NOT NULL
	,intUserID					INTEGER			NOT NULL
	,intPaymentID				INTEGER			NOT NULL
	,intSupplierID				INTEGER			NOT NULL
	,CONSTRAINT TOrder_PK PRIMARY KEY (intOrderID)
)

CREATE TABLE TSupplier
(
	 intSupplierID				INTEGER			NOT NULL
	,strCompanyName				VARCHAR(50)	NOT NULL
	,strContactFirstName		VARCHAR(50)		NOT NULL
	,strContactLastName			VARCHAR(50)		NOT NULL
	,strEmail					VARCHAR(50)		NOT NULL
	,strAddress1				VARCHAR(50)		NOT NULL
	,strAddress2				VARCHAR(50)		NOT NULL
	,strZip						VARCHAR(50)		NOT NULL
	,strPhoneNumber				VARCHAR(50)		NOT NULL
	,strURL						VARCHAR(50)		NOT NULL
	,strNotes					VARCHAR(255)	NOT NULL
	,intStateID					INTEGER			NOT NULL
	,CONSTRAINT TSupplier_PK PRIMARY KEY (intSupplierID)
)

-- --------------------------------------------------------------------------------
-- Step #1.2: Identify and Create Foreign Keys
-- --------------------------------------------------------------------------------
-- -    -----					------					---------
-- #	Child					Parent					Column(s)
-- -	-----					------					---------
-- 1	TUser					TState					intStateID
-- 2	TUser					TRole					intRoleID
-- 3	TInventoryAdjustment	TInventory				intInventoryID
-- 4	TInventoryAdjustment	TUsers					intUserID
-- 5	TInventoryAdjustment	TProduct				intProductID
-- 6	TInventoryAdjustment	TAdjustment				intAdjustmentID
-- 7	TInventory				TProduct				intProductID
-- 8	TInventory				TStatus					intStatusID
-- 9	TProduct				TProductLocation		intProductLocationID
-- 10	TProduct				TCategory				intCategoryID
-- 11	TOrderProducts			TProduct				intProductID
-- 12	TOrderProducts			TOrder					intOrderID
-- 14	TOrder					TSupplier				intSupplierID
-- 15	TOrder					TPayment				intPaymentID
-- 16	TOrder					TUser					intUserID
-- 17	TSuppliers				TState					intStateID


-- 1
ALTER TABLE TUser ADD CONSTRAINT TUser_TState_FK
FOREIGN KEY ( intStateID ) REFERENCES TState ( intStateID )

-- 2
ALTER TABLE TUser ADD CONSTRAINT TUser_TRole_FK
FOREIGN KEY ( intRoleID ) REFERENCES TRole ( intRoleID )

-- 3
ALTER TABLE TInventoryAdjustment ADD CONSTRAINT TInventoryAdjustment_TInventory_FK
FOREIGN KEY ( intInventoryID ) REFERENCES TInventory ( intInventoryID )

-- 4
ALTER TABLE TInventoryAdjustment ADD CONSTRAINT TInventoryAdjustment_TUser_FK
FOREIGN KEY ( intUserID ) REFERENCES TUser ( intUserID )

-- 5
ALTER TABLE TInventoryAdjustment ADD CONSTRAINT TInventoryAdjustment_TProduct_FK
FOREIGN KEY ( intProductID ) REFERENCES TProduct ( intProductID )

-- 6
ALTER TABLE TInventoryAdjustment ADD CONSTRAINT TInventoryAdjustment_TAdjustment_FK
FOREIGN KEY ( intAdjustmentID ) REFERENCES TAdjustment ( intAdjustmentID )

-- 7
ALTER TABLE TInventory ADD CONSTRAINT TInventory_TProduct_FK
FOREIGN KEY ( intProductID ) REFERENCES TProduct ( intProductID )

-- 8
ALTER TABLE TInventory ADD CONSTRAINT TInventory_TStatus_FK
FOREIGN KEY ( intStatusID ) REFERENCES TStatus ( intStatusID )

-- 9
ALTER TABLE TProduct ADD CONSTRAINT TProduct_TProductLocation_FK
FOREIGN KEY (intProductLocationID) REFERENCES TProductLocation (intProductLocationID)

-- 10
ALTER TABLE TProduct ADD CONSTRAINT TProduct_TCategory_FK
FOREIGN KEY (intCategoryID) REFERENCES TCategory (intCategoryID)

-- 11
ALTER TABLE TOrderProducts ADD CONSTRAINT TOrderProducts_TProduct_FK
FOREIGN KEY (intProductID) REFERENCES TProduct (intProductID)

-- 12
ALTER TABLE TOrderProducts ADD CONSTRAINT TOrderProducts_TOrder_FK
FOREIGN KEY (intOrderID) REFERENCES TOrder (intOrderID)

-- 13
ALTER TABLE TOrder ADD CONSTRAINT TOrder_TSupplier_FK
FOREIGN KEY (intSupplierID) REFERENCES TSupplier (intSupplierID)

-- 14
ALTER TABLE TOrder ADD CONSTRAINT TOrder_TPayment_FK
FOREIGN KEY (intPaymentID) REFERENCES TPayment (intPaymentID)

-- 15
ALTER TABLE TOrder ADD CONSTRAINT TOrder_TUser_FK
FOREIGN KEY (intUserID) REFERENCES TUser (intUserID)

-- 16
ALTER TABLE TSupplier ADD CONSTRAINT TSupplier_TState_FK
FOREIGN KEY (intStateID) REFERENCES TState (intStateID)

-- --------------------------------------------------------------------------------
--	Step #2 : Add Sample Data - INSERTS
-- --------------------------------------------------------------------------------
/*INSERT INTO TState (intStateID, strStateName)
VALUES	 (1, 'Ohio')
		,(2, 'Kentucky')
		,(3, 'Indiana')

INSERT INTO TRole(intRoleID, strRoleName, strRoleDesc)
VALUES	 (1, 'Owner', 'owns the buisness and has access to everything')
		,(2, 'Manager', 'manages day to day operations')
		,(3, 'Server', 'is a server')


INSERT INTO TUser(intUserID, intRoleID, strFirstName, strLastName, strPhoneNumber, strEmail, strAddress1, strAddress2, intStateID, strZip, strUserName, userPassword)
VALUES	 (1, 1, 'John', 'Dohn', '1234234234', 'johndohn@something.com', '3rd st', '', 1, '34567', 'john', '******')
		,(2, 2, 'Sarah', 'Miller', '3453453455', 'sarahsomething@something.com', '4th st', '', 2, '56787', 'sarah', '*****')
		,(3, 3, 'Jane', 'Frankfort', '1231233245', 'janejane@something.com', '5th st', '', 3, '45647', 'jane', '*******')

INSERT INTO TInventoryAdjustment(intInventoryAdjustmentID, intAdjustmentID, intInventoryID, intProductID, intUserID)
VALUES	 (1, 1, 1, 2, 2)
		,(2, 2, 1, 4, 1)


INSERT INTO TAdjustment(intAdjustmentID, strAdjustmentType, strAdjustmentDesc)
VALUES	 (1, 'Add New', 'add a new item to the inventory')
		,(2, 'Adjust', 'adjust the existing inventory')

INSERT INTO TInventory(intInventoryID, intProductID, intStatusID, intCurrentInventory)
VALUES	 (1, 2, 2, 16)
		,(2, 3, 3, 31)
		,(3, 4, 3, 31)
		,(4, 5, 1, 43)
		,(5, 6, 2, 5)
		,(6, 1, 1, 65)

INSERT INTO TStatus(intStatusID, strStatusType)
VALUES	 (1, 'High')
		,(2, 'Low')
		,(3, 'Good')

INSERT INTO TCategory(intCategoryID, strCategory)
VALUES	 (1, 'Vegetable')
		,(2, 'Red-meat')
		,(3, 'Poultry')
		,(4, 'Fish')
		,(5, 'Seasoning')
		,(6, 'Bread')
		,(7, 'Dairy Products')
		,(8, 'Other')

INSERT INTO TProductLocation(intProductLocationID, strLocation)
VALUES	 (1, 'dry storage')
		,(2, 'freezer')
		,(3, 'fridge')

INSERT INTO TProduct(intProductID, strProductName,intCategoryID,intProductLocationID,intUnitSize,intUnitType,monUnitPrice)
VALUES	 (1, 'Tomato',1,3,10,4,0.60)
		,(2, 'Chicken Breast',3,2,8,4,1.00)
		,(3, 'Wings',3,2,24,2,0.75)
		,(4, 'Steak',2,3,10,4,8.00)
		,(5, 'Milk',7,3,1,6,1.20)

INSERT INTO TOrderProducts(intOrderProductsID, intOrderID, intProductID, intProductQuantity)
VALUES	 (1, 1, 4)


INSERT INTO TPayment(intPaymentID, strPaymentType)
VALUES	 (1, 'debit')
		,(2, 'credit')
		,(3, 'cash')

INSERT INTO TOrder(intOrderID, dtmOrderDate, intUserID, intPaymentID, intSupplierID)
VALUES	 (1, '2/2/22', 2,2,1)


INSERT INTO TSupplier(intSupplierID, strCompanyName,strContactFirstName,strContactLastName,strEmail,strPhoneNumber,strAddress1,strAddress2,strZip,intStateID,strURL,strNotes)
VALUES	 (1, 'Sysco','Bill','Thomson','Billy@something.com','908098098','6th st','','98798',3,'someURL','a solid food purveyor')*/

-- --------------------------------------------------------------------------------
--	Step #4: VIEWS
-- --------------------------------------------------------------------------------
 --Inventory view
GO
CREATE VIEW Inventory_View AS
SELECT TI.intInventoryID as InventoryID, TP.strProductName as Product, TSS.strStatusType as Status, TPL.strLocation as StoredLocation, TP.intUnitSize as UnitSize, TP.intUnitType as CaseSize, TP.strProductDesc as Description
FROM TInventory as TI
	JOIN TProduct as TP
	ON TP.intProductID = TI.intProductID
	JOIN TStatus as TSS
	ON TSS.intStatusID = TI.intStatusID
	JOIN TCategory as TC
	ON TC.intCategoryID = TP.intCategoryID
	JOIN TProductLocation as TPL
	ON TPL.intProductLocationID = TP.intProductLocationID;

-- User view
GO
CREATE VIEW User_View AS
SELECT TU.intUserID as UserID, TR.strRoleName as Role,TU.strUserName as Username, TU.strFirstName as FirstName, TU.strLastName as LastName, TU.strEmail as Email, TU.strPhoneNumber as Phone, TU.strAddress1 as Address1, TU.strAddress2 as Address2, TState.strStateName as State, TU.strZip as Zip
FROM TUser as TU
	JOIN TRole as TR
	ON TR.intRoleID = TU.intRoleID
	JOIN TState 
	ON TState.intStateID = TU.intStateID;

-- Supplier view
GO
CREATE VIEW Supplier_View AS
SELECT TSP.intSupplierID as SupplierID, TSP.strCompanyName as CompanyName, TSP.strContactFirstName as FirstName, TSP.strContactLastName as LastName, TSP.strPhoneNumber as Phone, TSP.strEmail as Email, TSP.strURL as URL, TSP.strAddress1 as Address1, TSP.strAddress2 as Address2, TState.strStateName as State, TSP.strZip as Zip, TSP.strNotes as Notes
FROM TSupplier as TSP
	JOIN TState
	ON TState.intStateID = TSP.intStateID;