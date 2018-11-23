create view dbo.vUserFriends
as
select
	UserId = c.UserId,
	FriendId = f.UserId,
	Firstname = f.FirstName,
	LastName = f.LastName,
	Email = f.Email,
	Birthdate = f.Birthdate,
	Phone = f.Phone,
	Photo = f.Photo,
	Invitation = c.Invitation
from dbo.tContact c
	inner join dbo.tUser f on c.FriendId = f.UserId
union all
select
	UserId = c.FriendId,
	FriendId = f.UserId,
	Firstname = f.FirstName,
	LastName = f.LastName,
	Email = f.Email,
	Birthdate = f.Birthdate,
	Phone = f.Phone,
	Photo = f.Photo,
	Invitation = c.Invitation
from dbo.tContact c
	inner join dbo.tUser f on c.UserId = f.UserId