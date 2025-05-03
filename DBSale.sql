create database DBSALE

go

use DBSALE

go

create table [Rol](
[idRol] int primary key identity(1,1),
[name] varchar(50),
[timestamp] datetime default getdate()
)

go

create table [Menu](
[idMenu] int primary key identity(1,1),
[name] varchar(50),
[icon] varchar(50),
[url] varchar(50)
)

go

create table [MenuRol](
[idMenuRol] int primary key identity(1,1),
[idMenu] int references Menu([idMenu]),
[idRol] int references Rol([idRol])
)

go


create table [User](
[idUser] int primary key identity(1,1),
[fullName] varchar(100),
[email] varchar(40),
[idRol] int references Rol([idRol]),
[password] varchar(40),
[isActive] bit default 1,
[timestamp] datetime default getdate()
)

go

create table [Category](
[idCategory] int primary key identity(1,1),
[name] varchar(50),
[isActive] bit default 1,
[timestamp] datetime default getdate()
)

go

create table [Product] (
[idProduct] int primary key identity(1,1),
[name] varchar(100),
[idCategory] int references Category([idCategory]),
[stock] int,
[price] decimal(10,2),
[isActive] bit default 1,
[timestamp] datetime default getdate()
)

go

create table [IDNumber](
[idIDNumber] int primary key identity(1,1),
[last_Number] int not null,
[timestamp] datetime default getdate()
)
go

create table [Sale](
[idSale] int primary key identity(1,1),
[IDNumber] varchar(40),
[paymentType] varchar(50),
[total] decimal(10,2),
[timestamp] datetime default getdate()
)
go


create table [SaleDetails](
[idSaleDetails] int primary key identity(1,1),
[idSale] int references Sale(idSale),
[idProduct] int references Product(idProduct),
[amount] int,
[price] decimal(10,2),
[total] decimal(10,2)
)

go


insert into [Rol]([name]) values
('Administrador'),
('Employee'),
('Supervisor')

go

insert into [User]([fullName],[email],[idRol],[password]) values 
('Test User','test@emilioFilotti#.com',1,'#test123#')

go

INSERT INTO [Category]([name],[isActive]) values
('Laptops',1),
('Monitores',1),
('Keyboard',1),
('Headset',1),
('Memories',1),
('Accesories',1)

go

insert into [Product]([name],[idCategory],[stock],[price],[isActive]) values
('laptop samsung book pro',1,20,2500,1),
('laptop lenovo idea pad',1,30,2200,1),
('laptop asus zenbook duo',1,30,2100,1),
('monitor teros gaming te-2',2,25,1050,1),
('monitor samsung curvo',2,15,1400,1),
('monitor huawei gamer',2,10,1350,1),
('keyboard seisen gamer',3,10,800,1),
('keyboard antryx gamer',3,10,1000,1),
('keyboard logitech',3,10,1000,1),
('headset logitech gamer',4,15,800,1),
('headset hyperx gamer',4,20,680,1),
('headset redragon rgb',4,25,950,1),
('memory kingston rgb',5,10,200,1),
('fan cooler master',6,20,200,1),
('mini fan lenono',6,15,200,1)

go

insert into [Menu]([name],[icon],[url]) values
('DashBoard','dashboard','/pages/dashboard'),
('User','group','/pages/user'),
('Product','collections_bookmark','/pages/product'),
('Sale','currency_exchange','/pages/Sale'),
('Historial Sales','edit_note','/pages/historial'),
('Report','receipt','/pages/report')

go

--menus para administrador
insert into [MenuRol]([idMenu],[idRol]) values
(1,1),
(2,1),
(3,1),
(4,1),
(5,1),
(6,1)

go

--menus para empleado
insert into [MenuRol]([idMenu],[idRol]) values
(4,2),
(5,2)

go

--menus para supervisor
insert into [MenuRol]([idMenu],[idRol]) values
(3,3),
(4,3),
(5,3),
(6,3)

go

insert into [IDNumber](last_Number,[timestamp]) values
(0,getdate())