create proc dbo.sUserCreate
(
	@FirstName nvarchar(32),
	@LastName nvarchar(32),
	@Birthdate date,
	@Email nvarchar(32)
)
as
begin
	insert into dbo.tUser(FirstName , LastName , Birthdate , Email)
	               values(@FirstName, @LastName, @Birthdate, @Email);
	return scope_identity();
end