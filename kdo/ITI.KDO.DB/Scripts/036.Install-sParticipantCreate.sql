create procedure dbo.sParticipantCreate
(
    @UserId				int,
	@EventId			int,
	@ParticipantType    bit,
	@Invitation			bit
)

as
begin
	insert into dbo.tParticipant(UserId, EventId, ParticipantType, Invitation)
	values(@UserId, @EventId, @ParticipantType, @Invitation);
	return 0;
end