create view dbo.vContact
as
select
	ContactId = c.ContactId,
    UserId = c.UserId,
	FriendId = c.FriendId,
	Invitation = c.Invitation

	from dbo.tContact c