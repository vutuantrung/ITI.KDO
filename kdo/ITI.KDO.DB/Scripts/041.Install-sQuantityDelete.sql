create procedure dbo.sQuantityDelete
(
	@QuantityId int
)
as
begin
	delete from dbo.tQuantity where QuantityId = @QuantityId;
	return 0;
end;