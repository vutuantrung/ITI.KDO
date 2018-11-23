create procedure dbo.sFacebookContactInfoUpdate
(
	@FacebookId int,
    @Email 		nvarchar(64),
    @FirstName 	nvarchar(64),
    @LastName 	nvarchar(64),
    @BirthDate 	date,
    @Phone 		nvarchar(64)
)
as
begin
	update dbo.tFacebookContactInfo
	set 
		FacebookId = @FacebookId,
		Email = @Email,
		FirstName = @FirstName,
		LastName = @LastName,
		BirthDate = @BirthDate,
		Phone = @Phone
	where FacebookId = @FacebookId;
	return 0;
end;