create procedure dbo.sEventCreate
(
	@EventName nvarchar(32),
	@Descriptions nvarchar(200),
	@Dates date,
	@UserId int
)
as
begin
	insert into dbo.tEvent(EventName, Descriptions, Dates, UserId)
	values(@EventName, @Descriptions, @Dates, @UserId);
	return scope_identity();
end