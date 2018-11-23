create procedure dbo.sParticipationCreate
(
	@QuantityId int,
	@UserId int,
	@EventId int,
	@AmountUserPrice int	
	
)
as
begin
	insert into dbo.tParticipation(QuantityId, UserId, EventId, AmountUserPrice)
	values(@QuantityId, @UserId, @EventId, @AmountUserPrice);
	return scope_identity();
end