<template>
    <div class="container">
        <b-row>
            <b-col>
                <h2>Your Presents List</h2>

                <table class="table table-striped table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>Present Name</th>
                            <th>Options</th>
                        </tr>
                    </thead>

                    <tbody>
                        <tr v-if="presentList.length == 0">
                            <td colspan="7" class="text-center">We-want-a-present!!!</td>
                        </tr>

                        <tr v-for="i of presentList">
                            <td>{{ i.presentName }}</td>
                            <td>
                                <button @click="addPresent(i.presentId, i)"  class="btn btn-primary">Add</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </b-col>

            <b-col>
                <h2>Event Presents List</h2>
                <table class="table table-striped table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>Present Name</th>
                            <th>Options</th>
                        </tr>
                    </thead>

                    <tbody>
                        <tr v-if="myQuantityList.length == 0">
                            <td colspan="7" class="text-center">We-want-a-present!!!</td>
                        </tr>

                        <tr v-for="i of myQuantityList">
                            <td>{{ i.presentName }}</td>
                            <td>
                                <button @click="removePresent(i)" class="btn btn-primary">Remove</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import PresentApiService from '../../services/PresentApiService';
    import UserApiService from '../../services/UserApiService';
    import EventApiService from '../../services/EventApiService';
    import QuantityApiService from '../../services/QuantityApiService';

  export default {
    data() {
        return {
            user: {},
            eventId: null,
            event: {},
            presentList: [],
            quantityList: [],
            myQuantityList: [],
            itemQuantity: {}
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        this.eventId = this.$route.params.id;
        this.event = await this.executeAsyncRequest(() => EventApiService.getEventByIdAsync(this.eventId));
        this.presentList = await PresentApiService.getPresentListAsync(this.user.userId);
        await this.refreshQuantityList();
        for(var i = 0; i < this.quantityList.length; i++)
        {
             this.presentList.splice(this.presentList.indexOf(await PresentApiService.getPresentAsync(this.quantityList[i].presentId)), 1);
        }
    },

    methods: {
      ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

      async refreshQuantityList(){
            this.myQuantityList = [];
            this.quantityList = await this.executeAsyncRequest(() => QuantityApiService.getQuantityPresentListAsync(this.user.userId, this.eventId));
            
            for (var i = 0; i < this.quantityList.length; i++)
            {
                if (this.quantityList[i].nominatorId == this.user.userId)
                    this.myQuantityList.push(this.quantityList[i]);
            }
      },
      
      async addPresent(presentId, present) {
          try {
              this.itemQuantity.quantity = 1;
              this.itemQuantity.recipientId = this.user.userId;
              this.itemQuantity.nominatorId = this.user.userId;
              this.itemQuantity.eventId = this.eventId;
              this.itemQuantity.presentId = presentId;
              await QuantityApiService.createQuantityAsync(this.itemQuantity);
              await this.refreshQuantityList();

              this.presentList.splice(this.presentList.indexOf(present), 1);
          }
          catch(error) {
          }
      },

      async removePresent(quantity) {
          try {
              this.presentList.push(await PresentApiService.getPresentAsync(quantity.presentId));
              await QuantityApiService.deleteQuantityAsync(quantity.quantityId);
              await this.refreshQuantityList();
          }
          catch(error) {
          }
      }
  }
  };
</script>

<style lang="less">
.container
{
    margin-top: 150px;
}
</style>