create procedure dbo.sEventSuggestDelete
(
    @EventId int,
	@UserId int
)
as
begin
	delete from dbo.tEventSuggest where EventId = @EventId and UserId = @UserId
	return 0;
end;