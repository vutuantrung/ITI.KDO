create procedure dbo.sParticipationDelete
(
	@UserId int,
	@QuantityId int
)
as
begin
	delete from dbo.tParticipation where UserId = @UserId and QuantityId = @QuantityId;
	return 0;
end;