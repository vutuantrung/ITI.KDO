create proc dbo.sParticipantUpdate
(
    @UserId				int,
	@EventId			int,
	@ParticipantType    bit,
	@Invitation			bit
)
as
begin
	update dbo.tParticipant
	set UserId = @UserId,
		EventId = @EventId,
		ParticipantType = @ParticipantType,
		Invitation = @Invitation
	where UserId = @UserId and EventId = @EventId;
	return 0;
end;