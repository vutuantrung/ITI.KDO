create proc dbo.sUserUpdatePhoto
(
    @UserId int,
	@Photo varbinary(max)
)
as
begin
	update dbo.tUser
	set	Photo = @Photo
	where UserId = @UserId;
	return 0;
end;