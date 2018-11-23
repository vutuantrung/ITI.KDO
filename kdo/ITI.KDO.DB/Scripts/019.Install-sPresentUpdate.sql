create procedure dbo.sPresentUpdate
(
    @PresentId         int,
	@UserId 		   int,
	@CategoryPresentId int,
	@Price             float,
	@LinkPresent 	   nvarchar(32),
	@Picture		   varbinary(max),
	@PresentName 	   nvarchar(32)
)
as
begin
	update dbo.tPresent
	set UserId = @UserId,
		CategoryPresentId = @CategoryPresentId,
		Price = @Price,
		LinkPresent = @LinkPresent,
		Picture = @Picture,
		PresentName = @PresentName
	where PresentId = @PresentId;
	return 0;
end;