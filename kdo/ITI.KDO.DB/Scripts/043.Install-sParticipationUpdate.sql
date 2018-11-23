create procedure dbo.sParticipationUpdate
(
	@QuantityId int,
	@UserId int,
	@EventId int,
	@AmountUserPrice int	
	
)
as
begin
	update dbo.tParticipation
	set
		AmountUserPrice = @AmountUserPrice
	where UserId = @UserId and QuantityId = @QuantityId;
	return 0;
end;