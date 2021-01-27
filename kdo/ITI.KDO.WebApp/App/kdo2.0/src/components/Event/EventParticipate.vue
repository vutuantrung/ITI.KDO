<template>
<div>
    <div class="title">
        <h1 >PARTICIPATE</h1>
    </div>
    </section>

    <b-row>
        <b-col md="1">
        </b-col>
        <b-col md="5">
            <b-card header="INFORMATIONS" style="margin-left:1%;">
                <h6 class="text-center" slot="header" text-variant="white">INFORMATIONS</h6>
            <b-list-group class="text-center">
            <b-list-group-item>{{present.presentName}} </b-list-group-item>
            <b-list-group-item>{{present.price}}€</b-list-group-item>
            <b-list-group-item>{{present.linkPresent}}</b-list-group-item>
            <b-list-group-item>Number of this present : {{quantity.quantity}}</b-list-group-item>
            <b-list-group-item>Present for : {{recipient.firstName}} {{recipient.lastName}}</b-list-group-item>
            <b-list-group-item>Present idea from : {{nominator.firstName}} {{nominator.lastName}}</b-list-group-item>
            </b-list-group>
            </b-card>
        </b-col>
        <b-col md="4">
            <b-card>
                <b-form-input v-model="price"
                    type="text"
                    placeholder="Enter the ammount :">
                </b-form-input>

                <b-button @click="Particip()" class="btn btn-success">Participate</b-button>
                <b-button v-if="existing" @click="DeleteParticipation()" class="btn btn-danger">Remove your participation</b-button>                
            </b-card>

            <b-card style="margin-top:10%;" class="text-center" bg-variant="light" header="PARTICIPANTS">
                <h6 slot="header"class="mb-0">PARTICIPANTS</h6>
                <b-card v-for="i in participantUserList"
                    tag="article"
                    style="max-width: 16rem;margin-left:23%;"
                    class="mb-2">
                    <router-link :to="`/userProfile/display/${i.email}`">{{i.firstName}} {{i.lastName}}</router-link>
                </b-card>
            </b-card>
        </b-col>
    </b-row>
    </div>
</template>
<script>
   import { mapActions } from 'vuex';
    import PresentApiService from '../../services/PresentApiService';
    import QuantityApiService from '../../services/QuantityApiService';
    import UserApiService from "../../services/UserApiService";    
    import ParticipationApiService from "../../services/ParticipationApiService";
    import AuthService from "../../services/AuthService";

  export default {
    data() {
      return {
        user:{},
        present:{},
        quantity:{},
        nominator:{},
        recipient:{},
        participation:{},
        participantUserList:[],
        price: null,
        quantityId: null,
        eventId: null,
        existing: false
      };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        this.quantityId = this.$route.params.qid;
        this.eventId = this.$route.params.eid;

        this.quantity = await this.executeAsyncRequest(() => QuantityApiService.getQuantityAsync(this.quantityId));
        this.present = await this.executeAsyncRequest(() => PresentApiService.getPresentAsync(this.quantity.presentId));
        this.nominator = await this.executeAsyncRequest(() => UserApiService.getUserByIdAsync(this.quantity.nominatorId));
        this.recipient = await this.executeAsyncRequest(() => UserApiService.getUserByIdAsync(this.quantity.recipientId));
        await this.UpdateParticipantUserList();

        if (await this.executeAsyncRequest(() => ParticipationApiService.existingParticipationAsync(this.quantityId, this.user.userId)))
        {
            this.existing = true;
            this.participation = await this.executeAsyncRequest(() => ParticipationApiService.getParticipationAsync(this.quantityId, this.user.userId));
            this.price = this.participation.amountUserPrice;
        }
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),

        async Particip()
        {
            if (this.existing)
            {
                this.participation.amountUserPrice = this.price;
                await this.executeAsyncRequest(() => ParticipationApiService.updateParticipationAsync(this.participation));
            }
            else
            {
                this.participation.quantityId = this.quantityId;
                this.participation.userId = this.user.userId;
                this.participation.eventId = this.eventId;
                this.participation.amountUserPrice = this.price;
                var aux = await this.executeAsyncRequest(() => ParticipationApiService.createParticipationAsync(this.participation));
            }
            this.$router.go(-1);
        },

        async DeleteParticipation()
        {
            this.$router.go(-1);
            await this.executeAsyncRequest(() => ParticipationApiService.deleteParticipationAsync(this.quantityId, this.user.userId));
        },

        async UpdateParticipantUserList()
        {
            var user;
            var aux = await this.executeAsyncRequest(() => ParticipationApiService.getParticipationByQuantityAsync(this.quantityId));
            for (var i = 0; i < aux.length; i++)
            {
                user = await UserApiService.getUserByIdAsync(aux[i].userId);
                this.participantUserList.push(user);
            }
        }
    }
};
</script>
<style lang="less">
button.btn.btn-danger.btn.btn-secondary {
    background-color: #c75c5c;

}

</style>