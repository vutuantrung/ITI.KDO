<template>
    <div>
        <div class="page-header">
            <h1>Facebook Friends</h1>
        </div>

        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Contact Id</th>
                    <th>Facebook Id</th>
                    <th>Email</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>BirthDate</th>
                    <th>Phone</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="contacts.length == 0">
                    <td colspan="7" class="text-center">You have no facebook friend.</td>
                </tr>

                <tr v-for="i of contacts" :key="i.contactId">
                    <td>{{ i.contactId }}</td>
                    <td>{{ i.facebookId }}</td>
                    <td>{{ i.email }}</td>
                    <td>{{ i.firstName }}</td>
                    <td>{{ i.lastName }}</td>
                    <td>{{ i.birthDate }}</td>
                    <td>{{ i.phone }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex';
    import FacebookApiService from "../../services/FacebookApiService";

    export default {
        data() {
            return {
                contacts: []
            }
        },

        async mounted() {
            await this.loadList();
        },

        methods: {
            ...mapActions(['executeAsyncRequestOrDefault']),

            async loadList() {
                // We use the "executeAsyncRequestOrDefault" (cf. vuex/actions.js) action that does all the job for us :
                // 1) Set the loading state to true
                // 2) Execute the callback
                // 3) In case of error, notify the application of that
                // 4) Reset the loading state to false
                // The callback is a "lambda function", like in C#
                // "executeAsyncRequestOrDefault" is nice: if an exception is thrown during the request, it is automatically catched, and the return value is undefined... 
                // So, it fails silently.
                
                this.contacts = await this.executeAsyncRequestOrDefault(() => FacebookApiService.getFacebookFriendsAsync());
            }
        }
    }
</script>

<style lang="less">

</style>