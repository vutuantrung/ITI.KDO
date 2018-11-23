create proc dbo.sQuantityCreate
(
	@Quantity int,
	@RecipientId int,
	@NominatorId int,
	@EventId int,
	@PresentId int
)
as
begin
	insert into dbo.tQuantity(Quantity, RecipientId, NominatorId, EventId, PresentId)
	values(@Quantity, @RecipientId, @NominatorId, @EventId, @PresentId);
	return scope_identity();
end