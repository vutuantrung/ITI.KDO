create view dbo.vFacebookContact
as
	select
        UserId = c.UserId,
        FacebookId = c.FacebookId,
		Email = i.Email,
		FirstName = i.FirstName,
		LastName = i.LastName,
		BirthDate = i.BirthDate,
		Phone = i.Phone
		
	from dbo.tFacebookContact c
		left outer join dbo.tFacebookContactInfo i on i.FacebookId = c.FacebookId
	where c.UserId <> 0;