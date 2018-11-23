create proc dbo.sQuantityUpdate
(
	@QuantityId int,
	@Quantity int,
	@RecipientId int,
	@NominatorId int,
	@EventId int,
	@PresentId int
)
as
begin
	update dbo.tQuantity
	set
		Quantity = @Quantity,
		RecipientId = @RecipientId,
		EventId = @EventId,
		PresentId = @PresentId
	where NominatorId = @NominatorId and QuantityId = @QuantityId;
	return 0;
end;