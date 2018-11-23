create procedure dbo.sFacebookContactDelete
(
	@FacebookId int
)
as
begin
    delete from dbo.tFacebookContact where FacebookId = @FacebookId;
    delete from dbo.tFacebookContactInfo where FacebookId = @FacebookId;
	return 0;
end;