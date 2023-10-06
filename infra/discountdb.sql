create table Coupon (
	Id Serial primary key,
	BookId int,
	BookName varchar(50),
	Description varchar(50),
	Amount int
)