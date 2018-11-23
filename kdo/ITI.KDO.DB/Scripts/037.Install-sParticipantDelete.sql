create procedure dbo.sParticipantDelete
(
    @UserId        int,
	@EventId	   int
)

as
begin
	delete from dbo.tParticipant where UserId = @UserId and EventId = @EventId;
	return 0;
end;