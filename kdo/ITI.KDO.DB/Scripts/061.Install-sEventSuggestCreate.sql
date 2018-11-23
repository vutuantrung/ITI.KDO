create procedure dbo.sEventSuggestCreate
(
	@EventId int,
	@UserId int,
	@DateSuggest date,
	@Descriptions nvarchar(256)
)
as
begin
	insert into dbo.tEventSuggest(EventId , UserId , DateSuggest , Descriptions)
	                	   values(@EventId, @UserId, @DateSuggest, @Descriptions);
end