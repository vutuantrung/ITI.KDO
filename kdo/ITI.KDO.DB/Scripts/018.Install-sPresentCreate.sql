create procedure dbo.sPresentCreate
(
	@PresentName nvarchar(32),
	@Price float,
	@LinkPresent nvarchar(32),
	@Picture varbinary(max),
	@CategoryPresentId int,
	@UserId int
)
as
begin
	insert into dbo.tPresent(PresentName, Price, LinkPresent, Picture, CategoryPresentId, UserId)
	values(@PresentName, @Price, @LinkPresent, @Picture, @CategoryPresentId, @UserId);
	return scope_identity();
end
