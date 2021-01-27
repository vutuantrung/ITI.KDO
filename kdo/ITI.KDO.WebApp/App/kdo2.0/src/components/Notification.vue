<template>
    <div class="container">
        <h1>Contact Notifications List</h1>

        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>From</th>
                    <th>To</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="contactNotificationList.length == 0">
                    <td colspan="7" class="text-center">No notification</td>
                </tr>

                <tr v-for="i of contactNotificationList" :key="i.contactId">
                    <td>{{ i.senderEmail }}</td>
                    <td>{{ i.recipientsEmail }}</td>
                    <td>
                        <button @click="responseContactInvitation('yes', i.senderEmail, i.recipientsEmail, i.contactId)" class="btn btn-primary">Accept</button>
                        <button @click="responseContactInvitation('no', i.senderEmail, i.recipientsEmail, i.contactId)" class="btn btn-primary">Decline</button>
                    </td>
                </tr>
            </tbody>
        </table>

        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Event Name</th>
                    <th>Descriptions</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="eventNotificationList.length == 0">
                    <td colspan="7" class="text-center">No notification</td>
                </tr>

                <tr v-for="i of eventNotificationList" :key="i.eventId">
                    <td>{{ i.eventName }}</td>
                    <td>{{ i.description }}</td>
                    <td>
                        <button @click="responseEventInvitation('yes', i.eventId)" class="btn btn-primary">Accept</button>
                        <button @click="responseEventInvitation('no', i.eventId)" class="btn btn-primary">Decline</button>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../services/AuthService";
    import PresentApiService from '../services/PresentApiService';
    import UserApiService from '../services/UserApiService';
    import NotificationApiService from '../services/NotificationApiService';
    import ContactApiService from '../services/ContactApiService';
    import ParticipantApiService from '../services/ParticipantApiService';

    export default {
        data() {
            return {
                user: {},
                contactNotificationList: [],
                eventNotificationList: [],
                contactModel: {},
                eventModel: {}
            };
        },

        async mounted() {
            var userEmail = AuthService.emailUser();
            this.user = await UserApiService.getUserAsync(userEmail);
            await this.refreshContactNotificationList();
            await this.refreshEventNotificationList();
        },

        methods: {
            ...mapActions(['executeAsyncRequest']),

            async refreshContactNotificationList() {
                this.contactNotificationList = await NotificationApiService.getContactNotificationAsync(this.user.userId);
            },

            async refreshEventNotificationList(){
                this.eventNotificationList = await NotificationApiService.getEventNotificationAsync(this.user.userId);
            },

            async responseContactInvitation(response, firstEmail, secondEmail, notificationId){
                if(response == 'yes'){
                    try {
                        this.contactModel.senderEmail = firstEmail;
                        this.contactModel.recipientsEmail = secondEmail;

                        await NotificationApiService.setContactInvitationAsync(this.contactModel);
                    } catch (error) {
                        
                    }
                }else{
                    try {
                        await ContactApiService.deleteContactAsync(notificationId);
                    } catch (error) {
                        
                    }
                }
                await this.refreshContactNotificationList();
            },

            async responseEventInvitation(response, eventId){
                
                if(response == 'yes'){
                    try {
                        var modelEvent = {};
                        this.eventModel.userId = this.user.userId;
                        this.eventModel.eventId = eventId;
                        
                        await NotificationApiService.setEventInvitationAsync(this.eventModel);
                    } catch (error) {

                    }
                }else{
                    try {
                        await ParticipantApiService.deleteParticipantAsync(this.user.userId, eventId);
                    } catch (error) {
                        
                    }
                }
                await this.refreshEventNotificationList();
            }
        }
    };
</script>

<style lang="less">

</style>