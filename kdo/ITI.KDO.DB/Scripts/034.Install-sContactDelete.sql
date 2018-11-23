create procedure dbo.sContactDelete
(
    @ContactId int
)
as
begin
	delete from dbo.tContact 
	where ContactId = @ContactId;
	return 0;
end;