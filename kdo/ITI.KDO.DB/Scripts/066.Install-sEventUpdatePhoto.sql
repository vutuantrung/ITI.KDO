create proc dbo.sEventUpdatePhoto
(
    @EventId int,
	@Picture varbinary(max)
)
as
begin
	update dbo.tEvent
	set	Picture = @Picture
	where EventId = @EventId;
	return 0;
end;