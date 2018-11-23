create view dbo.vEventSuggest
as
	select
	    EventId = e.EventId,
		UserId = e.UserId,
		DateSuggest = e.DateSuggest,
		Descriptions = e.Descriptions

	from dbo.tEventSuggest e;