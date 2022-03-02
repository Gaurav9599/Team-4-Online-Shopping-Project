use shoppingDB1
go

create table CustomerDetails( 
CustId int identity(1,1) primary key,
CustName nvarchar(100) not null,
email nvarchar(100) not null, 
PhoneNum nvarchar(100) not null,
password nvarchar(100) not null,
)

alter table CustomerDetails 
add constraint email check(email like '%_@_%._%')


create table ShippingAddress(
AddressId int identity(1,1) primary key not null,
city nvarchar(100) not null,
village nvarchar(100),
landmark nvarchar(100),
pincode int not null,
userId int foreign key references CustomerDetails(CustId) on delete cascade,
)

create table retailer(
retailerId int identity(1,1) primary key  not null, 
retailerName nvarchar(100) not null,
retailerEmail nvarchar(200) not null, 
retailerPhoneNum  nvarchar(100),
country nvarchar(100),
state nvarchar(100),
city nvarchar(100) not null,
pincode int not null ,
password nvarchar(100) not null,
productType nvarchar(100) not null,
approvedStatus bit default 0,
)

alter table retailer 
add constraint retailerEmail check(retailerEmail like '%_@_%._%')


create table productDetails ( 
productId int identity(1,1) primary key not null, 
productCategory nvarchar(100),
productBrand nvarchar(100),
productName nvarchar(100),
fabric nvarchar(100),
productPrice money,
productQuantity int,
ratings float,
images nvarchar(200), 
retailerId int foreign key references retailer(retailerId) on delete cascade,
)

create table Cart(
CartId int identity(1,1) primary key not null,
customerId int foreign key references CustomerDetails(custId) on delete cascade,
productId int foreign key references productDetails(productId) on delete cascade,
quantity int not null,
TotalPrice int,
)

create table CustomerOrder(
custOrderId int identity(1,1) primary key,
productId int foreign key references productDetails(productId) on delete cascade,
customerId int foreign key references CustomerDetails(custId) on delete cascade,
purchasingQuantity int,
TotalPrice int,
orderdate Date not null default getdate(),
)

create table Orders(
orderId int identity(1,1) primary key not null,
TotalPrice int,
custOrderId int foreign key references CustomerOrder(custOrderId) on delete cascade,
)



create table Admin(
AdminId int identity(1,1) primary key not null,
AdminName nvarchar(100) not null,
AdminEmail nvarchar(100) not null, 
AdminPhoneNum nvarchar(200) not null,
Adminpassword nvarchar(100) not null,
)

alter table Admin 
add constraint AdminEmail check(AdminEmail like '%_@_%._%')
