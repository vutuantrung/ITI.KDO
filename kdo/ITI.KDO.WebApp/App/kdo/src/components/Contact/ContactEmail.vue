<template>
<div>
<section>
  <div class="title">
    <h1>CONTACT LIST</h1>
  </div>
</section>
<b-col class="my-1">
        <b-form-group horizontal class="mb-0">
          <b-input-group>
            <b-form-input v-model="filter" placeholder="Type to Search" />
            <b-btn>Go</b-btn>
            <b-input-group-button>
              <b-btn :disabled="!filter" @click="filter = ''">Clear</b-btn>
            </b-input-group-button>
          </b-input-group>
        </b-form-group>
      </b-col>



    <b-table show-empty
        stacked="md"
        :items="userList"
        :filter="filter"
        @filtered="onFiltered">
      <template slot="userId" slot-scope="row">{{row.value.firstName}} {{row.value.lastName}}</template>

        <template slot="row-details" slot-scope="row">
            <b-card>
                <ul>
                    <li v-for="(value, key) in row.item" :key="userId">{{ key }}: {{ value}}</li>
                </ul>
            </b-card>
        </template>
    </b-table>
</div>
</template>
<script>
  import { mapActions } from 'vuex';
   import AuthService from "../../services/AuthService";
    import UserApiService from '../../services/UserApiService';

  export default {
    data() {
        return {
            user: {},
            userList: [],
            filter: null,
                };
    },
     computed: {
    sortOptions () {
      // Create an options list from our fields
      return this.fields
        .filter(f => f.sortable)
        .map(f => { return { text: f.label, value: f.key } })
    }
  },
    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        await this.refreshList();
    },
    methods: {
      ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

      async refreshList() {
            this.userList = await UserApiService.getAllUserAsync();
      },
          info (item, index, button) {
      this.modalInfo.title = `Row index: ${index}`
      this.modalInfo.content = JSON.stringify(item, null, 2)
      this.$root.$emit('bv::show::modal', 'modalInfo', button)
    },
    resetModal () {
      this.modalInfo.title = ''
      this.modalInfo.content = ''
    },
    onFiltered (filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length
      this.currentPage = 1
    }
  }
  };

</script>
<style lang="less">

#custom-search-input{
    padding: 3px;
    border: solid 1px #E4E4E4;
    border-radius: 6px;
    background-color: #fff;
}

#custom-search-input input{
    border: 0;
    box-shadow: none;
}

#custom-search-input button{
    margin: 2px 0 0 0;
    background: none;
    box-shadow: none;
    border: 0;
    color: #666666;
    padding: 0 8px 0 10px;
    border-left: solid 1px #ccc;
}

#custom-search-input button:hover{
    border: 0;
    box-shadow: none;
    border-left: solid 1px #ccc;
}

#custom-search-input .glyphicon-search{
    font-size: 23px;
}
</style>