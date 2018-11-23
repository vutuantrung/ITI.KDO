create proc dbo.sPresentUpdatePhoto
(
    @PresentId int,
	@Picture varbinary(max)
)
as
begin
	update dbo.tPresent
	set	Picture = @Picture
	where PresentId = @PresentId;
	return 0;
end;