create procedure dbo.sEventSuggestUpdate
(
    @EventId int,
	@UserId int,
	@DateSuggest date,
	@Descriptions nvarchar(256)
)
as
begin
	update dbo.tEventSuggest
	set		DateSuggest = @DateSuggest,
			Descriptions = @Descriptions
	where	EventId = @EventId and UserId = @UserId;
	return 0;
end;