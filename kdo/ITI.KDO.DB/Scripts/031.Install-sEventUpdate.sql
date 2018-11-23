create procedure dbo.sEventUpdate
(
    @EventId         int,
	@Dates			 date,
	@Descriptions 	 nvarchar(200),
	@EventName 	     nvarchar(32)
)
as
begin
	update dbo.tEvent
	set 
		Dates = @Dates,
		Descriptions = @Descriptions,
		EventName = @EventName
	where EventId = @EventId;
	return 0;
end;