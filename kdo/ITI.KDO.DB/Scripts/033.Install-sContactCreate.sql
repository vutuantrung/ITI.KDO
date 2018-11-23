create procedure dbo.sContactCreate
(
    @FriendId        int,
	@UserId			 int,
	@Invitation 	 bit
)

as
begin
	insert into dbo.tContact(FriendId, UserId, Invitation)
	values(@FriendId, @UserId, @Invitation);
	return 0;
end