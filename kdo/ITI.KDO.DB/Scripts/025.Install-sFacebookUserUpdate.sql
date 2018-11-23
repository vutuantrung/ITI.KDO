create procedure dbo.sFacebookUserUpdate
(
	@UserId      int,
	@FacebookId	 varchar(32),
	@AccessToken varchar(256)
)
as
begin
	update dbo.tFacebookUser set AccessToken = @AccessToken where UserId = @UserId;
	return 0;
end;