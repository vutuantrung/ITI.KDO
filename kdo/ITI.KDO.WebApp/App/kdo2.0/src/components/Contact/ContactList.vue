<template>
    <div class="container">
      <div class="page-header">
            <h1>Contact List</h1>
      </div>

      <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Contact Id</th>
                    <th>First Email</th>
                    <th>Second Email</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="contactList.length == 0">
                    <td colspan="7" class="text-center">No Contact found</td>
                </tr>

                <tr v-for="i of contactList" :key="i.contactId">
                    <td>{{ i.contactId }}</td>
                    <td>{{ i.friendEmail}}</td>
                    <td>
                    </td>
                </tr>
                                    <td><router-link :to="`userProfile/display/${i.userEmail}`">{{ i.userEmail }}</router-link></td>

            </tbody>
        </table>

    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import UserApiService from '../../services/UserApiService';
    import ContactApiService from '../../services/ContactApiService';

  export default {
    data() {
        return {
            user: {},
            contactList: []
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        await this.refreshList();
    },

    methods: {
      ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

      async refreshList() {
        this.contactList = await ContactApiService.getContactListAsync(this.user.userId);
      },

      async deleteContact(contactId) {
        try {
            await ContactApiService.deleteContactAsync(contactId);
        } catch (error) {
            
        }
        await this.refreshList();
      }
  }
  };
</script>

<style lang="less">

</style>